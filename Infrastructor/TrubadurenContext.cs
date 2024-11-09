using Microsoft.EntityFrameworkCore;
using webapi.models;

namespace webapi.infrastuctor;

public class TrubadurenContext : DbContext
{
    public TrubadurenContext(DbContextOptions<TrubadurenContext> options)
    : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Song> Songs { get; set; }
}