using System;
using System.Threading.Tasks;
using ArvatoLibrary.Extensions;
using ArvatoLibrary.Services;

namespace ArvatoConsoleApplication {
    class Program {
        public static async Task Main(string[] args) {
            while (true) {
                string fromCurrenyCode = null;
                string toCurrencyCode = null;
                decimal? fromAmount = null;
                DateTime? currencyDate = null;


                Console.Clear();

                //Input from curreny code
                Console.Write("Input from curreny code: ");
                fromCurrenyCode = Console.ReadLine();

                Console.WriteLine();

                if (fromCurrenyCode.IsNullOrWhiteSpace()) {
                    Console.WriteLine("Invalid input: from currency code is missing");
                    Console.ReadKey();
                    continue;
                } else if (fromCurrenyCode.Length != 3) {
                    Console.WriteLine("Invalid input: from currency code has invalid format");
                    Console.ReadKey();
                    continue;
                }


                //Input to currency code
                Console.Write("Input to currency code: ");
                toCurrencyCode = Console.ReadLine();

                if (toCurrencyCode.IsNullOrWhiteSpace()) {
                    Console.WriteLine("Invalid input: to currency code is missing");
                    Console.ReadKey();
                    continue;
                } else if (toCurrencyCode.Length != 3) {
                    Console.WriteLine("Invalid input: to currency code has invalid format");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine();


                //Input currency date
                Console.Write("Input currency date(yyyy-MM-dd): ");
                currencyDate = Console.ReadLine().ToDateTime();

                Console.WriteLine();

                //Input amount in
                Console.Write("Input amount in " + fromCurrenyCode.ToUpper() + ": ");
                fromAmount = Console.ReadLine().ToDecimal();

                if (fromAmount == null) {
                    Console.WriteLine("Invalid input: from amount has invalid format/is not a number");
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("Loading...");


                //Converted amount
                decimal? convertedAmount = await new CurrencyExternalService().Convert(fromCurrenyCode, toCurrencyCode, fromAmount, currencyDate);

                Console.WriteLine();
                Console.WriteLine("Amount in " + toCurrencyCode + ": " + convertedAmount.Round());
                Console.WriteLine();


                //Exit
                Console.WriteLine("Hit any key to go again or ESC to exit");

                if(Console.ReadKey().Key == ConsoleKey.Escape) {
                    break;
                }
            }
        }
    }
}
