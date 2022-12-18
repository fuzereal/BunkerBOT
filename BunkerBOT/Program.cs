using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using BunkerBOT.Classes;

namespace BunkerBOT
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var token = "UR TOKEN";
            var botClient = new BunkerBOTClient(token);
            using var cancellationTokenSource = new CancellationTokenSource();

            
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } 
            };

            botClient.StartReceiving(receiverOptions, cancellationTokenSource);

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Начал работу с  @{me.Username}");
            Console.ReadLine();

            cancellationTokenSource.Cancel();
        }
    }
}
