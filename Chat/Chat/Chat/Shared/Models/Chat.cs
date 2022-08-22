namespace Chat.Shared.Models;

public class Chat : BaseEntity
{
    public string ChatName { get; set; }
    
    public ICollection<User> Users { get; set; }
}