using System;

namespace DotNetMigrations.UnitTests.Tim
{
    public class DatabaseVersionChecker : IDatabaseVersionChecker
    {
        private readonly ILongValueFromDatabaseRetriever longValueFromDatabaseRetriever;

        public DatabaseVersionChecker(ILongValueFromDatabaseRetriever longValueFromDatabaseRetriever)
        {
            this.longValueFromDatabaseRetriever = longValueFromDatabaseRetriever;
        }

        public long GetCurrentVersion()
        {
            return longValueFromDatabaseRetriever.GetLongFromSqlStatement("SELECT MAX([version]) FROM [schema_migrations]");
        }

        public long GetPreviousDatabaseVersion(long dbVersion)
        {
            return
                longValueFromDatabaseRetriever.GetLongFromSqlStatement(
                    "SELECT MAX([version]) FROM [schema_migrations] WHERE [version] <> " + dbVersion);
        }
    }
}