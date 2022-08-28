using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Chat.Shared.Models;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Pass { get; set; }
    
    public ICollection<Chat> Chats { get; set; }
}