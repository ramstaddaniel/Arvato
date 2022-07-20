using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ArvatoLibrary.Extensions {
    public static class Extensions {
        private readonly static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.None
        };

        public static string ConvertToJson(this object @object, JsonSerializerSettings settings = null) {
            if (@object == null) {
                return null;
            }
            settings ??= jsonSerializerSettings;

            Type type = @object.GetType();
            if (type == typeof(string)) {
                return (string)@object;
            }
            return JsonConvert.SerializeObject(@object, settings);
        }
        public static T ConvertFromJson<T>(this string stringValue) {
            if (stringValue.IsNullOrWhiteSpace()) {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(stringValue);
        }

        public static bool IsNullOrWhiteSpace(this string stringValue) {
            return string.IsNullOrWhiteSpace(stringValue);
        }

        public static DateTime? ToDateTime(this string stringValue) {
            if (stringValue.IsNullOrWhiteSpace()) {
                return null;
            }

            if (DateTime.TryParse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)) {
                return date;
            }

            return null;
        }

        public static DateTime? ToTime(this string stringValue) {
            if (stringValue.IsNullOrWhiteSpace()) {
                return null;
            }

            if (DateTime.TryParseExact(stringValue, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)) {
                return date;
            }

            return null;
        }

        public static decimal? ToDecimal(this string stringValue) {
            if (stringValue.IsNullOrWhiteSpace()) {
                return null;
            }

            if (decimal.TryParse(stringValue, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalValue)) {
                return decimalValue;
            }

            return null;
        }

        public static decimal? Get(this Dictionary<string, decimal?> dictionary, string key) {
            if(dictionary == null || key.IsNullOrWhiteSpace()) {
                return null;
            }

            if(dictionary.TryGetValue(key, out decimal? decimalValue)) {
                return decimalValue;
            }

            return null;
        }

        public static decimal? Round(this decimal? decimalValue, int? decimals = 2) {
            if(decimalValue == null) {
                return null;
            }

            return decimal.Round(decimalValue.Value, decimals.GetValueOrDefault());
        }

    }
}
