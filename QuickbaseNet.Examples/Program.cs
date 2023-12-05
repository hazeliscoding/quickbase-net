using Newtonsoft.Json;
using QuickbaseNet.Helpers;
using QuickbaseNet.Models;
using QuickbaseNet.Requests;
using QuickbaseNet.Services;

namespace QuickbaseNet.Examples
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var quickBaseClient = new QuickbaseClient("diamond", "bztp36_cn6v_0_c5k72hvbvma5mh8xmjgzb4qqsys");
            var query = new QuickbaseQueryBuilder()
                .From("bmycek2xq")
                .Select(3, 7, 14, 75, 150, 157, 354, 355, 367, 538, 539, 540, 541, 542, 543)
                .Where("{'7'.'CT'.'10136'}")
                .Build();

            string jsonRequest = JsonConvert.SerializeObject(query);

            var result = await quickBaseClient.QueryRecords(query);
            if (result.IsSuccess)
            {
                Console.WriteLine("Success!");
                Console.WriteLine(JsonConvert.SerializeObject(result.Response, Formatting.Indented));
            }
            else
            {
                Console.WriteLine("Error!");
                Console.WriteLine(JsonConvert.SerializeObject(result.Error, Formatting.Indented));
            }

            // var recordBuilder = new QuickbaseCommandBuilder()
            //     .ForTable("your_table_id")
            //     .ReturnFields(1, 2, 3)
            //     .AddRecord(record => record
            //         .AddField("fieldId1", "Value1")
            //         .AddField("fieldId2", "Value2"))
            //     .AddRecord(record => record
            //         .AddField("fieldId1", "Another Value1")
            //         .AddField("fieldId2", "Another Value2"));
            //
            // InsertOrUpdateRecordRequest request = recordBuilder.Build();

            var commandBuilder = new QuickbaseCommandBuilder()
                .ForTable("bck7gp3q2")
                .WithDeletionCriteria("{6.EX.'hello'}");

            DeleteRecordRequest deleteRequest = commandBuilder.BuildDeleteCommand();

        }
    }
}
