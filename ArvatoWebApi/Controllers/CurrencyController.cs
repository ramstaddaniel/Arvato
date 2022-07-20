using ArvatoLibrary.DataModels;
using ArvatoLibrary.Services;
using ArvatoWebApi.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ArvatoWebApi.Controllers {
    [ApiController]
    public class CurrencyController : BaseController {

        private readonly ILogger<CurrencyController> _logger;

        public CurrencyController(ILogger<CurrencyController> logger) {
            _logger = logger;
        }

        [Route("api/currency/convert")]
        [HttpPost]
        public async Task<CurrencyConvertResponse> Convert(CurrencyConvertRequest request) {
            return new CurrencyConvertResponse {
                Amount = await new CurrencyExternalService().Convert(request.FromCurrencyCode, request.ToCurrencyCode, request.Amount, request.Date)
            };
        }

        [Route("api/currency/get")]
        [HttpPost]
        public async Task<CurrencyGetResponse> Get(CurrencyGetRequest request) {
            Currency currency = await new CurrencyService().Get(request.CurrencyCode);

            if (request.FromDate != null || request.ToDate != null) {
                currency.Rates = currency.Rates?.Where(rate =>
                        (request.FromDate == null || rate.Date >= request.FromDate) &&
                        (request.ToDate == null || rate.Date <= request.ToDate)
                    )
                    .ToList();
            }

            return new CurrencyGetResponse {
               Currency = currency
            };
        }
    }
}
