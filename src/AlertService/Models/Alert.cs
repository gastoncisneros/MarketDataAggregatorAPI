namespace AlertService.Models;

public class Alert
{
    public Guid Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public decimal TargetPrice { get; set; }
    public string Condition { get; set; } = string.Empty; // "above" or "below"
    public DateTime CreatedAt { get; set; }
}
