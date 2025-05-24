using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace PSA_1uzd.Business.Services
{
    public class SpotifyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientId = "174a2aebf8dd49eb9ee6c1e3016568cf";
        private readonly string _clientSecret = "45d5291a93994a28a86a0a14dfb9212e";

        public SpotifyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetAccessTokenAsync()
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));

            var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
            request.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            });

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<JsonElement>(json);

            return tokenObj.GetProperty("access_token").GetString();
        }

        public async Task<List<(string Name, string ImageUrl, string Url)>> SearchPlaylistsAsync(string genre)
        {
            try
            {
                var token = await GetAccessTokenAsync();

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(genre)}&type=playlist&limit=5");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var jsonObj = JsonSerializer.Deserialize<JsonElement>(json);

                var playlists = new List<(string Name, string ImageUrl, string Url)>();

                foreach (var item in jsonObj.GetProperty("playlists").GetProperty("items").EnumerateArray())
                {
                    if (item.ValueKind != JsonValueKind.Object)
                        continue; // Skip if not an object

                    if (!item.TryGetProperty("name", out var nameProp) ||
                        !item.TryGetProperty("images", out var imagesProp) ||
                        !item.TryGetProperty("external_urls", out var externalUrlsProp))
                        continue; // Skip if any key part is missing

                    var name = item.GetProperty("name").GetString();
                    var imageUrl = item.GetProperty("images").EnumerateArray().FirstOrDefault().GetProperty("url").GetString();
                    var url = item.GetProperty("external_urls").GetProperty("spotify").GetString();

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(url))
                    {
                        playlists.Add((name, imageUrl, url));
                    }
                }

                return playlists;
            }
            catch (Exception)
            {
                return new List<(string Name, string ImageUrl, string Url)>();
            }
        }

    }
}

