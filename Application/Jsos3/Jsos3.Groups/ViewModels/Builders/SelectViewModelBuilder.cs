using Jsos3.Groups.ViewModels.Models;

namespace Jsos3.Groups.ViewModels.Builders;

public interface ISelectViewModelBuilder
{
    SelectViewModel BuildForSemesters(string selected, List<string> semesters);
}

public class SelectViewModelBuilder : ISelectViewModelBuilder
{
    public SelectViewModel BuildForSemesters(string selected, List<string> semesters) => new()
    {
        Label = "Semestr",
        Options = semesters.ToDictionary(x => x, x => x),
        QueryParameterName = "semesterId",
        Selected = selected
    };
}
