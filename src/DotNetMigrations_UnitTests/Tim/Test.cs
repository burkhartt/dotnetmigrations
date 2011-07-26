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