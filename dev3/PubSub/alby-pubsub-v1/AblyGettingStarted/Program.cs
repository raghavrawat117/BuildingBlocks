using System;
using IO.Ably;
using IO.Ably.Realtime;

namespace MyApp
{
    // There are 2 API keys
    //****************************** (Subscribe only)
    ////****************************** (Root)
    public class AblyGettingStarted
    {
        static async Task Main(string[] args)
        {
            // Initialize the Ably Realtime client
            ClientOptions clientOptions = new ClientOptions(ENV.apiKey_root)
            {
                ClientId = "my-first-client"
            };

            AblyRealtime realtime = new AblyRealtime(clientOptions);

            // Wait for the connection to be established
            realtime.Connection.On(ConnectionEvent.Connected, args => {
                Console.WriteLine("Made my first connection!");
            });

            // Get a channel instance
            var channel = realtime.Channels.Get("my-first-channel");

            // Subscribe to messages on the channel
            channel.Subscribe(message =>
            {
                Console.WriteLine($"Received message: {message.Data}");
            });

            await channel.PublishAsync("", "A message sent from my first client!");

            // Keep the program running
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}