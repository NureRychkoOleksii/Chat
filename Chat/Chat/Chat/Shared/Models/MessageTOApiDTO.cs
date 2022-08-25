using Chat.Shared.Models;

namespace Chat.Shared.Models;

public class MessageTOApiDTO : BaseEntity
{
    public string Content { get; set; }

    public string Chat { get; set; }

    public string User { get; set; }
    
}