namespace DotNetMigrations.Tim
{
    public interface ILongValueFromDatabaseRetriever
    {
        long GetLongFromSqlStatement(string sqlStatement);
    }
}