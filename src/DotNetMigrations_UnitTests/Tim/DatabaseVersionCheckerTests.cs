using DotNetMigrations.Tim;
using Moq;
using NUnit.Framework;

namespace DotNetMigrations.UnitTests.Tim
{
    [TestFixture]
    public class DatabaseVersionCheckerTests
    {
        [Test]
        public void Returns_5_when_the_max_version_is_5()
        {
            var retriever = GetDatabaseRetrieverThatReturnsThisValueFromThisSqlStatement(5,
                                                                                         "SELECT MAX([version]) FROM [schema_migrations]");

            var result = new DatabaseVersionChecker(retriever).GetCurrentVersion();

            Assert.AreEqual(5, result);
        }

        [Test]
        public void Returns_12_when_the_max_version_is_12()
        {
            var retriever = GetDatabaseRetrieverThatReturnsThisValueFromThisSqlStatement(12,
                                                                                         "SELECT MAX([version]) FROM [schema_migrations]");

            var result = new DatabaseVersionChecker(retriever).GetCurrentVersion();

            Assert.AreEqual(12, result);
        }

        [Test]
        public void Returns_4_when_the_previous_version_is_4()
        {
            var retriever = GetDatabaseRetrieverThatReturnsThisValueFromThisSqlStatement(4,
                                                                                         "SELECT MAX([version]) FROM [schema_migrations] WHERE [version] <> 5");

            var result = new DatabaseVersionChecker(retriever).GetPreviousDatabaseVersion(5);

            Assert.AreEqual(4, result);
        }

        [Test]
        public void Returns_11_when_the_previous_version_is_11()
        {
            var retriever = GetDatabaseRetrieverThatReturnsThisValueFromThisSqlStatement(11,
                                                                                         "SELECT MAX([version]) FROM [schema_migrations] WHERE [version] <> 12");

            var result = new DatabaseVersionChecker(retriever).GetPreviousDatabaseVersion(12);

            Assert.AreEqual(11, result);
        }

        private static ILongValueFromDatabaseRetriever GetDatabaseRetrieverThatReturnsThisValueFromThisSqlStatement(
            int value, string sqlStatement)
        {
            var longValueFromDatabaseRetriever = new Mock<ILongValueFromDatabaseRetriever>();
            longValueFromDatabaseRetriever.Setup(
                x => x.GetLongFromSqlStatement(sqlStatement))
                .Returns(value);
            return longValueFromDatabaseRetriever.Object;
        }
    }
}