
using System.Text;
using System.Web;

namespace Steve.ManagerHero.UserService.Helpers;
public class UrlBuilder
{
    private readonly string _baseUrl;
    private readonly Dictionary<string, string> _queryParameters;

    public UrlBuilder(string baseUrl)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new ArgumentException("Base URL cannot be null or empty", nameof(baseUrl));

        _baseUrl = baseUrl;
        _queryParameters = new Dictionary<string, string>();
    }

    public UrlBuilder AddQueryParameter(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("Query parameter key cannot be null or empty", nameof(key));

        _queryParameters[key] = value;
        return this;
    }

    public UrlBuilder AddQueryParameters(IDictionary<string, string> parameters)
    {
        foreach (var param in parameters)
        {
            AddQueryParameter(param.Key, param.Value);
        }
        return this;
    }

    public override string ToString()
    {
        var sb = new StringBuilder(_baseUrl);

        if (_queryParameters.Count > 0)
        {
            sb.Append('?');
            
            bool first = true;
            foreach (var param in _queryParameters)
            {
                if (!first)
                sb.Append('&');
                
                sb.Append(HttpUtility.UrlEncode(param.Key));
                sb.Append('=');
                sb.Append(HttpUtility.UrlEncode(param.Value));
                
                first = false;
            }
        }

        return sb.ToString();
    }
}