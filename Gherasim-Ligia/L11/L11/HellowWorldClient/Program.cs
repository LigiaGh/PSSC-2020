﻿using HelloWorldInterface;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Threading.Tasks;


namespace HellowWorldClient
{
    class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            { 
                using (var client = await ConnectClient())  // se dezaloca
                {
                    await DoClientWork(client); // creem un client care se conecteaza la cluster
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering() // configuram un cluster unde se conecteaza clientul noastra
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                // clientul n-are nevoie sa stie de implementari numai de intefete
                .ConfigureLogging(logging => logging.AddConsole())
                .AddSimpleMessageStreamProvider("SMSProvider", options => { options.FireAndForgetDelivery = true; })
                .Build();

            await client.Connect(); //clientul e pregatit sa apele grainuri din clusterul local
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            // example of calling grains from the initialized client
            
            var friend = client.GetGrain<IHello>(0); // cheia grainului // acelasi id 2 grinuri -> merg la acelasi obiect
            var response = await friend.SayHello("Good morning, HelloGrain!");
            Console.WriteLine($"\n\n{response}\n\n");

            //Pick a guid for a chat room grain and chat room stream
            var guid = Guid.Empty;
            //Get one of the providers which we defined in config
            var streamProvider = client.GetStreamProvider("SMSProvider");
            //Get the reference to a stream
            var stream = streamProvider.GetStream<string>(guid, "CHAT");
            await stream.OnNextAsync("Hello event");
        }
    }
}
