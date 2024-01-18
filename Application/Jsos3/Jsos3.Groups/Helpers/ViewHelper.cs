using System.Numerics;

namespace Jsos3.Groups.Helpers;

public interface IViewHelper
{
    string DashWhenZero<T>(T number) where T : INumber<T>;
}

internal class ViewHelper : IViewHelper
{
    private const string EmptyCharacter = "-";

    public string DashWhenZero<T>(T number) where T : INumber<T>
    {
        return T.IsZero(number) ?
            EmptyCharacter :
            (number.ToString() ?? EmptyCharacter);
    }
}
