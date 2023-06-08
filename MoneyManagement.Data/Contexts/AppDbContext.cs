using Microsoft.EntityFrameworkCore;
using MoneyManagement.Domain.Entities;

namespace MoneyManagement.Data.Contexts;

public class AppDbContext: DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{ }
	public DbSet<User> Users { get; set; }
	public DbSet<Goal> Goals { get; set; }
	public DbSet<Wallet> Wallets { get; set; }
	public DbSet<Report> Reports { get; set; }
	public DbSet<Reminder> Reminders { get; set; }
	public DbSet<Category> Categories { get; set; }
	public DbSet<Transaction> Transactions { get; set; }
}
