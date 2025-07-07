using System.Runtime.InteropServices;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Backend.Shared.Domain;

public class User
{
    public User() { }


    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<Transaction> Transactions { get; set; } = [];


    private User(string userName, string email, string passwordhash)
    {
        UserName = userName;
        Email = email;
        PasswordHash = passwordhash;
    }

    public static User CreateUser(string userName, string email, string passwordhash)
        => new(userName, email, passwordhash);

}
