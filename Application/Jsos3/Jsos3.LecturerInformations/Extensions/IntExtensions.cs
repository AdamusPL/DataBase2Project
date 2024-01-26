namespace Jsos3.LecturerInformations.Extensions;

internal static class IntExtensions
{
    public static int Clamp(this int? value, int min, int max)
    {
        if (!value.HasValue || value < min || min > max)
        {
            return min;
        }

        if (value > max)
        {
            return max;
        }

        return value.Value;
    }
}
