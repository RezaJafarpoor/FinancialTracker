using Backend.Shared.Domain;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Backend.Shared;

public class AuthService(ApplicationContext dbContext, IPasswordHasher<User> passwordHasher)
{
    public async Task CreateUser(string userName, string email, string password)
    {
        var user = User.CreateUser(userName, email);
        var hashedPassword = passwordHasher.HashPassword(user, password);
        user.SetHashedPassword(hashedPassword);
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
    }
}
