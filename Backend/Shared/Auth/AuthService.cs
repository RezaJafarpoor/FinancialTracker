using Backend.Features.CreateAccount;
using Backend.Features.Login;
using Backend.Features.ResetPassword;
using Backend.Shared.Domain;
using Backend.Shared.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Shared.Auth;

public class AuthService(ApplicationContext dbContext,
IPasswordHasher<User> passwordHasher, TokenProvider tokenProvider)
{
    public async Task<bool> CreateUser(CreateAccountDto dto)
    {
        var user = User.CreateUser(dto.UserName, dto.Email);
        var hashedPassword = passwordHasher.HashPassword(user, dto.Password);
        user.SetHashedPassword(hashedPassword);
        dbContext.Users.Add(user);
        return await dbContext.SaveChangesAsync() > 0;
    }


    public async Task<string> Login(LoginDto dto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);
        if (user is null)
        {
            // failed logic here
            return "false";
        }
        var hashedPassword = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (hashedPassword == PasswordVerificationResult.Failed)
        {
            // failed logic here

            return "false";
        }
        var token = tokenProvider.GenerateJwtToken(user.Id);
        return token;
    }

    public async Task<bool> ResetPassword(ResetDto dto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName == dto.UserName);
        if (user is null)
            return false;
        var checkOldPassword = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.OldPassword);
        if (checkOldPassword != PasswordVerificationResult.Success)
            return false;

        var newHashedPassword = passwordHasher.HashPassword(user, dto.NewPassword);
        user.SetHashedPassword(newHashedPassword);
        return await dbContext.SaveChangesAsync() > 0;
    }

}
