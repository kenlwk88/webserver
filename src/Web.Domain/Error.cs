using Newtonsoft.Json;

namespace Web.Domain
{
    public static class Error
    {
        private static readonly Dictionary<string, string> _ErrorList = new Dictionary<string, string>()
        {
            {"101" , "invalid_arguments"},
            {"201", "invalid_user"},
            {"202", "invalid_user_full_name"},
            {"203", "invalid_user_email"},
            {"204", "invalid_user_phone"},
            {"205", "invalid_user_age"},
            {"801", "operation_failed"},
            {"999", "system_error" }
        };
        public static object Response(int errorCode)
        {
            return new
            {
                code = errorCode,
                message = _ErrorList.GetValueOrDefault(errorCode.ToString(), "unknown_error")
            };
        }
        public static T TryCast<T>(this object obj) where T : class
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
