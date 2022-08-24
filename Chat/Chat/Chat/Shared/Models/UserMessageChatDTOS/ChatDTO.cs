namespace Chat.Shared.Models.UserAndChatDTOS;

public class ChatDTO : BaseEntity
{
    public string ChatName { get; set; }
    
    public bool IsPrivate { get; set; }

    public ICollection<int> UsersId { get; set; }
}