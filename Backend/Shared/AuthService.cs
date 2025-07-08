using Backend.Features.CreateAccount;
using Backend.Shared.Domain;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Backend.Shared;

public class AuthService(ApplicationContext dbContext, IPasswordHasher<User> passwordHasher)
{
    public async Task CreateUser(CreateAccountDto dto)
    {
        var user = User.CreateUser(dto.UserName, dto.Email);
        var hashedPassword = passwordHasher.HashPassword(user, dto.Password);
        user.SetHashedPassword(hashedPassword);
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
    }


}
