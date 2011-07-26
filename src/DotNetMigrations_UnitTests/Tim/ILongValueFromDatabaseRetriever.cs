namespace DotNetMigrations.UnitTests.Tim
{
    public interface ILongValueFromDatabaseRetriever
    {
        long GetLongFromSqlStatement(string sqlStatement);
    }
}