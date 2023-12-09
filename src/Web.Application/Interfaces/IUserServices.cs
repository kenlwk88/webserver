using Web.Domain.User;
using Web.Domain.User.Common;

namespace Web.Application
{
    public interface IUserServices
    {
        Task<GetUserApiResponse> Get(ApplyFilter filter);
        Task<RegisterUserApiResponse> Register(RegisterUserApiRequest request);
        Task<UpdateUserApiResponse> Update(UpdateUserApiRequest request);
        Task<DeleteUserApiResponse> Delete(DeleteUserApiRequest request);
    }
}
