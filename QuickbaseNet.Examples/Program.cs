using Newtonsoft.Json;
using QuickbaseNet.Helpers;
using QuickbaseNet.Requests;
using QuickbaseNet.Services;

namespace QuickbaseNet.Examples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var quickBaseClient = new QuickbaseClient("builderprogram-pcohen", "b4mtg6_p7vz_0_dcmcjr3bdtp4csdtuu6qx3tyzuc");

            var insertCommand = new QuickbaseCommandBuilder()
                .ForTable("bsfhutyzn")
                .ReturnFields(1, 2, 3)
                .AddNewRecord(record => record
                    .AddField("6", "Cupcakes")
                    .AddField("7", "$14"))
                .BuildInsertUpdateCommand();

            var result = await quickBaseClient.InsertRecords(insertCommand);
            
            // var quickBaseClient = new QuickbaseClient("diamond", Environment.GetEnvironmentVariable("QB_USERTOKEN"));
            // var query = new QuickbaseQueryBuilder()
            //     .From("bsfhutyzn")
            //     .Select(6, 7, 8, 9, 10, 11, 12, 13)
            //     .Build();
            //
            // string jsonRequest = JsonConvert.SerializeObject(query);
            //
            // var result = await quickBaseClient.QueryRecords(query);
            if (result.IsSuccess)
            {
                Console.WriteLine("Success!");
                Console.WriteLine(JsonConvert.SerializeObject(result.Value, Formatting.Indented));
            }
            else if (result.IsFailure)
            {
                Console.WriteLine("Error!");
                Console.WriteLine(JsonConvert.SerializeObject(result.QuickbaseError, Formatting.Indented));
            }


            
            // InsertOrUpdateRecordRequest request = recordBuilder.Build();

            // var commandBuilder = new QuickbaseCommandBuilder()
            //     .ForTable("bck7gp3q2")
            //     .WithDeletionCriteria("{6.EX.'hello'}");
            //
            // DeleteRecordRequest deleteRequest = commandBuilder.BuildDeleteCommand();

        }
    }
}
