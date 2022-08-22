namespace Chat.Shared.Models;
public class Message : BaseEntity
{
    public string Content { get; set; }
    
    public User User { get; set; }

    public Chat Chat { get; set; }
    
    public DateTime DateCreated { get; set; }
}