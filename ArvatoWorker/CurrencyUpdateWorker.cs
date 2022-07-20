using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ArvatoLibrary.Extensions;
using ArvatoLibrary.Services;

namespace ArvatoWorker {
    public class CurrencyUpdateWorker : BackgroundService {
        private readonly ILogger<CurrencyUpdateWorker> _logger;

        public CurrencyUpdateWorker(ILogger<CurrencyUpdateWorker> logger) {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
            DateTime? lastUpdateTime = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json", true, false)
                   .Build()["UpdateTime"].ToTime();

            DateTime? updatedTime = null;
            
            if(lastUpdateTime == null) {
                throw new Exception("No update time configured in appSettings.json");
            }

            CurrencyExternalService currencyExternalService = new CurrencyExternalService();

            while (!stoppingToken.IsCancellationRequested) {
                try {
                    DateTime now = DateTime.Now;

                    if((lastUpdateTime == null || lastUpdateTime < now) &&
                        lastUpdateTime.Value.Hour == now.Hour && lastUpdateTime.Value.Minute == now.Minute) {

                        await currencyExternalService.UpdateAllCurrencies();

                        updatedTime = now;
                    }
                } catch (Exception exception) {
                    _logger.LogError(exception, "Updating currencies failed: ");
                }


                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
