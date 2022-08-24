namespace Chat.Shared.Models.UserAndChatDTOS;

public class UserDTO : BaseEntity
{
    public string Name { get; set; }
    
    public ICollection<int> ChatsId { get; set; }
}