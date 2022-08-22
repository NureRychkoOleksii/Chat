namespace Chat.Server.Data.Models;

public class Chat : BaseEntity
{
    public string ChatName { get; set; }
    
    public ICollection<User> Users { get; set; }
}