using System.Diagnostics; // To use Stopwatch timer.

namespace WorkingWithTasks;

partial class Program
{
    private static void TitleWrapper(Func<string> title)
    {
        ConsoleColor previousColor = ForegroundColor;
        ForegroundColor = ConsoleColor.DarkYellow;
        WriteLine(title());
        ForegroundColor = previousColor;
    }

     private static void SectionTitle(string title)
    {
        TitleWrapper(() => $"*** {title} ***");
    }

    private static void TaskTitle(string title)
    {
        TitleWrapper(() => $"{title}");
    }

    private static void OutputThreadInfo()
    {
        Thread t = Thread.CurrentThread;
        TitleWrapper(() => $"Thread Id: {t.ManagedThreadId}, Priority: {t.Priority}, Background: {t.IsBackground}, Name: {t.Name ?? "null"}");
    }

    private static Stopwatch RunStart()
    {
        OutputThreadInfo();
        Stopwatch timer = Stopwatch.StartNew();
        return timer;
    }

    private static void RunEnd(Stopwatch timer) => WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");
}
