using Chat.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;

namespace Chat.Server.Data;

public class AppContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Models.Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public AppContext(DbContextOptions<AppContext> options) : base(options)
    {
        
    }
}