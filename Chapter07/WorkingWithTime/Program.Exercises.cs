using System.Globalization; // To use CultureInfo.

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

    private static void DateAndTimeCalculations(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Date and time calculations: {culture}");
        ConfigureConsole(culture: culture, overrideComputerCulture: overrideComputerCulture);

        DateTime xmas = new(year: 2024, month: 12, day: 25);
        DateTime beforeXmas = xmas.Subtract(TimeSpan.FromDays(12));
        DateTime afterXmas = xmas.AddDays(12);
        WriteLine($"12 days before Christmas: {beforeXmas:d}");
        WriteLine($"12 days after Chrismas: {afterXmas:d}");
        
        TimeSpan untilXmas = xmas - DateTime.Now;
        WriteLine($"Now: {DateTime.Now}");
        WriteLine($"There are {untilXmas.Days} days and {untilXmas.Hours} hours until Christmas {xmas.Year}.");
        WriteLine($"There are {untilXmas.TotalHours:N0} hours until Christmas {xmas.Year}.");

        DateTime kidsWakeUp = new(year: 2024, month: 12, day: 25, hour: 6, minute: 30, second: 0);
        WriteLine($"Kids wake up: {kidsWakeUp}");
        WriteLine($"The kids woke me up at {kidsWakeUp.ToShortTimeString()}");

        WriteLine();
    }

    private static void MicrosecondsAndNanoseconds(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Milli-, micro-, and nanosecods: {culture}");
        ConfigureConsole(culture: culture, overrideComputerCulture: overrideComputerCulture);

        DateTime preciseTime = new(year: 2022, month: 11, day: 8, hour: 12, minute: 0, second: 0, millisecond: 6, microsecond: 999);
        WriteLine($"Millisecond: {preciseTime.Millisecond}, Microsecond: {preciseTime.Microsecond}, Nanoseconds: {preciseTime.Nanosecond}");
        
        preciseTime = DateTime.UtcNow;
        // Nanosecond value will be 0 to 900 in 100 nanosecond increments.
        WriteLine($"Millisecond: {preciseTime.Millisecond}, Microsecond: {preciseTime.Microsecond}, Nanoseconds: {preciseTime.Nanosecond}");

        WriteLine();
    }

    private static void GlobalizationWithDatesAndTimes(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Globalization with dates and times: {culture}");
        ConfigureConsole(culture: culture, overrideComputerCulture: overrideComputerCulture);

        // Same as Thread.CurrenThread.CurrentCulture.
        WriteLine($"Current culture: {CultureInfo.CurrentCulture.Name} -- Culture: {culture}");

        string textDate = "July 4, 2024";
        DateTime independenceDay = DateTime.Parse(textDate);
        WriteLine($"Text: {textDate}, DateTime: {independenceDay:d MMMM} -- Culture: {culture}");
        textDate = "7/4/2024";
        independenceDay = DateTime.Parse(textDate);
        WriteLine($"Text: {textDate}, DateTime: {independenceDay:d MMMM} -- Culture: {culture}");

        // Explicitly override the current culture by setting a provider.
        culture = "en-US";
        independenceDay = DateTime.Parse(textDate, provider: CultureInfo.GetCultureInfo(culture));
        WriteLine($"Text: {textDate}, DateTime: {independenceDay:d MMMM} -- Culture: {culture}");
        
        WriteLine();
    }

    private static void ComplexitiesOfDST(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Complexities of daylight saving time: {culture}");
        ConfigureConsole(culture: culture, overrideComputerCulture: overrideComputerCulture);

        for (int year = 2023; year <= 2028; year++)
        {
            Write($"{year} is a leap year: {DateTime.IsLeapYear(year)}.  ");
            WriteLine($"There are {DateTime.DaysInMonth(year: year, month: 2)} days in February {year}.");
        }

        DateTime xmas = new(year: 2024, month: 12, day: 25);
        string textDate = "July 4, 2024";
        DateTime independenceDay = DateTime.Parse(textDate);
        WriteLine($"Is Christmas daylight saving time? {xmas.IsDaylightSavingTime()}");
        WriteLine($"Is July 4th daylight saving time? {independenceDay.IsDaylightSavingTime()}");

        WriteLine();
    }

    private static void LocalizingDayOfWeekEnum(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Localizing the DayOfWeek enum: {culture}");
        ConfigureConsole(culture: culture, overrideComputerCulture: overrideComputerCulture);

        // DayOfWeek is not localized to Danish ("da-DK")
        WriteLine($"Culture: {Thread.CurrentThread.CurrentCulture.NativeName}, DayOfWeek: {DateTime.Now.DayOfWeek}");

        // Use dddd format code to get day of week localized.
        WriteLine($"Culture: {Thread.CurrentThread.CurrentCulture.NativeName}, DayOfWeek: {DateTime.Now:dddd}");

        // Use GetDayName method to get day of week localized.
        WriteLine($"Culture: {Thread.CurrentThread.CurrentCulture.NativeName}, DayOfWeek: {DateTimeFormatInfo.CurrentInfo.GetDayName(DateTime.Now.DayOfWeek)}");

        WriteLine();
    }

    private static void WorkingWithOnlyDateOrTime(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Working with only a date or time: {culture}");

        DateOnly party = new(year: 2024, month: 11, day: 12);
        WriteLine($"The .NET 9 release party in on {party.ToLongDateString()}.");

        TimeOnly starts = new(hour: 11, minute: 30);
        WriteLine($"The party starts at {starts}.");

        DateTime calendarEntry = party.ToDateTime(starts);
        WriteLine($"Add to your calendar: {calendarEntry}.");

        WriteLine();
    }

    private static void WorkingWithDateTimeFormats(string culture = "en-US", bool overrideComputerCulture = true)
    {
        SectionTitle($"Working with date/time formats: {culture}");
        ConfigureConsole(culture, overrideComputerCulture);

        DateTimeFormatInfo dtfi = DateTimeFormatInfo.CurrentInfo;
        // Or use Thread.CurrentThread.CurrentCulture.DateTimeFormat.

        WriteLine($"Date separator: {dtfi.DateSeparator}");
        WriteLine($"Time separator: {dtfi.TimeSeparator}");
        WriteLine($"Long date pattern: {dtfi.LongDatePattern}");
        WriteLine($"Short date pattern: {dtfi.ShortDatePattern}");
        WriteLine($"Long time pattern: {dtfi.LongTimePattern}");
        WriteLine($"Short time pattern: {dtfi.ShortTimePattern}");
        Write("Day Names:");
        for (int i = 0; i < dtfi.DayNames.Length - 1; i++)
        {
            Write($"  {dtfi.GetDayName((DayOfWeek)i)}");
        }
        WriteLine();
        Write("Month names:");
        for (int i = 1; i < dtfi.MonthNames.Length; i++)
        {
            Write($"  {dtfi.GetMonthName(i)}");
        }
        WriteLine();
        WriteLine();
    }
}