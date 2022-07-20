using ArvatoLibrary.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArvatoWebApi.Messages {
    public class CurrencyConvertRequest {
        public string FromCurrencyCode { get; set; }
        public string ToCurrencyCode { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? Date { get; set; }
    }

    public class CurrencyConvertResponse {
        public decimal? Amount { get; set; }
    }

    public class CurrencyGetRequest {
        public string CurrencyCode { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class CurrencyGetResponse {
        public Currency Currency { get; set; }
    }
}
