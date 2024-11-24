﻿
using System.Net.Http.Json; // To use ReadFromJsonAsync<T> method.
using Northwind.Common.EntityModels.SqlServer.Models; // To use Product.

Write("Enter a client name or press Enter: ");
string? clientName = ReadLine();
if (string.IsNullOrWhiteSpace(clientName)) { clientName = $"console-client-{Guid.NewGuid()}"; }
WriteLine($"X-Client-Id will be: {clientName}");
HttpClient client = new();
client.BaseAddress = new("https://localhost:5081");
client.DefaultRequestHeaders.Accept.Add(new("application/json"));
// Specify the rate limiting client id for this console app.
client.DefaultRequestHeaders.Add("X-Client-Id", clientName);
while (true)
{
    WriteInColor($"{DateTime.UtcNow:hh:mm:ss}: ", ConsoleColor.DarkGreen);
    int waitFor = 1; // Second.
    try
    {
        HttpResponseMessage response = await client.GetAsync("api/products");
        if (response.IsSuccessStatusCode)
        {
            Product[]? products = await response.Content.ReadFromJsonAsync<Product[]>();
            if (products != null)
            {
                foreach (Product product in products)
                {
                    Write(product.ProductName);
                    Write(", ");
                }
                WriteLine();
            }
        }
        else
        {
            string retryAfter = response.Headers.GetValues("Retry-After").ToArray()[0];
            if (int.TryParse(retryAfter, out waitFor)) { retryAfter = $"I will retry after {waitFor} seconds."; }
            WriteInColor($"{(int)response.StatusCode}: {await response.Content.ReadAsStringAsync()} {retryAfter}", ConsoleColor.DarkRed);
            WriteLine();
        }
    }
    catch (Exception ex)
    {
        WriteLine(ex.Message);
    }
    await Task.Delay(TimeSpan.FromSeconds(waitFor));
}