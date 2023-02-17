using System.Net;
using System.Text;
using System.Text.Json;
using Web.Domain;

namespace Web.Server.Middleware
{
    public class RequestHandler
    {
        private readonly RequestDelegate _next;
        private IHostEnvironment _environment;
        private ILogger<RequestHandler> _logger;
        public RequestHandler(RequestDelegate next, IHostEnvironment env, ILogger<RequestHandler> logger)
        {
            _next = next;
            _environment = env;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                var isLog = context.Request.Path != "/";
                if (isLog) 
                {
                    await LogRequest(context.Request);
                }
                await _next(context);
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                CommonResponse sysError = Error.Response(999).TryCast<CommonResponse>();
                var result = JsonSerializer.Serialize(sysError);
                await response.WriteAsync(result);
            }
        }
        private async Task LogRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableBuffering();
            request.Body.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            request.Body.Seek(0, SeekOrigin.Begin);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            var message = $"{request.Scheme}://{request.Host}{request.Path} {request.QueryString} {bodyAsText}";
            _logger.LogInformation(message);
        }
    }
}
