using ArvatoLibrary.DataModels;
using ArvatoLibrary.DataModels.Fixer;
using System.Collections.Generic;
using System.Linq;
using ArvatoLibrary.Extensions;
using System.Net.Http;
using System;
using System.Threading.Tasks;

namespace ArvatoLibrary {
    public static class FixerApiClient {
        private readonly static string apiKey = "pfrHX357V7vEQZLJc762YmCrFBHcsdVS";
        private readonly static string baseUrl = "https://api.apilayer.com/fixer/";


        public async static Task<Currency> Get(string fromCurrencyCode, string toCurrencyCode = null, DateTime? date = null) {
            return await Get(fromCurrencyCode,
                !toCurrencyCode.IsNullOrWhiteSpace() ? new List<string> { toCurrencyCode } : null,
                date);
        }
        public async static Task<Currency> Get(string fromCurrencyCode, List<string> toCurrencyCodes = null, DateTime? date = null) {
            if(fromCurrencyCode.IsNullOrWhiteSpace()) {
                return null;
            }

            toCurrencyCodes ??= new List<string>();

            string toCurrencyCodesAsString = string.Join(",", toCurrencyCodes);
            GetResponse response = null;

            if (date != null) {
                string dateAsString = date.Value.ToString("yyyy-MM-dd");

                string cacheKey = fromCurrencyCode + "|" + toCurrencyCodesAsString + dateAsString;

                response = Cache.Get<GetResponse>(cacheKey);

                if (response == null) {
                    response = await DoHttpRequest<GetResponse>(baseUrl + dateAsString + "?base=" + fromCurrencyCode + "&symbols=" + toCurrencyCodesAsString, apiKey);
                }

                Cache.Add(cacheKey, response);
            } else {
                response = await DoHttpRequest<GetResponse>(baseUrl + "latest?base=" + fromCurrencyCode + "&symbols=" + toCurrencyCodesAsString, apiKey);
            }

            if(response == null) {
                return null;
            }

            return new Currency {
                Code = response.@base,
                Rates = new List<CurrencyDate> {
                        new CurrencyDate {
                        Date = response.date.ToDateTime(),
                        Rates = response.rates?.Select(rate => {
                            return new CurrencyRate {
                                Code = rate.Key,
                                Rate = rate.Value
                            };
                        })?.ToList()
                    }
                }
            };
        }
        
        public static async Task<List<Currency>> GetAll() {
            return (await DoHttpRequest<GetAllResponse>(baseUrl + "symbols", apiKey))?
                .symbols?
                .Select(symbolKeyValuePair => {
                    return new Currency {
                        Code = symbolKeyValuePair.Key
                    };
             })?.ToList();
        }

        private static async Task<T> DoHttpRequest<T>(string url, string apiKey) {
            using (HttpClient client = new HttpClient()) {
                client.DefaultRequestHeaders.Add("apiKey", apiKey);
                string response = await client.GetStringAsync(url);
                return response.ConvertFromJson<T>();
            }
        }
    }
}
    