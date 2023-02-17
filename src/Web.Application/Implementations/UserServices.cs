using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application;
using Web.Domain;
using Web.Domain.User;
using Web.Domain.User.Common;

namespace Web.Application
{
    public class UserServices : IUserServices
    {
        private readonly ILogger<UserServices> _logger;
        public UserServices(ILogger<UserServices> logger)
        {
            _logger= logger;
        }
        public async Task<GetUserApiResponse> Get(ApplyFilter filter)
        {
            GetUserApiResponse response = new();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Get));
                return Error.Response(801).TryCast<GetUserApiResponse>(); ;
            }
            return response;
        }
        public async Task<CreateUserApiResponse> Create(CreateUserApiRequest request)
        {
            CreateUserApiResponse response = new();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Create));
                return Error.Response(801).TryCast<CreateUserApiResponse>();
            }
            return response;
        }
        public async Task<UpdateUserApiResponse> Update(UpdateUserApiRequest request)
        {
            UpdateUserApiResponse response = new();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Update));
                return Error.Response(801).TryCast<UpdateUserApiResponse>();
            }
            return response;
        }
        public async Task<DeleteUserApiResponse> Delete(DeleteUserApiRequest request)
        {
            DeleteUserApiResponse response = new();
            try
            {

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Delete));
                return Error.Response(801).TryCast<DeleteUserApiResponse>();
            }
            return response;
        }
    }
}
