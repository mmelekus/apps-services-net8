using Humanizer; // To use common Humanizer extension methods.
using Humanizer.Inflections; // To use Vocabularies.
using System.Globalization;
using System.Runtime.CompilerServices; // To use CultureInfo.

using HumanizingData;
using System.Security.Cryptography.X509Certificates;

partial class Program
{
    private static void ConfigureConsole(string culture = "en-US")
    {
        // To enable special characters like ... (elipses) as a single character.
        OutputEncoding = System.Text.Encoding.UTF8;
        Thread t = Thread.CurrentThread;
        t.CurrentCulture = CultureInfo.GetCultureInfo(culture);
        t.CurrentUICulture = t.CurrentCulture;
        WriteLine($"Current culture: {t.CurrentCulture.DisplayName}");
        WriteLine();
    }
    private static void OutputCasings(string original)
    {
        WriteLine($"Original casing: {original}");
        WriteLine($"Lower casing: {original.Transform(To.LowerCase)}");
        WriteLine($"Upper casing: {original.Transform(To.UpperCase)}");
        WriteLine($"Title casing: {original.Transform(To.TitleCase)}");
        WriteLine($"Sentence casing: {original.Transform(To.SentenceCase)}");
        WriteLine($"Lower, then Sentence casing: {original.Transform(To.LowerCase, To.SentenceCase)}");
        WriteLine();
    }

    private static void OutputSpacingAndDashes()
    {
        string ugly = "ERROR_MESSAGE_FROM_SERVICE";
        
        WriteLine($"Original string: {ugly}");
        WriteLine($"Humanized: {ugly.Humanize()}");
        WriteLine($"Humanized, lower case: {ugly.Humanize(LetterCasing.LowerCase)}"); // LetterCasing is legacy and will be removed in the future.
        WriteLine($"Transformed (lower case, then sentence case): {ugly.Transform(To.LowerCase, To.SentenceCase)}"); // Use Transform for casing instead.
        WriteLine($"Humanized, Transformed (lower case, then sentence case): {ugly.Humanize().Transform(To.LowerCase, To.SentenceCase)}");
    }

    private static void OutputEnumNames()
    {
        var favoriteAncientWonder = WondersOfTheAncientWorld.StatusOfZeusAtOlympia;
        
        WriteLine($"Raw enum value name: {favoriteAncientWonder}");
        WriteLine($"Humanized: {favoriteAncientWonder.Humanize()}");
        WriteLine($"Humanized, then Titleized: {favoriteAncientWonder.Humanize().Titleize()}");
        WriteLine($"Truncated to 8 characters: {favoriteAncientWonder.ToString().Truncate(length: 8)}");
        WriteLine($"Kebaberized: {favoriteAncientWonder.ToString().Kebaberize()}");
    }

    private static void NumberFormatting()
    {
        Vocabularies.Default.AddIrregular("biceps", "bicepses");
        Vocabularies.Default.AddIrregular("attorney general", "attorneys general");

        int number = 123;
        WriteLine($"Original number: {number}");
        WriteLine($"Roman: {number.ToRoman()}");
        WriteLine($"Words: {number.ToWords()}");
        WriteLine($"Ordinal words: {number.ToOrdinalWords()}");
        WriteLine();

        string[] things = { "fox", "person", "sheep", "apple", "goose", "oasis", "potato", "die", "dwarf", "attorney general", "biceps" };
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j < things.Length; j++)
            {
                Write(things[j].ToQuantity(i, ShowQuantityAs.Words));
                if (j < things.Length - 1) { Write(", " ); }
            }
            WriteLine();
        }
        WriteLine();

        int thousands = 12345;
        int millions = 123456789;
        WriteLine($"Original: {thousands}, Metric: About {thousands.ToMetric(decimals: 0)}");
        WriteLine($"Original: {thousands}, Metric: {thousands.ToMetric(MetricNumeralFormats.WithSpace | MetricNumeralFormats.UseShortScaleWord, decimals: 0)}");
        WriteLine($"Original: {millions}, Metric: {millions.ToMetric(decimals: 1)}");
    }

    private static void DateTimeFormatting()
    {
        DateTimeOffset now = DateTimeOffset.Now;

        // By default, all Humanizer comparisons are to Now (UTC).
        WriteLine($"Now (UTC): {now}");
        WriteLine($"Add 3 hours, Humanized: {now.AddHours(3).Humanize()}");
        WriteLine($"Add 3 hours and 1 minute, Humanized: {now.AddHours(3).AddMinutes(1).Humanize()}");
        WriteLine($"Subtract 3 hours, Humanized: {now.AddHours(-3).Humanize()}");
        WriteLine($"Add 24 hours, Humanized: {now.AddHours(24)}");
        WriteLine($"Add 25 hours, Humanized: {now.AddHours(25).Humanize()}");
        WriteLine($"Add 7 days, Humanized: {now.AddDays(7).Humanize()}");
        WriteLine($"Add 7 days and 1 minute, Humanized: {now.AddDays(7).AddMinutes(1).Humanize()}");
        WriteLine($"Add 1 month, Humanized: {now.AddMonths(1).Humanize()}");
        WriteLine();

        // Examples of TimeSpan humanization.
        int[] daysArray = { 12, 13, 14, 15, 16 };
        foreach (int days in daysArray)
        {
            WriteLine($"{days} days, Humanized: {TimeSpan.FromDays(days).Humanize()}");
            WriteLine($"{days} days, Humanized with precision 2: {TimeSpan.FromDays(days).Humanize(precision: 2)}");
            WriteLine($"{days} days, Humanized with max unit days: {TimeSpan.FromDays(days).Humanize(maxUnit: Humanizer.Localisation.TimeUnit.Day)}");
            WriteLine();
        }

        // Examples of clock notation.
        TimeOnly[] times = { new TimeOnly(9, 0), new TimeOnly(9, 15), new TimeOnly(15, 30) };
        foreach (TimeOnly time in times)
        {
            WriteLine($"{time}: {time.ToClockNotation()}");
        }
    }
}
