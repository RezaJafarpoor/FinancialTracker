using Backend.Shared.Domain;
using Microsoft.AspNetCore.Identity;

namespace Backend.Shared;

public class AuthService(IPasswordHasher<User> passwordHasher)
{
    public string CreateUser()
    {
        var user = new User();
        var hashedPassword = passwordHasher.HashPassword(user, "Reza1234");
        return hashedPassword;
    }
}
