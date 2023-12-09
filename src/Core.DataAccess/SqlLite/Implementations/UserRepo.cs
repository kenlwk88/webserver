using Microsoft.Extensions.Logging;

namespace Core.DataAccess.SqlLite
{
    public class UserRepo : IUserRepo
    {
        private readonly ILogger<UserRepo> _logger;
        private readonly DbContext _dbContext;
        public UserRepo(ILogger<UserRepo> logger, DbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            try
            {
                var sql = @"SELECT * FROM Users";
                var result = await _dbContext.QueryAsync<UserDto>(sql);
                return result;
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message, nameof(GetAll));
                return Enumerable.Empty<UserDto>();
            }
        }
        public async Task<UserDto> GetById(int id)
        {
            try
            {
                var sql = @$"SELECT * FROM Users Where Id = {id}";
                var result = await _dbContext.QueryAsync<UserDto>(sql);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(GetById));
                return null;
            }
        }
        public async Task<UserDto> GetByEmail(string email)
        {
            try
            {
                var sql = @$"SELECT * FROM Users Where Lower(Email) = '{email.Trim().ToLower()}'";
                var result = await _dbContext.QueryAsync<UserDto>(sql);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(GetByEmail));
                return null;
            }
        }
        public async Task<UserDto> GetByUserName(string userName)
        {
            try
            {
                var sql = @$"SELECT * FROM Users Where Lower(Username) = '{userName.Trim().ToLower()}'";
                var result = await _dbContext.QueryAsync<UserDto>(sql);
                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(GetByEmail));
                return null;
            }
        }
        public async Task Create(UserDto userDto)
        {
            try
            {
                var sql = @"INSERT INTO Users (Username, Email, Phone, SkillSets, Hobby)
                            VALUES (@Username, @Email, @Phone, @SkillSets, @Hobby)";
                await _dbContext.ExecuteAsync(sql, userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Create));
            }
        }
        public async Task Update(UserDto userDto)
        {
            try
            {
                var sql = @"UPDATE Users SET Username=@Username, Email=@Email, Phone=@Phone, SkillSets=@SkillSets, Hobby=@Hobby WHERE Id=@Id";
                await _dbContext.ExecuteAsync(sql, userDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Create));
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                var sql = @$"DELETE FROM Users WHERE Id = {id}";
                await _dbContext.ExecuteAsync(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, nameof(Create));
            }
        }
    }
}
