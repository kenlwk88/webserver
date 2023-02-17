using System;
using Web.Domain;
using Web.Domain.User;
using Web.Domain.User.Common;

namespace Web.Application
{
    public interface IUserServices
    {
        Task<GetUserApiResponse> Get(ApplyFilter filter);
        Task<CreateUserApiResponse> Create(CreateUserApiRequest request);
        Task<UpdateUserApiResponse> Update(UpdateUserApiRequest request);
        Task<DeleteUserApiResponse> Delete(DeleteUserApiRequest request);
    }
}
