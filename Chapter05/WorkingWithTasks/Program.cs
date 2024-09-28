using System.Diagnostics; // To use Stopwatch

namespace WorkingWithTasks;

partial class Program
{
    static void Main(string[] args)
    {
        // RunSync();
        // RunTasksNoWait();
        // RunTasksWait();
        // RunWaitForTasks();
        // RunNestedMethodsOuterWait();
        RunNestedMethodsAllWait();
    }

    private static void RunSync()
    {
        Stopwatch timer = RunStart();
        
        SectionTitle("Running methods synchronously on one thread.");
        MethodA();
        MethodB();
        MethodC();
        RunEnd(timer);
    }

    private static void RunTasksNoWait()
    {
        Stopwatch timer = RunStart();

        SectionTitle("Running methods asynchronously on multiple threads, no waiting.");
        Task taskA = new(MethodA);
        taskA.Start();
        Task taskB = Task.Factory.StartNew(MethodB);
        Task taskC = Task.Run(MethodC);
        RunEnd(timer);
    }

    private static void RunTasksWait()
    {
        Stopwatch timer = RunStart();

        SectionTitle("Running methods asynchronously on multiple threads, waiting.");
        Task taskA = new(MethodA);
        taskA.Start();
        Task taskB = Task.Factory.StartNew(MethodB);
        Task taskC = Task.Run(MethodC);

        Task[] tasks = { taskA, taskB, taskC };
        Task.WaitAll(tasks);

        RunEnd(timer);
    }

    private static void RunWaitForTasks()
    {
        Stopwatch timer = RunStart();

        SectionTitle("Passing the result of one task as an input into another.");
        Task<string> taskServiceThenSProc = Task.Factory
            .StartNew(CallWebService) // returns Task<decimal>
            .ContinueWith(previousTask => // returns Task<string>
                CallStoredProdcedure(previousTask.Result));
        WriteLine($"Result: {taskServiceThenSProc.Result}");

        RunEnd(timer);
    }

    private static void RunNestedMethodsOuterWait()
    {
        Stopwatch timer = RunStart();

        SectionTitle("Nested and child tasks, outer wait");
        Task outerTask = Task.Factory.StartNew(() => OuterMethod(false));
        outerTask.Wait();
        WriteLine("Console app is stopping.");
    }

    private static void RunNestedMethodsAllWait()
    {
        Stopwatch timer = RunStart();

        SectionTitle("Nested and child tasks, all wait");
        Task outerTask = Task.Factory.StartNew(() => OuterMethod(true));
        outerTask.Wait();
        WriteLine("Console app is stopping.");
    }
}
