using Jsos3.Shared.Models;

namespace Jsos3.Shared.Logic;

public interface ITranslationService
{
    string Translate(string key);
    string Translate(DayOfWeek dayOfWeek);
    string Translate(GroupType groupType);
    string Translate(Regularity regularity);
}
