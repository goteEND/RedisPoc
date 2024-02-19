using RedisPoc.Entitites;

namespace RedisPoc.Services;

public interface IUserService
{
    public Task<User> Get(int id);
    public Task<List<User>> Get();
    public Task Create(User user);
    public Task Update(User user);
    public Task Delete(int id);


}