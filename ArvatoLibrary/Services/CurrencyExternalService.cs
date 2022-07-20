using ArvatoLibrary.DataModels;
using ArvatoLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArvatoLibrary.Services {
    public class CurrencyExternalService : BaseService {

        public async Task<decimal?> Convert(Currency fromCurrency, 
            Currency toCurrency,
            decimal? amount,
            DateTime? date = null) {

            return await Convert(fromCurrency?.Code, toCurrency?.Code, amount, date);
        }

        public async Task<decimal?> Convert(string fromCurrencyCode,
            string toCurrencyCode,
            decimal? amount,
            DateTime? date = null) {

            if (fromCurrencyCode.IsNullOrWhiteSpace()) {
                throw new Exception("From currency code is NULL");
            }

            if (toCurrencyCode.IsNullOrWhiteSpace() ||
                amount == null) {
                return null;
            }

            Currency fromCurrency = await FixerApiClient.Get(fromCurrencyCode, toCurrencyCode, date);

            if (fromCurrency?.LatestRate?.Rates == null) {
                return null;
            }

            return fromCurrency.LatestRate.GetRate(toCurrencyCode) * amount;
        }

        public async Task<bool> UpdateAllCurrencies(DateTime? date = null, 
            List<Currency> currencies = null) {

            if (currencies == null) {
                currencies = await FixerApiClient.GetAll();

                if (currencies == null) {
                    return false;
                }

                List<Task> currenyGetTasks = new List<Task>();

                foreach (Currency currency in currencies) {
                    currenyGetTasks.Add(
                        FixerApiClient.Get(currency.Code, (string)null, date)
                            .ContinueWith((responseTask) => {
                                currency.Rates = responseTask?.Result?.Rates;
                            }));
                }

                await Task.WhenAll(currenyGetTasks);
            }

            await new CurrencyService().Save(currencies);

            return true;
        }
    }
}
