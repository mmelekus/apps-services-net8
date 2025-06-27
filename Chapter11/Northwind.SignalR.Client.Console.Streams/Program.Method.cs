using System;
using System.Net;

partial class Program
{
    static async IAsyncEnumerable<string> GetStockAsync()
    {
        for (int i = 0; i < 10; i++)
        {
            // Return a random four-letter stock code.
            yield return $"{AtoZ()}{AtoZ()}{AtoZ()}{AtoZ()}";
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }

    static string AtoZ()
    {
        // Generate a random letter from A to Z.
        return char.ConvertFromUtf32(Random.Shared.Next(65, 91));
    }
}
