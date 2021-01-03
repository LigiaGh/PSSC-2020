using System;
using System.Threading.Tasks;
using HelloWorldGrain;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace OrleansHelloWorld
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
                var host = await StartSilo(); // porneste runtime-ul de orleans
                Console.WriteLine("\n\n Press Enter to terminate...\n\n");
                Console.ReadLine(); // asteapta enter si opreste cluster

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration // pornirea clusterului
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering() // construim un builder pe localhost - o singura masina, cea locala
                .Configure<ClusterOptions>(options => // unde ruleaza si cum se va numi( cum il pot adresa)
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                // vreau sa imi cauti grainurile in assembly-ul unde e HelloGrain 
                // parts -> spun unde sunt grainurile
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole()) // partea de logging
                .AddSimpleMessageStreamProvider("SMSProvider", options => { options.FireAndForgetDelivery = true; })
                .AddMemoryGrainStorage("PubSubStore");

            var host = builder.Build(); // build
            await host.StartAsync(); // pornesc clusterul
            return host;
        }
    }
}
