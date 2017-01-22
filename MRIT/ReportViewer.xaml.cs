using System.Data.SqlTypes;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MRIT
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer
    {
        public ReportViewer()
        {
            InitializeComponent();
        }

        private void ViewRansomsByEmail_Click(object sender, RoutedEventArgs e)
        {
            RansomRecordsList.Items.Clear();
            RansomRecordsList.Visibility = Visibility.Hidden;
            pieChart.Visibility = Visibility.Visible;
            pieChart.Series.Clear();
            ReportTitle.Content = "Ransom Demands per Attacker Email";
            var attackers = SqlFunctions.GetRansomsByEmailAddresses();
            foreach (var attacker in attackers)
                pieChart.Series.Add(new PieSeries
                {
                    Title = attacker.Email,
                    Values = new ChartValues<int> { int.Parse(attacker.RansomText) },
                    DataLabels = true, HorizontalAlignment = HorizontalAlignment.Center, IsManipulationEnabled = true
                });
        }

        private void ViewRansomsVsHosts_Click(object sender, RoutedEventArgs e)
        {
            RansomRecordsList.Items.Clear();
            RansomRecordsList.Visibility = Visibility.Hidden;
            pieChart.Visibility = Visibility.Visible;
            pieChart.Series.Clear();
            ReportTitle.Content = "Hosts Held Ransom vs Total Ransom Demands";
            var countInfo = SqlFunctions.GetRansomsVsHosts();
            foreach (var count in countInfo)
            pieChart.Series.Add(new PieSeries
            {
                Title = count[0],
                Values = new ChartValues<int> { int.Parse(count[1]) },
                DataLabels = true,
                HorizontalAlignment = HorizontalAlignment.Center,
                IsManipulationEnabled = true
            });
        }


        private void ViewBtcWalletsByEmail_Click(object sender, RoutedEventArgs e)
        {
            RansomRecordsList.Items.Clear();
            RansomRecordsList.Visibility = Visibility.Hidden;
            pieChart.Visibility = Visibility.Visible;
            pieChart.Series.Clear();
            ReportTitle.Content = "Number of Bitcoin Wallets per Attacker";
            var attackers = SqlFunctions.GetBtcWalletsByEmailAddresses();
            foreach (var attacker in attackers)
                pieChart.Series.Add(new PieSeries
                {
                    Title = attacker.Email,
                    Values = new ChartValues<int> { int.Parse(attacker.RansomText) },
                    DataLabels = true,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    IsManipulationEnabled = true
                });
        }

        private void ViewAllRansoms_Click(object sender, RoutedEventArgs e)
        {
            pieChart.Visibility = Visibility.Hidden;
            RansomRecordsList.Visibility = Visibility.Visible;
            RansomRecordsList.Items.Clear();
            ReportTitle.Content = "All Ransom Demands Found";
            var newView = new GridView { AllowsColumnReorder = true };
            newView.Columns.Add(new GridViewColumn { Header = "Host", Width = 110, DisplayMemberBinding = new Binding("Ip") });
            newView.Columns.Add(new GridViewColumn { Header = "Port", Width = 90, DisplayMemberBinding = new Binding("Port") });
            newView.Columns.Add(new GridViewColumn { Header = "Database", Width = 200, DisplayMemberBinding = new Binding("DatabaseName") });
            newView.Columns.Add(new GridViewColumn { Header = "BTC Wallet", Width = 275, DisplayMemberBinding = new Binding("BitcoinWallet") });
            newView.Columns.Add(new GridViewColumn { Header = "Email", Width = 150, DisplayMemberBinding = new Binding("Email") });
            newView.Columns.Add(new GridViewColumn { Header = "Ransom Note", Width = 500, DisplayMemberBinding = new Binding("RansomText") });
            RansomRecordsList.View = newView;
            var ransoms = SqlFunctions.GetAllRansoms();
            foreach (var ransom in ransoms)
                RansomRecordsList.Items.Add(ransom);
        }
    }
}
