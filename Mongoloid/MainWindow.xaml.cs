using Microsoft.Win32;
using Mongoloid.Classes;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Mongoloid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Thread _importThread;
        private List<Thread> _executorThreads;
        private List<ThreadedExecutor> _threadExecutors;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImportRecords_Click(object sender, RoutedEventArgs e)
        {
            if (ImportRecords.Content.Equals("Import"))
            {
                ImportRecords.Content = "Stop";
                SetImportControlsEnabledState(false);
                _importThread = new Thread(ImportShodanFile);
                _importThread.Start();
            }
            else
            {
                SetImportControlsEnabledState(true);
                ImportRecords.Content = "Import";
                if (_importThread != null && _importThread.IsAlive)
                    _importThread.Abort();
            }
        }

        private void SetImportControlsEnabledState(bool enabled)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                ImportRecords.IsEnabled = enabled;
                CsvRadioButton.IsEnabled = enabled;
                JsonRadioButton.IsEnabled = enabled;
                OpenFile.IsEnabled = enabled;
                InputFile.IsEnabled = enabled;
            });
        }

        private void SetCrawlControlsEnabled(bool enabled)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                ThreadCount.IsEnabled = enabled;
                GetRansoms.IsEnabled = enabled;
            });
        }

        private void ImportShodanFile()
        {
            var inputFile = ThreadedFunctions.GetControlValue("InputFile", Dispatcher, this).ToLower();
            var csvIsChecked = ThreadedFunctions.GetControlValue("CsvRadioButton", Dispatcher, this).ToLower().Equals("true");
            ThreadedFunctions.SetControlValue("CurrentStatus", "Getting records from Shodan export.", Dispatcher, this);
            var hosts = csvIsChecked ? FileFunctions.GetAllRecordsFromCsvFile(inputFile) : FileFunctions.GetAllRecordsFromJsonFile(inputFile);
            ThreadedFunctions.SetControlValue("CurrentStatus", $"Adding {hosts.Count} records to SQL database.", Dispatcher, this);
            if (SqlFunctions.AddShodanRecordToSqlDatabase(hosts))
            {
                ThreadedFunctions.SetControlValue("CurrentStatus", $"Done, {hosts.Count} records added.", Dispatcher, this);
                return;
            }
            ThreadedFunctions.ShowError(Constants.ImportFailedStatus, Dispatcher);
            ThreadedFunctions.SetControlValue("CurrentStatus", "ERROR!", Dispatcher, this);
            SetImportControlsEnabledState(true);
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = CsvRadioButton.IsChecked == true ? Constants.CsvFileFilter : Constants.JsonFileFilter };
            if (ofd.ShowDialog() == true)
                InputFile.Text = ofd.FileName;
        }

        private void btnGetRansoms_Click(object sender, RoutedEventArgs e)
        {
            if (GetRansoms.Content.Equals("Get Ransoms"))
            {
                GetRansoms.Content = "Stop";
                SetCrawlControlsEnabled(false);
                var hosts = SqlFunctions.GetAllIpsAndPorts();
                var ransomSchemas = SqlFunctions.GetAllRansomSchemas();
                TotalServers.Content = hosts.Count.ToString();
                var threadCount = int.Parse(ThreadCount.Text);
                var leftOver = hosts.Count % threadCount;
                var countPerPage = (hosts.Count - leftOver) / threadCount;
                _threadExecutors = new List<ThreadedExecutor>();
                _executorThreads = new List<Thread>();
                for (var thread = 0; thread < threadCount; thread++)
                {
                    var skipItems = countPerPage * thread;
                    if (thread == (threadCount - 1))
                        countPerPage += leftOver;
                    var newExecutor = new ThreadedExecutor(Dispatcher, this, hosts.Skip(skipItems).Take(countPerPage).ToList(), ransomSchemas);
                    _threadExecutors.Add(newExecutor);
                    _executorThreads.Add(new Thread(newExecutor.StartGettingDetails));
                }
                foreach (var thread in _executorThreads)
                    thread.Start();
            }
            else
            {
                SetCrawlControlsEnabled(true);
                GetRansoms.Content = "Get Ransoms";
                foreach (var thread in _executorThreads)
                {
                    if (thread != null && thread.IsAlive)
                        thread.Abort();
                }
            }
        }

        public void CheckThreadStates()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)delegate
            {
                if (_threadExecutors.Any(executor => !executor.CurrentState.Equals("Stopped")))
                    return;
                SetCrawlControlsEnabled(true);
                CurrentStatus.Content = "DONE!";
            });
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex(Constants.NumberValidatorRegex);
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ManageRansomSchemas_Click(object sender, RoutedEventArgs e)
        {
            var manageSchemas = new ManageSchemas();
            manageSchemas.ShowDialog();
        }

        private void ViewReports_Click(object sender, RoutedEventArgs e)
        {
            var viewReports = new ReportViewer();
            viewReports.ShowDialog();
        }
    }
}
