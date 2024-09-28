namespace AsyncEnumerable;

partial class Program
{
    static async Task Main(string[] args)
    {
        await foreach (int number in GetNumbersAsync())
        {
            WriteLine($"Number: {number}");
        }
    }
}
