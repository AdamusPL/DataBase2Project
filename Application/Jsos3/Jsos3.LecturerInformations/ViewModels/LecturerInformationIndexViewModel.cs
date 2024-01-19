using Jsos3.LecturerInformations.Models;

namespace Jsos3.LecturerInformations.ViewModels;

public class LecturerInformationIndexViewModel
{
    public required List<LecturerDataDto> LecturersData {  get; set; }
    public required int PageNumber {  get; set; }
    public required int NumberOfPages {  get; set; }
}
