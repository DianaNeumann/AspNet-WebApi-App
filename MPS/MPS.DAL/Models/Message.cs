using MPS.DAL.Models.Tools;

namespace MPS.DAL.Models;

public class Message
{
    public Message(Guid id, string content, MessageSource source)
    {
        Id = id;
        Content = content;
        ProcessedAccount = null;
        Type = MessageType.New;
        Source = source;
    }

    public Message()
    {
    }

    public Guid Id { get; set; }
    public string Content { get; set; }
    public virtual Account ProcessedAccount { get; set; }
    public virtual MessageType Type { get; set; }
    public virtual MessageSource Source { get; set; }
}