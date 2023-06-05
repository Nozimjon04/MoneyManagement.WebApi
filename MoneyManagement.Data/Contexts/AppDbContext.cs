using Microsoft.EntityFrameworkCore;
using MoneyManagement.Domain.Entities;

namespace MoneyManagement.Data.Contexts;

public class AppDbContext: DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{ }
	public DbSet<User> Users { get; set; }
	public DbSet<Wallet> Wallets { get; set; }
}
