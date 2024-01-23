using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickbaseNet.Requests;
using QuickbaseNet.Responses;

namespace QuickbaseNet.Services
{
    public class QuickbaseClient
    {
        private const string BaseUrl = "https://api.quickbase.com";
        private const string UserAgent = "QuickbaseNet/0.1.1";

        private readonly HttpClient _httpClient = new HttpClient();

        public QuickbaseClient(string realm, string userToken)
        {
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{realm}.quickbase.com");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        }

        public async Task<(QuickbaseQueryResponse Response, QuickbaseErrorResponse Error, bool IsSuccess)> QueryRecords(QuickbaseQueryRequest quickBaseRequest)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(quickBaseRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/v1/records/query", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return (JsonConvert.DeserializeObject<QuickbaseQueryResponse>(jsonResponse), null, true);
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return (null, JsonConvert.DeserializeObject<QuickbaseErrorResponse>(errorResponse), false);
        }

        internal async Task<(QuickbaseRecordUpdateResponse Response, QuickbaseErrorResponse Error, bool IsSuccess)>
            InsertRecords(InsertOrUpdateRecordRequest quickBaseRequest)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(quickBaseRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/v1/records", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return (JsonConvert.DeserializeObject<QuickbaseRecordUpdateResponse>(jsonResponse), null, true);
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return (null, JsonConvert.DeserializeObject<QuickbaseErrorResponse>(errorResponse), false);
        }

        internal async Task<(QuickbaseRecordUpdateResponse Response, QuickbaseErrorResponse Error, bool IsSuccess)>
            UpdateRecords(InsertOrUpdateRecordRequest quickBaseRequest)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(quickBaseRequest), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/v1/records", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return (JsonConvert.DeserializeObject<QuickbaseRecordUpdateResponse>(jsonResponse), null, true);
            }

            var errorResponse = await response.Content.ReadAsStringAsync();
            return (null, JsonConvert.DeserializeObject<QuickbaseErrorResponse>(errorResponse), false);
        }
    }
}
