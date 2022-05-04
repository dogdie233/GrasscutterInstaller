namespace GrasscutterInstaller;

public class LazyGetJsonObject<T>
{
    private readonly string _url;
    private T? _value;

    public T Value
    {
        get
        {
            if (_value == null) { _value = NetworkHelper.RequestJsonAsync<T>(HttpMethod.Get, _url).Result; }
            return _value;
        }
    }

    public void Refresh() => _value = NetworkHelper.RequestJsonAsync<T>(HttpMethod.Get, _url).Result;
    public async void RefreshAsync() => _value = await NetworkHelper.RequestJsonAsync<T>(HttpMethod.Get, _url);

    public LazyGetJsonObject(string url)
    {
        _url = url;
    }
}
