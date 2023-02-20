namespace Web.Server.Filter.Security
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
    public class ApiKeyValidator : IApiKeyValidator
    {
        private readonly string _apiKey;
        public ApiKeyValidator(IConfiguration configuration)
        {
            _apiKey = configuration.GetValue<string>("ApiKey");
        }
        public bool IsValid(string apiKey)
        {
            if(apiKey == _apiKey)
                return true;
            return false;
        }
    }
}
