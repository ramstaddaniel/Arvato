using System.Collections.Generic;
using System.Linq;

namespace ArvatoLibrary.DataModels {
    public class Currency {
        public int? Sequence { get; set; }
        public string Code { get; set; }
        public List<CurrencyDate> Rates { get; set; } = new List<CurrencyDate>();

        public CurrencyDate LatestRate {
            get {
                return Rates?.LastOrDefault();
            }
        }
    }
}
