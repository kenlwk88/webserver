using Newtonsoft.Json;
using Web.Domain.User.Common;

namespace Web.Domain.User
{
    public class GetUserApiResponse : CommonResponse
    {
        [JsonProperty("data", Order = int.MaxValue)]
        public List<UserModel> Data { get; set; }
    }
}
