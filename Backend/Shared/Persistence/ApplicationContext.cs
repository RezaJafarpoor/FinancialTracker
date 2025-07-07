using Backend.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace Backend.Shared.Persistence;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{

    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

}
