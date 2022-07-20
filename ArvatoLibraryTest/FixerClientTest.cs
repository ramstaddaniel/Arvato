using ArvatoLibrary;
using ArvatoLibrary.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArvatoLibraryTest {
    [TestClass]
    public class FixerClientTest {

        [TestMethod]
        public void GetLatest() {
            Currency currency = FixerApiClient.Get("eur", "nok").Result;

            Assert.IsNotNull(currency, "Currency is NULL");
        }
    }
}
