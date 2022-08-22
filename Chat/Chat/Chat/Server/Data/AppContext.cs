using Chat.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

namespace Chat.Server.Data;

public class AppContext : DbContext
{
    private DbSet<User> users;
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        
    }
}