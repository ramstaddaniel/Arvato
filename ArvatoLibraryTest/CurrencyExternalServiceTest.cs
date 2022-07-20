using ArvatoLibrary.DataModels;
using ArvatoLibrary.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ArvatoLibraryTest {
    [TestClass]
    public class CurrencyExternalServiceTest {

        [TestMethod]
        public void Convert() {
            CurrencyExternalService currencyService = new CurrencyExternalService();

            decimal? amountWithDate = currencyService.Convert("eur", "nok", 10, new DateTime(2022, 02, 15)).Result;
            decimal? amountLatest = currencyService.Convert("eur", "nok", 10).Result;


            Assert.IsNotNull(amountLatest, "Latest conversion amount is NULL");
            Assert.IsNotNull(amountWithDate, "Conversion amount on date is NULL");
            Assert.AreNotEqual(amountWithDate, amountLatest, "Conversion amount on date and latest is equal");
        }

        [TestMethod]
        public void Get() {
            Currency currency = new CurrencyService().Get("eur").Result;

            Assert.IsNotNull(currency, "Currency is NULL");
            Assert.IsNotNull(currency.Rates, "Currency rates is NULL");
        }

        [TestMethod]
        public void UpdateAll() {
            DateTime now = DateTime.Now;

            bool isUpdateOk = new CurrencyExternalService().
                UpdateAllCurrencies(null, new List<Currency> {
                    new Currency {
                        Code = "EUR",
                        Rates = new List<CurrencyDate> {
                            new CurrencyDate {
                                Date = now,
                                Rates = new List<CurrencyRate> {
                                    new CurrencyRate {
                                        Code = "NOK",
                                        Rate = 12.3m
                                    },
                                    new CurrencyRate {
                                        Code = "SEK",
                                        Rate = 11.3m
                                    },
                                    new CurrencyRate {
                                        Code = "DEK",
                                        Rate = 8.3m
                                    }
                                }
                            },
                            new CurrencyDate {
                                Date = now.AddDays(-10),
                                Rates = new List<CurrencyRate> {
                                    new CurrencyRate {
                                        Code = "NOK",
                                        Rate = 15.3m
                                    },
                                    new CurrencyRate {
                                        Code = "SEK",
                                        Rate = 11.3m
                                    },
                                    new CurrencyRate {
                                        Code = "DEK",
                                        Rate = 8.3m
                                    }
                                }
                            }
                        }
                    },
                    new Currency {
                        Code = "NOK",
                        Rates = new List<CurrencyDate> {
                            new CurrencyDate {
                                Date = now,
                                Rates = new List<CurrencyRate> {
                                    new CurrencyRate {
                                        Code = "EUR",
                                        Rate = 0.1m
                                    },
                                    new CurrencyRate {
                                        Code = "SEK",
                                        Rate = 1.2m
                                    },
                                    new CurrencyRate {
                                        Code = "DEK",
                                        Rate = 0.8m
                                    }
                                }
                            },
                            new CurrencyDate {
                                Date = now.AddDays(-10),
                                Rates = new List<CurrencyRate> {
                                    new CurrencyRate {
                                        Code = "EUR",
                                        Rate = 0.1m
                                    },
                                    new CurrencyRate {
                                        Code = "SEK",
                                        Rate = 1.2m
                                    },
                                    new CurrencyRate {
                                        Code = "DEK",
                                        Rate = 0.8m
                                    }
                                }
                            }
                        }
                    }
                })
                .Result;

            Assert.IsTrue(isUpdateOk, "Updating all currencies failed");
        }
    }
}
