using Microsoft.AspNetCore.SignalR.Client; // To use HubConnection
using Northwind.Common.Streams; // To use StockPrice.

Write("Enter a stock (press Enter for MSFT): ");
string? stock = ReadLine()?.Trim().ToUpperInvariant();
if (string.IsNullOrEmpty(stock))
{
    stock = "MSFT";
}

HubConnection hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:5111/stockprice")
    .Build();
await hubConnection.StartAsync();

try
{
    CancellationTokenSource cts = new();
    IAsyncEnumerable<StockPrice> stockPrices = hubConnection.StreamAsync<StockPrice>("GetStockPriceUpdates", stock, cts.Token);
    await foreach (StockPrice stockPrice in stockPrices)
    {
        WriteLine($"{stockPrice.Stock} is now {stockPrice.Price:C}.");
        Write("Do you want to cancel (Y/N)? ");
        ConsoleKey key = ReadKey().Key;
        if (key == ConsoleKey.Y)
        {
            cts.Cancel();
        }
        WriteLine();
    }
}
catch (Exception ex)
{
    WriteLine($"{ex.GetType()} says {ex.Message}");
}

WriteLine();
WriteLine("Streaming download completed.");
await hubConnection.SendAsync("UploadStocks", GetStockAsync());
WriteLine("Uploading stocks to service... (press ENTER to stop.)");
ReadLine();
WriteLine("Ending console app.");