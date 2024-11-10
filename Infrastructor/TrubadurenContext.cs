using Microsoft.EntityFrameworkCore;
using Entities.Models;

namespace webapi.Infrastructor;

public class TrubadurenContext : DbContext
{
    public TrubadurenContext(DbContextOptions<TrubadurenContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Song> Songs { get; set; }
}