using inventoryapp.Models;

namespace inventoryapp.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _httpClient;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> LoginAsync(string credential)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7184/api/Users/login");
            request.Headers.Add("Authorization", credential);
            return await _httpClient.SendAsync(request);
        }
    }
}
