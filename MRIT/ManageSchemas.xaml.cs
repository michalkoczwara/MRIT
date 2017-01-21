using System.Windows;

namespace MRIT
{
    /// <summary>
    /// Interaction logic for ManageSchemas.xaml
    /// </summary>
    public partial class ManageSchemas
    {
        public ManageSchemas()
        {
            InitializeComponent();
        }

        private void RefreshRansoms_Click(object sender, RoutedEventArgs e)
        {
            RansomSchemas.Items.Clear();
            foreach (var ransomSchema in SqlFunctions.GetAllRansomSchemas())
                RansomSchemas.Items.Add(ransomSchema);
        }

        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            foreach (RansomSchema ransomSchema in RansomSchemas.SelectedItems)
                SqlFunctions.DeleteRansomSchema(ransomSchema);
            DatabaseNameValue.Text = CollectionNameValue.Text = EmailFieldNameValue.Text = BtcNameValue.Text = RansomNameValue.Text = string.Empty;
            RefreshRansoms_Click(this, e);
        }

        private void RansomSchemas_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (RansomSchemas.SelectedItems == null || RansomSchemas.SelectedItems.Count < 1)
                return;
            var selectedRansomSchema = (RansomSchema)RansomSchemas.SelectedItems[0];
            DatabaseNameValue.Text = selectedRansomSchema.DatabaseName;
            CollectionNameValue.Text = selectedRansomSchema.CollectionName;
            EmailFieldNameValue.Text = selectedRansomSchema.EmailField;
            BtcNameValue.Text = selectedRansomSchema.BitcoinField;
            RansomNameValue.Text = selectedRansomSchema.NotesField;
        }

        private void AddSchema_Click(object sender, RoutedEventArgs e)
        {
            SqlFunctions.AddUpdateRansomSchema(new RansomSchema(DatabaseNameValue.Text, CollectionNameValue.Text, BtcNameValue.Text, EmailFieldNameValue.Text, RansomNameValue.Text) {ID = -1});
            DatabaseNameValue.Text = CollectionNameValue.Text = EmailFieldNameValue.Text = BtcNameValue.Text = RansomNameValue.Text = string.Empty;
            RefreshRansoms_Click(this, e);
        }

        private void UpdateSchema_Click(object sender, RoutedEventArgs e)
        {
            if (RansomSchemas.SelectedItems == null || RansomSchemas.SelectedItems.Count < 1)
            {
                MessageBox.Show("No schema has been selected for update.", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                SqlFunctions.AddUpdateRansomSchema(new RansomSchema(DatabaseNameValue.Text, CollectionNameValue.Text, BtcNameValue.Text, EmailFieldNameValue.Text, RansomNameValue.Text) { ID = ((RansomSchema)RansomSchemas.SelectedItems[0]).ID });
                DatabaseNameValue.Text = CollectionNameValue.Text = EmailFieldNameValue.Text = BtcNameValue.Text = RansomNameValue.Text = string.Empty;
                RefreshRansoms_Click(this, e);
            }
        }

        private void ManageSchemasWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshRansoms_Click(this, e);
        }
    }
}
