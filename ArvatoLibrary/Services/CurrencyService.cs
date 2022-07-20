using ArvatoLibrary.DataModels;
using ArvatoLibrary.Extensions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ArvatoLibrary.Services {
    public class CurrencyService : BaseService {
        public async Task<Currency> Get(string currencyCode) {
            using (DataBaseContext context = new DataBaseContext()) {
                return await context.Currencies
                    .Where(currency =>
                        currency.Code == currencyCode
                    ).Include(currency => currency.Rates)
                    .ThenInclude(currencyDate => currencyDate.Rates)
                    .FirstOrDefaultAsync();
            }
        }

        public async Task<bool> Save(Currency currency) {
            if (currency == null) {
                return false;
            }
            return await Save(new List<Currency> { currency });
        }

        public async Task<bool> Save(List<Currency> currencies) {
            if (currencies == null) {
                return false;
            }

            using (DataBaseContext context = new DataBaseContext()) {
                foreach (Currency newCurrency in currencies) {
                    if (newCurrency.Code.IsNullOrWhiteSpace()) {
                        continue;
                    }

                    newCurrency.Code = newCurrency.Code.ToUpper();

                    Currency existingCurrency = await context.Currencies.FirstOrDefaultAsync(existingCurrency => existingCurrency.Code == newCurrency.Code);

                    if (existingCurrency != null) {
                        existingCurrency.Rates = newCurrency.Rates;
                        context.Currencies.Update(existingCurrency);
                    } else {
                        context.Currencies.Add(newCurrency);
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
