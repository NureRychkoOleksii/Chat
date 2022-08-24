namespace Chat.Shared.Models.UserAndChatDTOS;

public class MessageDTO
{
    public string Content { get; set; }
    
    public UserDTO User { get; set; }

    public ChatDTO Chat { get; set; }
    
    public DateTime DateCreated { get; set; }
}