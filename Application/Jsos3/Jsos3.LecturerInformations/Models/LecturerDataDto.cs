namespace Jsos3.LecturerInformations.Models;

public class LecturerDataDto
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required List<string> Emails { get; set; }
    public required List<string> Phones { get; set; }
}
