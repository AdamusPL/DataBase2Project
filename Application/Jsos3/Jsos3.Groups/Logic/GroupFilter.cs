using Jsos3.Groups.Infrastructure.Models;

namespace Jsos3.Groups.Logic;

internal interface IGroupFilter
{
    IEnumerable<Group> Filter(IEnumerable<Group> groups, string? courseName);
}

internal class GroupFilter : IGroupFilter
{
    public IEnumerable<Group> Filter(IEnumerable<Group> groups, string? courseName) =>
        groups.Where(x => x.Course.Contains(courseName ?? string.Empty, StringComparison.InvariantCultureIgnoreCase));
}
