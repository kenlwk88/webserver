using Web.Domain.User.Common;

namespace Web.Domain.User
{
    public class GetUserApiResponse : CommonResponse
    {
        public List<UserModel> data { get; set; }
    }
}
