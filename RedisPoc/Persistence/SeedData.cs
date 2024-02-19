using Microsoft.EntityFrameworkCore;
using RedisPoc.Entitites;

namespace RedisPoc.Persistence;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new DataContext(serviceProvider.GetRequiredService<DbContextOptions<DataContext>>());

        if (context.Users.Any())
        {
            return;
        }

        var users = new List<User>();
        for (int i = 0; i < 10000; i++)
        {
            users.Add(new User
            {
                DisplayName = $"Test User {i}",
                Email = $"testuser{i}@example.com",
                Password = $"TestPassword{i}"
            });
        }

        context.Users.AddRange(users);
        context.SaveChanges();
    }
}