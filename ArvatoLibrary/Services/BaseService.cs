using Microsoft.Extensions.Logging;

namespace ArvatoLibrary.Services {
    public class BaseService {
        protected ILogger logger = null;

        public BaseService() {
            logger = new LoggerFactory().CreateLogger(GetType().ToString());
        }
    }
}
