namespace Mongoloid
{
    internal class RansomSchema
    {
        public int ID { get; set; }
        public string DatabaseName { get; set; }
        public string CollectionName { get; set; }
        public string BitcoinField { get; set; }
        public string EmailField { get; set; }
        public string NotesField { get; set; }
        public bool IsSelected { get; set; }

        public RansomSchema() { }

        public RansomSchema(string databaseName, string collectionName, string btcField, string emailField, string notesField)
        {
            DatabaseName = databaseName;
            CollectionName = collectionName;
            BitcoinField = btcField;
            EmailField = emailField;
            NotesField = notesField;
        }
    }
}
