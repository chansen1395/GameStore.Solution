using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GameStore.Models
{
  public class GameStoreContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerEmployee> CustomerEmployee { get; set; }
    public DbSet<CustomerGame> CustomerGame { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Game> Games { get; set; }
    

    public GameStoreContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}