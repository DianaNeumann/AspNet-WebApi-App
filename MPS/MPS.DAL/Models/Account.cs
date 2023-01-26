using System.Text.Json.Serialization;
using MPS.DAL.Models.Tools;

namespace MPS.DAL.Models;

public class Account
{
    public Account(Guid id, string login, Role role)
    {
        Id = id;
        Login = login;
        PasswordHash = string.Empty;
        Role = role;
        ProcessedMessages = new List<Message>();
        Reports = new List<Report>();
    }

    public Account()
    {
    }

    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }
    [JsonIgnore]
    public virtual ICollection<Message> ProcessedMessages { get; set; }
    [JsonIgnore]
    public virtual ICollection<Report> Reports { get; set; }
}