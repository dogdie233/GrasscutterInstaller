using Newtonsoft.Json;
using NLog;

namespace GrasscutterInstaller;

public static class NetworkHelper
{
    private static readonly HttpClient _client = new HttpClient();
    private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

    public static async Task<T> RequestJsonAsync<T>(HttpRequestMessage requestMessage)
    {
        // Set default UserAgent
        if (!requestMessage.Headers.Contains("User-Agent"))
        {
            requestMessage.Headers.Add("User-Agent", "GrasscutterInstaller/" + Environment.Version.ToString());
        }
        _logger.Trace($"Send a {requestMessage.Method} request to {requestMessage.RequestUri}");

        // Send resquest
        var response = await _client.SendAsync(requestMessage);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
        {
            // TODO: Help me correct my grammar, my English is poor, XD.
            _logger.Warn($"The response status code is {(int)response.StatusCode} of {requestMessage.RequestUri}");
        }
        var jsonText = await response.Content.ReadAsStringAsync();
        _logger.Trace($"Convert to {typeof(T)} from response content: " + jsonText);

        // Convert json text
        try
        {
            var json = JsonConvert.DeserializeObject<T>(jsonText);
            if (json == null)
            {
                _logger.Error("When convert json");
                throw new NullReferenceException("Json convert result is null.");
            }
            return json;
        }
        catch (Exception)
        {
            Utils.DumpExceptionJson(jsonText, typeof(T), response);
            _logger.Error($"An exception was occurred when deserialize text to Type {typeof(T)} from {requestMessage.RequestUri}, file save to ");
            throw;
        }
    }

    public static async Task<T> RequestJsonAsync<T>(HttpMethod method, string uri, string accept = "application/vnd.github.v3+json")
    {
        var requestMessage = new HttpRequestMessage(method, uri);
        requestMessage.Headers.Add("Accept", accept);
        return await RequestJsonAsync<T>(requestMessage);
    }

    public static async Task DownloadAsync(Stream targetStream, string uri, IProgress<double>? progress = null)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        requestMessage.Headers.Add("User-Agent", "GrasscutterInstaller/" + Environment.Version.ToString());
        var response = await _client.SendAsync(requestMessage, HttpCompletionOption.ResponseHeadersRead);
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            // TODO: Help me correct my grammar, my English is poor, XD.
            _logger.Error($"The response status code is {(int)response.StatusCode} of {requestMessage.RequestUri}");
            throw;
        }
        long readLength = 0;
        var responseStream = await response.Content.ReadAsStreamAsync();
        var buffer = new byte[1024];
        if (response.Content.Headers.ContentLength == null)
        {
            throw new HttpRequestException($"Not contain content-length in the request {uri}");
        }
        var totalLength = response.Content.Headers.ContentLength.Value;
        int length;
        while ((length = responseStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            readLength += length;
            targetStream.Write(buffer, 0, length);
            progress?.Report((double)readLength / totalLength);
        }
        targetStream.Flush();
    }
}
