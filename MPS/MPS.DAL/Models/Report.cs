namespace MPS.DAL.Models;

public class Report
{
    public Report(Guid id, string link, Account account)
    {
        Id = id;
        Link = link;
        CreationDate = DateTime.Now;
        Account = account;
    }

    public Report()
    {
    }

    public Guid Id { get; set; }
    public string Link { get; set; }
    public DateTime CreationDate { get; set; }
    public virtual Account Account { get; set; }
}