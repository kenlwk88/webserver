using AutoMapper;
using Core.DataAccess.SqlLite;
using Microsoft.Extensions.Logging;
using Web.Domain;
using Web.Domain.User;
using Web.Domain.User.Common;

namespace Web.Application
{
    public class UserServices : IUserServices
    {
        private readonly ILogger<UserServices> _logger;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public UserServices(ILogger<UserServices> logger, IUserRepo userRepo, IMapper mapper)
        {
            _logger= logger;
            _userRepo= userRepo;
            _mapper= mapper;
        }
        public async Task<GetUserApiResponse> Get(ApplyFilter filter)
        {
            GetUserApiResponse response = new();
            try
            {
                var users = await _userRepo.GetAll();

                #region Filter
                if (filter != null) 
                {
                    if (filter.username != null && filter.username.Trim().Length > 0)
                    {
                        users = users.Where(x => x.Username.ToLower().Contains(filter.username.ToLower())).OrderBy(y => y.Username).ToList();
                    }
                    if (filter.email != null && filter.email.Trim().Length > 0) 
                    {
                        users = users.Where(x => x.Email.ToLower().Contains(filter.email.ToLower())).OrderBy(y => y.Email).ToList();
                    }
                    if (filter.phone != null && filter.phone.Trim().Length > 0)
                    {
                        users = users.Where(y => y.Phone != null).Where(x => x.Phone.ToLower().Contains(filter.phone.ToLower())).OrderBy(y => y.Phone).ToList();
                    }
                }
                #endregion

                var result = _mapper.Map<List<UserModel>>(users);
                response.Data = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Get));
                return Error.Response(801).TryCast<GetUserApiResponse>();
            }
            return response;
        }
        public async Task<RegisterUserApiResponse> Register(RegisterUserApiRequest request)
        {
            RegisterUserApiResponse response = new();
            try
            {
                //Validation email is exist or not. Do not allow duplicate email
                var email = await _userRepo.GetByEmail(request.Email);
                if (email != null)
                    return Error.Response(202).TryCast<RegisterUserApiResponse>();

                //Validation username is exist or not. Do not allow duplicate username
                var user = await _userRepo.GetByUserName(request.UserName);
                if (user != null)
                    return Error.Response(203).TryCast<RegisterUserApiResponse>();

                //Create User
                await _userRepo.Create(_mapper.Map<UserDto>(request));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Register));
                return Error.Response(801).TryCast<RegisterUserApiResponse>();
            }
            return response;
        }
        public async Task<UpdateUserApiResponse> Update(UpdateUserApiRequest request)
        {
            UpdateUserApiResponse response = new();
            try
            {
                //Validate User
                var user = await _userRepo.GetById(request.Id);
                if(user == null)
                    return Error.Response(201).TryCast<UpdateUserApiResponse>();

                //Validate User Email
                if (request.Email.Trim().ToLower() != user.Email.Trim().ToLower()) 
                {
                    //Check duplicate email
                    var otherUser = await _userRepo.GetByEmail(request.Email);
                    if (otherUser != null)
                        return Error.Response(202).TryCast<UpdateUserApiResponse>();
                }

                //Validate Username
                if (request.UserName.Trim().ToLower() != user.Username.Trim().ToLower())
                {
                    //Check duplicate email
                    var otherUser = await _userRepo.GetByUserName(request.UserName);
                    if (otherUser != null)
                        return Error.Response(203).TryCast<UpdateUserApiResponse>();
                }

                //Update User
                await _userRepo.Update(_mapper.Map<UserDto>(request));
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
                //Validate User
                var user = await _userRepo.GetById(request.Id);
                if (user == null)
                    return Error.Response(201).TryCast<DeleteUserApiResponse>();

                //Delete User
                await _userRepo.Delete(request.Id);
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
