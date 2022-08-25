namespace Chat.Shared.Models.UserAndChatDTOS;

public class MessageDTO : BaseEntity
{
    public string Content { get; set; }
    
    public string PreviousMessage { get; set; }
    
    public int User { get; set; }

    public int Chat { get; set; }
    
    public DateTime DateCreated { get; set; }
}