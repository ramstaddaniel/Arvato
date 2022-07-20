using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace ArvatoLibrary.DataModels {
    public class DoHttpRequest {
        public string Url { get; set; }
        public string Content { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public HttpMethod Method { get; set; } = HttpMethod.Post;
        public string ProxyUrl { get; set; }
        public string ApiKey { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ContentType { get; set; }
        public string AcceptType { get; set; }
        public string CertificateThumbprint { get; set; }
        public bool UsePreAuthenticate { get; set; } = true;
        public Func<string> RefreshTokenDelegate { get; set; }
        public Action<string> LogDebugDelegate { get; set; }
        public int? Timeout { get; set; }
    }

    public class DoHttpResponse {
        public string Content { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
    }
}
