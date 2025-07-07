namespace Backend.Shared.Domain;

public class User
{
    public User() { }


    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public List<Transaction> Transactions { get; set; } = [];


    private User(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }

    public static User CreateUser(string userName, string email)
        => new(userName, email);
    public void SetHashedPassword(string hashPassword)
    {
        PasswordHash = hashPassword;
    }

}
