using Jsos3.Groups.Models;

namespace Jsos3.Groups.ViewModels.Models;

public class GroupViewModel
{
    public required bool ShowAbsenceLink { get; set; }
    public required GroupDto Group { get; set; }
}
