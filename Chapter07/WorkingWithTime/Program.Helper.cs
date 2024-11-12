using System.Globalization; // To use CultureInfo.
using System.Text; // To use Encoding

partial class Program
{
    private static void ConfigureConsole(string culture = "en-US", bool overrideComputerCulture = true)
    {
        // To enable special characters like Euro currency symbol.
        OutputEncoding = Encoding.UTF8;
        Thread t = Thread.CurrentThread;

        if (overrideComputerCulture)
        {
            t.CurrentCulture = CultureInfo.GetCultureInfo(culture);
            t.CurrentUICulture = t.CurrentCulture;
        }
        
        CultureInfo ci = t.CurrentCulture;
        WriteLine($"Current culture: {ci.DisplayName}");
        WriteLine($"Short date pattern: {ci.DateTimeFormat.ShortDatePattern}");
        WriteLine($"Long date pattern: {ci.DateTimeFormat.LongDatePattern}");
        WriteLine();
    }

    private static void SectionTitle(string title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine($"*** {title} ***");
        ForegroundColor = previousColor;
    }
}