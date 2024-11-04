partial class Program
{
    private static void SpecifyingDateTimeValues(string culture = "en-US", bool overrideComputerCulture = true)
    {
        ConfigureConsole(culture: culture, overrideComputerCulture: overrideComputerCulture); // Defaults to en-US culture.
        SectionTitle($"Specifying date and time values: {culture}");
        WriteLine($"DateTime.MinValue:  {DateTime.MinValue}");
        WriteLine($"DateTime.MaxValue:  {DateTime.MaxValue}");
        WriteLine($"DateTime.UnixEpoch: {DateTime.UnixEpoch}");
        WriteLine($"DateTime.Now:       {DateTime.Now}");
        WriteLine($"DateTime.Today:     {DateTime.Today}");
        WriteLine($"DateTime.Today:     {DateTime.Today:d}");
        WriteLine($"DateTime.Today:     {DateTime.Today:D}");
    }
}