using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuickbaseNet.Errors;
using QuickbaseNet.Requests;
using QuickbaseNet.Responses;

namespace QuickbaseNet.Services
{
    /// <summary>
    /// Provides methods for interacting with the QuickBase API.
    /// </summary>
    public class QuickbaseClient
    {
        private const string BaseUrl = "https://api.quickbase.com";
        private const string UserAgent = "QuickbaseNet/1.0.1";

        /// <summary>
        /// Gets or sets the HTTP client used to make requests to the QuickBase API.
        /// </summary>
        public HttpClient Client { get; set; } = new HttpClient();

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickbaseClient"/> class with the specified realm and user token.
        /// </summary>
        /// <param name="realm">The realm hostname of the QuickBase account.</param>
        /// <param name="userToken">The user token used for authentication.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="realm"/> or <paramref name="userToken"/> is null or empty.</exception>
        public QuickbaseClient(string realm, string userToken)
        {
            if (string.IsNullOrEmpty(realm))
            {
                throw new ArgumentNullException(nameof(realm), "Realm cannot be null or empty");
            }

            if (string.IsNullOrEmpty(userToken))
            {
                throw new ArgumentNullException(nameof(userToken), "User token cannot be null or empty");
            }

            Client.BaseAddress = new Uri(BaseUrl);
            Client.DefaultRequestHeaders.Add("QB-Realm-Hostname", $"{realm}.quickbase.com");
            Client.DefaultRequestHeaders.Add("Authorization", $"QB-USER-TOKEN {userToken}");
            Client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
        }

        /// <summary>
        /// Sends a query request to the QuickBase API and retrieves the response.
        /// </summary>
        /// <param name="quickBaseRequest">The query request to send.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the query response.</returns>
        public async Task<QuickbaseResult<QuickbaseQueryResponse>> QueryRecords(QuickbaseQueryRequest quickBaseRequest)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(quickBaseRequest), Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/v1/records/query", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var queryResponse = JsonConvert.DeserializeObject<QuickbaseQueryResponse>(jsonResponse);

                // Check if data is null or empty
                return queryResponse.Data.Count == 0
                    ? QuickbaseResult.Failure<QuickbaseQueryResponse>(QuickbaseError.NotFound("QuickbaseNet.Failure",
                        "No records found", $"The query did not find any records matching that criteria"))
                    : QuickbaseResult.Success(queryResponse);
            }

            var errorResponse = JsonConvert.DeserializeObject<QuickbaseErrorResponse>(jsonResponse);

            // Check if its 4xx error
            if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest &&
                response.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {
                return QuickbaseResult.Failure<QuickbaseQueryResponse>(QuickbaseError.ClientError("QuickbaseNet.ClientError", errorResponse.Message, errorResponse.Description));
            }

            // Its a 5xx error
            return QuickbaseResult.Failure<QuickbaseQueryResponse>(QuickbaseError.ServerError("QuickbaseNet.ServerError", errorResponse.Message, errorResponse.Description));
        }

        /// <summary>
        /// Sends a request to insert records to the QuickBase API and retrieves the response.
        /// </summary>
        /// <param name="quickBaseRequest">The insert request to send.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the insert response.</returns>
        public async Task<QuickbaseResult<QuickbaseRecordUpdateResponse>> InsertRecords(InsertOrUpdateRecordRequest quickBaseRequest)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(quickBaseRequest), Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/v1/records", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Check if data is null or empty
                if (string.IsNullOrEmpty(jsonResponse))
                {
                    return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.NotFound("QuickbaseNet.Failure",
                                               "No records found", $"The query did not find any records matching that criteria"));
                }

                return QuickbaseResult.Success(JsonConvert.DeserializeObject<QuickbaseRecordUpdateResponse>(jsonResponse));
            }

            var errorResponse = await response.Content.ReadAsStringAsync();

            // Check if its 4xx error
            if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest &&
                response.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {
                return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.ClientError("QuickbaseNet.ClientError", errorResponse, "Client error"));
            }

            // Its a 5xx error
            return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.ServerError("QuickbaseNet.ServerError", errorResponse, "Server error"));
        }

        /// <summary>
        /// Sends a request to update records in the QuickBase API and retrieves the response.
        /// </summary>
        /// <param name="quickBaseRequest">The update request to send.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the update response.</returns>
        public async Task<QuickbaseResult<QuickbaseRecordUpdateResponse>> UpdateRecords(InsertOrUpdateRecordRequest quickBaseRequest)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(quickBaseRequest), Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/v1/records", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                // Check if data is null or empty
                if (string.IsNullOrEmpty(jsonResponse))
                {
                    return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.NotFound("QuickbaseNet.Failure",
                                               "No records found", $"The query did not find any records matching that criteria"));
                }

                return QuickbaseResult.Success(JsonConvert.DeserializeObject<QuickbaseRecordUpdateResponse>(jsonResponse));
            }

            var errorResponse = await response.Content.ReadAsStringAsync();

            // Check if its 4xx error
            if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest &&
                response.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {
                return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.ClientError("QuickbaseNet.ClientError", errorResponse, "Client error"));
            }

            // Its a 5xx error
            return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.ServerError("QuickbaseNet.ServerError", errorResponse, "Server error"));
        }

        /// <summary>
        /// Sends a request to delete records from the QuickBase API and retrieves the response.
        /// </summary>
        /// <param name="quickBaseRequest">The delete request to send.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the delete response.</returns>
        public async Task<QuickbaseResult<QuickbaseRecordUpdateResponse>> DeleteRecords(DeleteRecordRequest quickBaseRequest)
        {
            // Serialize your request object into a JSON string
            var requestJson = JsonConvert.SerializeObject(quickBaseRequest);
            HttpContent content = new StringContent(requestJson, Encoding.UTF8, "application/json");

            // Create an HttpRequestMessage for DELETE
            var request = new HttpRequestMessage(HttpMethod.Delete, "/v1/records")
            {
                Content = content
            };

            // Send the request
            var response = await Client.SendAsync(request);

            // The rest of your method remains the same
            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(jsonResponse))
                {
                    return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.NotFound("QuickbaseNet.Failure",
                        "No records found", $"The query did not find any records matching that criteria"));
                }

                return QuickbaseResult.Success(JsonConvert.DeserializeObject<QuickbaseRecordUpdateResponse>(jsonResponse));
            }

            var errorResponse = await response.Content.ReadAsStringAsync();

            if (response.StatusCode >= System.Net.HttpStatusCode.BadRequest &&
                response.StatusCode < System.Net.HttpStatusCode.InternalServerError)
            {
                return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.ClientError("QuickbaseNet.ClientError", errorResponse, "Client error"));
            }

            return QuickbaseResult.Failure<QuickbaseRecordUpdateResponse>(QuickbaseError.ServerError("QuickbaseNet.ServerError", errorResponse, "Server error"));
        }
    }
}
