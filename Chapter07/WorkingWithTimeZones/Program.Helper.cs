using System.Collections.ObjectModel; // To use ReadOnlyCollection<T>

partial class Program
{
    private static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"*** {title} ***");
        ForegroundColor = previousColor;
    }

    private static void OutputTimeZones()
    {
        // Get the time zones registered with the OS
        ReadOnlyCollection<TimeZoneInfo> zones = TimeZoneInfo.GetSystemTimeZones();
        WriteLine($"*** {zones.Count} time zones:");
        // Order the time zones by Id instead of DisplayName
        foreach (TimeZoneInfo zone in zones.OrderBy(z => z.Id))
        {
            WriteLine($"{zone.Id}");
        }
        WriteLine();
    }

    private static void OutputDateTime(DateTime dateTime, string title)
    {
        SectionTitle(title);
        WriteLine($"Value: {dateTime}");
        WriteLine($"Kind: {dateTime.Kind}");
        WriteLine($"IsDaylightSavingTime: {dateTime.IsDaylightSavingTime()}");
        WriteLine($"ToLocalTime: {dateTime.ToLocalTime()}");
        WriteLine($"ToUniversalTime: {dateTime.ToUniversalTime()}");
        WriteLine();
    }

    private static void OutputTimeZone(TimeZoneInfo zone, string title)
    {
        SectionTitle(title);
        WriteLine($"Id: {zone.Id}");
        WriteLine($"IsDaylightSavingTime(DateTime.Now): {zone.IsDaylightSavingTime(DateTime.Now)}");
        WriteLine($"StandardName: {zone.StandardName}");
        WriteLine($"DaylightName: {zone.DaylightName}");
        WriteLine($"BaseUtcOffset: {zone.BaseUtcOffset}");
        WriteLine();
    }

    private static string GetCurrentZoneName(TimeZoneInfo zone, DateTime when)
    {
        // Time zone names change if Daylight Saving time is active
        // e.g. GTM Standard Time becomes GMT Summer Time
        return zone.IsDaylightSavingTime(when) ? zone.DaylightName : zone.StandardName;
    }
}