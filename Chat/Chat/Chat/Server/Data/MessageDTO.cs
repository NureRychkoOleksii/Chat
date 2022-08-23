using Chat.Shared.Models;

namespace Chat.Server.Data;

public class MessageDTO
{
    public string Content { get; set; }

    public Shared.Models.Chat Chat { get; set; }

    public User User { get; set; }
    
}