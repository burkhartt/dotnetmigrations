using System.Data.Common;
using DotNetMigrations.Core;
using DotNetMigrations.Core.Data;

namespace DotNetMigrations.UnitTests.Tim
{
    public class LongValueFromDatabaseRetriever : ILongValueFromDatabaseRetriever
    {
        private readonly DataAccess dataAccess;
        private readonly ILogger logger;

        public LongValueFromDatabaseRetriever(DataAccess dataAccess, ILogger logger)
        {
            this.dataAccess = dataAccess;
            this.logger = logger;
        }

        public long GetLongFromSqlStatement(string sqlStatement)
        {
            long currentVersion = -1;

            try
            {
                using (var cmd = dataAccess.CreateCommand())
                {
                    cmd.CommandText = sqlStatement;
                    var version = cmd.ExecuteScalar<string>();
                    if (version != null)
                    {
                        version = version.Trim();
                    }
                    long.TryParse(version, out currentVersion);

                    if (currentVersion < 0)
                    {
                        throw new SchemaException("schema_migrations table appears to be corrupted");
                    }
                }
            }
            catch (DbException ex)
            {
                logger.WriteError(ex.Message);
            }

            return currentVersion;
        }
    }
}