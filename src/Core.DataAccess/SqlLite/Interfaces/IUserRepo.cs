namespace Core.DataAccess.SqlLite
{
    public interface IUserRepo
    {
        Task<IEnumerable<UserDto>> GetAll();
        Task<UserDto> GetById(int id);
        Task<UserDto> GetByEmail(string email);
        Task<UserDto> GetByUserName(string userName);
        Task Create(UserDto userDto);
        Task Update(UserDto userDto);
        Task Delete(int id);
    }
}
