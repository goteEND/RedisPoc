using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using RedisPoc.Entitites;
using RedisPoc.Repositories.Interfaces;

namespace RedisPoc.Services
{
    public class UserService : IUserService
    {
        private readonly string _cacheAllKey = "users";
        private readonly IUserRepository _userRepository;
        private readonly IDistributedCache _cache;

        public UserService(IUserRepository userRepository, IDistributedCache cache)
        {
            _userRepository = userRepository;
            _cache = cache;
        }

        public async Task<User> Get(int id)
        {
            var userFromCache = await _cache.GetStringAsync(id.ToString());

            if (string.IsNullOrEmpty(userFromCache))
            {
                var userFromDb = await _userRepository.Get(id);
                await _cache.SetStringAsync(id.ToString(), JsonSerializer.Serialize(userFromDb));
                return userFromDb;
            }

            return JsonSerializer.Deserialize<User>(userFromCache);
        }

        public async Task<List<User>> Get()
        {
            var usersFromCache = await _cache.GetStringAsync(_cacheAllKey);

            if (string.IsNullOrEmpty(usersFromCache))
            {
                var usersFromDb = await _userRepository.Get();
                await _cache.SetStringAsync(_cacheAllKey, JsonSerializer.Serialize(usersFromDb));
                return usersFromDb;
            }

            return JsonSerializer.Deserialize<List<User>>(usersFromCache);
        }

        public async Task Create(User user)
        {
            await _userRepository.Create(user);

            await _cache.RemoveAsync(_cacheAllKey);
        }

        public async Task Update(User user)
        {
            await _userRepository.Update(user);

            await _cache.RemoveAsync(user.Id.ToString());
            await _cache.RemoveAsync(_cacheAllKey);
        }

        public async Task Delete(int id)
        {
            await _userRepository.Delete(id);

            await _cache.RemoveAsync(id.ToString());
            await _cache.RemoveAsync(_cacheAllKey);
        }

    }
}