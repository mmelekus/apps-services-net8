using Microsoft.AspNetCore.SignalR.Client; // To use HubConnection
using Northwind.Common.Models; // To use UserMode, MessageModel

Write("Enter a username (required): ");
string? userName = ReadLine()?.Trim();
if (string.IsNullOrEmpty(userName))
{
    WriteLine("You must enter a username to register with chat!");
    return;
}

Write("Enter your groups (optional): ");
string? groups = ReadLine()?.Trim();
HubConnection hubConnection = new HubConnectionBuilder()
    .WithUrl("https://localhost:5111/chat")
    .Build();
hubConnection.On<MessageModel>("ReceiveMessage", (message) =>
{
    WriteLine($"To {message.To}, From {message.From}: {message.Body}");
});
await hubConnection.StartAsync();
WriteLine("Successfullly started.");

UserModel registration = new()
{
    Name = userName,
    Groups = groups
};
await hubConnection.InvokeAsync("Register", registration);
WriteLine("Successfully registered.");
WriteLine("Listening... (press ENTER to stop.)");
ReadLine();