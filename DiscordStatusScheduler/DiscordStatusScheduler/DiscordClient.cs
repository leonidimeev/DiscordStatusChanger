using System.Text;

namespace DiscordStatusScheduler;

public class DiscordClient
{
    private readonly AppOptions _appOptions;

    static string[] status_set = { "online", "idle", "dnd" };
    static int status_selector = 0;

    public DiscordClient(AppOptions appOptions)
    {
        _appOptions = appOptions;
    }

    public async Task<HttpResponseMessage> StatusTextChangeAsync(string text, string status)
    {
        DateTime currentDateTime = DateTime.Now;
        DateTime newDateTime = currentDateTime.AddSeconds(1);
        string formattedDateTime = newDateTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

        string payload = $"" +
            $"{{\"custom_status\": {{\"text\": \"{text}\", " +
            $"\"expires_at\": \"{formattedDateTime}\", " +
            $"\"status\": \"{status}\"}}}}";

        return await SendHttpRequestAsync("PATCH", _appOptions.DiscordApiUrl, payload);
    }

    public async Task<HttpResponseMessage> SendHttpRequestAsync(string method, string url, string payload)
    {
        using (var httpClient = new HttpClient())
        {
            var request = new HttpRequestMessage(new HttpMethod(method), url)
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };

            request.Headers.Add("authorization", _appOptions.UserOptions.AuthToken);

            return await httpClient.SendAsync(request);
        }
    }
}
