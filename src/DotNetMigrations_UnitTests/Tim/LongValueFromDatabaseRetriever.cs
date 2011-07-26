using System.Data.Common;
using DotNetMigrations.Core;
using DotNetMigrations.Core.Data;

namespace DotNetMigrations.UnitTests.Tim
{
    public class LongValueFromDatabaseRetriever : ILongValueFromDatabaseRetriever
    {
        private readonly DataAccess dataAccess;
        private readonly ILogger logger;
        private const int ErrorCode = -1;

        public LongValueFromDatabaseRetriever(DataAccess dataAccess, ILogger logger)
        {
            this.dataAccess = dataAccess;
            this.logger = logger;
        }

        public long GetLongFromSqlStatement(string sqlStatement)
        {
            try
            {
                return CheckTheDatabaseForTheCurrentVersion(sqlStatement);
            }
            catch (DbException ex)
            {
                logger.WriteError(ex.Message);
                return ErrorCode;
            }
        }

        private long CheckTheDatabaseForTheCurrentVersion(string sqlStatement)
        {
            using (var cmd = dataAccess.CreateCommand())
            {
                var currentVersion = GetTheCurrentVersionFromTheDatabase(cmd, sqlStatement);
                CheckThatTheSchemaTableIsNotCorrupted(currentVersion);
                return currentVersion;
            }
        }

        private static void CheckThatTheSchemaTableIsNotCorrupted(long currentVersion)
        {
            if (currentVersion < 0)
                throw new SchemaException("schema_migrations table appears to be corrupted");
        }

        private static long GetTheCurrentVersionFromTheDatabase(DbCommand cmd, string sqlStatement)
        {
            long currentVersion;
            cmd.CommandText = sqlStatement;
            var version = cmd.ExecuteScalar<string>();
            if (version != null)
                version = version.Trim();
            long.TryParse(version, out currentVersion);
            return currentVersion;
        }
    }
}