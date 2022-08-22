namespace Chat.Server.Data.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Pass { get; set; }
    
    public ICollection<Chat> Chats { get; set; }
}