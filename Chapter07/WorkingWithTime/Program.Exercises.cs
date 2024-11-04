partial class Program
{
    private static void SpecifyingDateTimeValues(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Specifying date and time values: {culture}");
        ConfigureConsole(culture: culture, overrideComputerCulture: overrideComputerCulture); // Defaults to en-US culture.

        WriteLine($"DateTime.MinValue:  {DateTime.MinValue}");
        WriteLine($"DateTime.MaxValue:  {DateTime.MaxValue}");
        WriteLine($"DateTime.UnixEpoch: {DateTime.UnixEpoch}");
        WriteLine($"DateTime.Now:       {DateTime.Now}");
        WriteLine($"DateTime.Today:     {DateTime.Today}");
        WriteLine($"DateTime.Today:     {DateTime.Today:d}");
        WriteLine($"DateTime.Today:     {DateTime.Today:D}");
        WriteLine();
    }

    private static void FormattingDateTimeValues(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Formatting date and time values: {culture}");
        ConfigureConsole(culture: culture, overrideComputerCulture: overrideComputerCulture); // Defaults to en-US culture

        DateTime xmas = new(year: 2024, month: 12, day: 25);
        WriteLine($"Christmas (default format): {xmas}");
        WriteLine($"Christmas (custom short format): {xmas:ddd d/M/yy}");
        WriteLine($"Christmas (custom long format): {xmas:dddd, dd MMMM yyyy}");
        WriteLine($"Christmas (standard long format): {xmas:D}");
        WriteLine($"Christmas (Sortable): {xmas:u}");
        WriteLine($"Christmas is in month {xmas.Month} of the year.");
        WriteLine($"Christmas is day {xmas.DayOfYear} of {xmas.Year}.");
        WriteLine($"Christmas {xmas.Year} is on a {xmas.DayOfWeek}.");
        WriteLine();
    }
}