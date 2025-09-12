namespace SingkoFItnessWebApi.Models;

public partial class Aiquery
{
    public int QueryId { get; set; }

    public int UserId { get; set; }

    public string QueryText { get; set; } = null!;

    public string? Airesponse { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}