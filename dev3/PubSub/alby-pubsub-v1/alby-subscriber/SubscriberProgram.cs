using System;
using IO.Ably;
using IO.Ably.Realtime;

namespace MyApp
{
    public class AlbySubscriber
    {
        static string channelName = "channel-order";
        static async Task Main(string[] args)
        {
            // Initialize the Ably Realtime client
            ClientOptions clientOptions = new ClientOptions(ENV.apiKey_root) // also can use subsciber
            {
                ClientId = channelName
            };

            AblyRealtime realtime = new AblyRealtime(clientOptions);

            // Wait for the connection to be established
            realtime.Connection.On(ConnectionEvent.Connected, args => {
                Console.WriteLine("Subscriber started!");
            });

            // Get a channel instance
            var channel = realtime.Channels.Get(channelName);

            // // Subscribe to messages on the channel
            // channel.Subscribe(message =>
            // {
            //     Console.WriteLine($"Received message: {message.Data}");
            // });

            // Subscribe to messages on the channel with a specific event name
            channel.Subscribe("create", message =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"message received for event {message.Name}");
                Console.WriteLine($"message data: {message.Data}");
                Console.WriteLine($"message id: {message.Id}");
                Console.WriteLine($"email sent for this order.");
                Console.WriteLine($"notification sent for this order.");
                Console.ResetColor();
            });

            channel.Subscribe("update", message =>
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"message received for event {message.Name}");
                Console.WriteLine($"message data: {message.Data}");
                Console.WriteLine($"message id: {message.Id}");
                Console.WriteLine($"notification sent for this order.");
                Console.ResetColor();
            });

            // Keep the program running
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}