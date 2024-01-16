using Jsos3.Shared.Models;

namespace Jsos3.Shared.Logic;

internal class PolishTranslationService : ITranslationService
{
    public string Translate(string key)
    {
        throw new NotImplementedException();
    }

    public string Translate(DayOfWeek dayOfWeek) => dayOfWeek switch
    {
        DayOfWeek.Monday => "Poniedziałek",
        DayOfWeek.Tuesday => "Wtorek",
        DayOfWeek.Wednesday => "Środa",
        DayOfWeek.Thursday => "Czwartek",
        DayOfWeek.Friday => "Piątek",
        DayOfWeek.Saturday => "Sobota",
        DayOfWeek.Sunday => "Niedziela",
        _ => throw new ArgumentOutOfRangeException(nameof(dayOfWeek), dayOfWeek, null)
    };

    public string Translate(GroupType groupType) => groupType switch
    {
        GroupType.Lecture => "Wykład",
        GroupType.Laboratory => "Laboratorium",
        GroupType.Practice => "Ćwiczenia",
        GroupType.Project => "Projekt",
        _ => throw new ArgumentOutOfRangeException(nameof(groupType), groupType, null)
    };

    public string Translate(Regularity regularity) => regularity switch
    {
        Regularity.Odd => "Tydzień nieparzysty",
        Regularity.Even => "Tydzień parzysty",
        Regularity.Every => "Co tydzień",
        _ => throw new ArgumentOutOfRangeException(nameof(regularity), regularity, null)
    };
}
