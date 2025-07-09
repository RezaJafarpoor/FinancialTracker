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
    public async Task<Response<bool>> CreateUser(CreateAccountDto dto)
    {
        var duplicateEmailCheck = await dbContext.Users.AnyAsync(u => u.Email == dto.Email);
        if (duplicateEmailCheck)
            return Response<bool>.Failure("This email is already used");
        var user = User.CreateUser(dto.UserName, dto.Email);
        var hashedPassword = passwordHasher.HashPassword(user, dto.Password);
        user.SetHashedPassword(hashedPassword);
        dbContext.Users.Add(user);
        if (await dbContext.SaveChangesAsync() > 0)
            return Response<bool>.Success();
        return Response<bool>.Failure("Something went wrong with user creation");
    }


    public async Task<Response<string>> Login(LoginDto dto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null)
            return Response<string>.Failure("username or password is wrong");
        var hashedPassword = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (hashedPassword == PasswordVerificationResult.Failed)
            return Response<string>.Failure("username or password is wrong");

        var token = tokenProvider.GenerateJwtToken(user.Id);
        return Response<string>.Success(token);

    }

    public async Task<Response<bool>> ResetPassword(ResetDto dto)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user is null)
            return Response<bool>.Failure("username or password is wrong");
        var checkOldPassword = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.OldPassword);
        if (checkOldPassword != PasswordVerificationResult.Success)
            return Response<bool>.Failure("username or password is wrong");

        var newHashedPassword = passwordHasher.HashPassword(user, dto.NewPassword);
        user.SetHashedPassword(newHashedPassword);
        if (!(await dbContext.SaveChangesAsync() > 0))
            return Response<bool>.Failure("Something went wrong");
        return Response<bool>.Success();
    }

}
