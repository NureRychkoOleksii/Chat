namespace Chat.Server.Data.Models;

public class Message : BaseEntity
{
    public string Content { get; set; }
    
    public User User { get; set; }

    public Chat Chat { get; set; }
}