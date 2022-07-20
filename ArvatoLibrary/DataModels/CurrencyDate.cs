using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ArvatoLibrary.Extensions;

namespace ArvatoLibrary.DataModels {
    public class CurrencyDate {

        public int? Sequence { get; set; }
        public DateTime? Date { get; set; }

        [NotMapped]
        private List<CurrencyRate> rate = null;

        public List<CurrencyRate> Rates {
            get {
                return rate;
            }
            set {
                rateIndex = null;
                rate = value;
            }
        }

        [NotMapped]

        private Dictionary<string, decimal?> rateIndex = null;
        [NotMapped]
        public Dictionary<string, decimal?> RatesIndex {
            get {
                if(Rates == null) {
                    return null;
                }

                if(rateIndex == null) {
                    rateIndex = new Dictionary<string, decimal?>();
                    foreach(CurrencyRate rate in Rates) {
                        if(rate?.Code == null || rate.Rate == null) {
                            continue;
                        }
                        rateIndex[rate.Code] = rate.Rate;
                    }
                }

                return rateIndex;
            }
        }

        public decimal? GetRate(string currencyCode) {
            if (RatesIndex == null || currencyCode.IsNullOrWhiteSpace()) {
                return null;
            }

            return RatesIndex.Get(currencyCode.ToUpper());
        }
    }
}
