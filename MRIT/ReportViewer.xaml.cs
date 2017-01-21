using LiveCharts;
using LiveCharts.Wpf;
using System.Windows;

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
    }
}
