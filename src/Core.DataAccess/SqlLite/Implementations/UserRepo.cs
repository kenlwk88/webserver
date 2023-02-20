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
        public async Task Create(UserDto userDto)
        {
            try
            {
                var sql = @"INSERT INTO Users (FullName, Email, Phone, Age)
                            VALUES (@FullName, @Email, @Phone, @Age)";
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
                var sql = @"UPDATE Users SET FullName=@FullName, Email=@Email, Phone=@Phone, Age=@Age WHERE Id=@Id";
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
