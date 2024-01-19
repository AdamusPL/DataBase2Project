using Jsos3.Groups.ViewModels.Models;

namespace Jsos3.Groups.ViewModels.Builders;

public class FilterViewModelBuilder
{
    private string _selectedSemester = string.Empty;
    private List<string> _semesters = [];

    public FilterViewModelBuilder WithSelectedSemester(string selectedSemester)
    {
        _selectedSemester = selectedSemester;
        return this;
    }

    public FilterViewModelBuilder WithSemesters(List<string> semesters)
    {
        _semesters = semesters;
        return this;
    }

    public FilterViewModel Build()
    {
        return new FilterViewModel
        {
            SelectedSemester = _selectedSemester,
            Semesters = _semesters
        };
    }
}
