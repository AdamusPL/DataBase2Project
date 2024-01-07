using Jsos3.Groups.ViewModels.Models;

namespace Jsos3.Groups.ViewModels.Builders;

public class SelectViewModelBuilder
{
    public SelectViewModel BuildForSemesters(string selected, List<string> semesters)
    {
        return new()
        {
            Options = semesters.ToDictionary(x => x, x => x),
            QueryParameterName = "semesterId",
            RedirectUrl = "/Student",
            Selected = selected
        };
    }
}
