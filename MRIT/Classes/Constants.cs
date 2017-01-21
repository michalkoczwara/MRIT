namespace MRIT
{
    internal class Constants
    {
        internal const string SqlConnectionString = "Server=NAS;Database=OpenMongo;User Id=OpenMongoUser;Password=OpenMongoUser";
        internal const string JsonFileFilter = "*.json;*.txt|*.json;*.txt";
        internal const string CsvFileFilter = "*.csv;*.txt|*.csv;*.txt";
        internal const string ImportFailedStatus = "Failed to import Shodan records from export file.";
        internal const string GetDatabasesStatus = "Getting databases from MongoDB servers.";
        internal const string NumberValidatorRegex = "[^0-9]+";
    }
}
