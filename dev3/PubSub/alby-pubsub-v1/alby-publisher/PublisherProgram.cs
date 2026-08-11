using System;
using IO.Ably;
using IO.Ably.Realtime;

namespace MyApp
{
    public class AlbyPublisher
    {
        static string channelName = "channel-order";
        static async Task Main(string[] args)
        {
            // Initialize the Ably Realtime client
            ClientOptions clientOptions = new ClientOptions(ENV.apiKey_root)
            {
                ClientId = channelName
            };

            AblyRealtime realtime = new AblyRealtime(clientOptions);

            // Wait for the connection to be established
            realtime.Connection.On(ConnectionEvent.Connected, args => {
                Console.WriteLine("Publisher started!");
            });

            // Get a channel instance
            var channel = realtime.Channels.Get(channelName);
            
            // // Publish a message to the channel
            // await channel.PublishAsync("", $"Order:{Guid.NewGuid()} placed at {DateTime.Now}");

            // Publish a message to the channel
            string order1 = $"Order:{Guid.NewGuid()} placed at {DateTime.Now}";
            await channel.PublishAsync("create", order1);
            PrintGreen($"Channel: create Order{order1}");

            string order2 = $"Order:{Guid.NewGuid()} updated at {DateTime.Now}";
            // Publish a message to the channel
            await channel.PublishAsync("update", order2);
            PrintBlue($"Channel: update Order{order2}");

            // Keep the program running
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void PrintBlue(string content)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(content);
            Console.ResetColor();
        }

        static void PrintGreen(string content)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(content);
            Console.ResetColor();
        }
    }
}