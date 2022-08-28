using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Chat.Shared.Models;

public class Chat : BaseEntity
{
    public string ChatName { get; set; }

    public bool IsPrivate { get; set; } = true;
    
    [JsonIgnore]
    [IgnoreDataMember]
    public ICollection<User> Users { get; set; }
}