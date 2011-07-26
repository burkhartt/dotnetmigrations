namespace DotNetMigrations.Tim
{
    public interface IDatabaseVersionChecker
    {
        long GetCurrentVersion();
        long GetPreviousDatabaseVersion(long dbVersion);
    }
}