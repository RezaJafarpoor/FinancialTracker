using Backend.Features.CreateAccount;
using Backend.Features.Login;
using Backend.Shared.Domain;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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


    public async Task Login(LoginDto dto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);
        if (user is null)
        {
            // failed logic here
            return;
        }
        var hashedPassword = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (hashedPassword == PasswordVerificationResult.Failed)
        {
            // failed logic here

            return;
        }
        // generate token logic here
        return;
    }


}
