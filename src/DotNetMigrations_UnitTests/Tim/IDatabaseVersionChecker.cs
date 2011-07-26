namespace DotNetMigrations.UnitTests.Tim
{
    public interface IDatabaseVersionChecker
    {
        long GetCurrentVersion();
        long GetPreviousDatabaseVersion(long dbVersion);
    }
}