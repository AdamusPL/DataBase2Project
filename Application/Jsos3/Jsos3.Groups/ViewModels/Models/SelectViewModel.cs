namespace Jsos3.Groups.ViewModels.Models;

public class SelectViewModel
{
    public string? Selected { get; set; }
    public required Dictionary<string, string> Options { get; set; }
    public required string QueryParameterName { get; set; }
    public required string RedirectUrl { get; set; }
}
