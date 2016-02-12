using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JuliaHayward.Common.Logging;

namespace CommonTests
{
    [TestClass]
    public class LoggingTests
    {
        [TestMethod]
        public void TrelloLogger_Works()
        {
            var key = ConfigurationManager.AppSettings["TrelloKey"];
            var auth = ConfigurationManager.AppSettings["TrelloAuthKey"];
            var logger = new TrelloLogger(key, auth);

            logger.Error("Test", "Test message", "Test description");
            // Need to check Trello now!
        }
    }
}
