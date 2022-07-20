using System.Collections.Generic;

namespace ArvatoLibrary.DataModels.Fixer {
    public class GetResponse {
        public string @base { get; set; }
        public string date { get; set; }
        public Dictionary<string, decimal?> rates { get; set; } = new Dictionary<string, decimal?>();
        public bool? success { get; set; }
    }

    public class GetAllResponse {
        public Dictionary<string, string> symbols { get; set; } = new Dictionary<string, string>();
        public bool? success { get; set; }
    }
}
