
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using Google.Apis.Auth;
using Newtonsoft.Json;
using Steve.ManagerHero.UserService.Application.Interfaces.Services;

namespace Steve.ManagerHero.UserService.Infrastructure.Auth.External;

public class GoogleAuthService(
    IConfiguration _configuration
) : IGoogleAuthService
{
    private readonly string? _clientId = _configuration["Authentication:Google:ClientId"];
    private readonly string? _clientSecret = _configuration["Authentication:Google:ClientSecret"];
    private readonly string? _redirectUri = _configuration["Authentication:Google:RedirectUri"];


    public string GetLoginUrl(string state)
    {
        if (string.IsNullOrEmpty(_clientId)) throw new NullReferenceException("The ClientId must be config in section Authentication:Google:ClientId before use GoogleAuthService");

        if (string.IsNullOrEmpty(_redirectUri)) throw new NullReferenceException("The RedirectUri must be config in section Authentication:Google:RedirectUri before use GoogleAuthService");

        // Build google login url
        var url = new StringBuilder("https://accounts.google.com/o/oauth2/v2/auth?");
        url.Append($"client_id={_clientId}");
        url.Append($"&redirect_uri={HttpUtility.UrlEncode(_redirectUri)}");
        url.Append($"&response_type=code");
        url.Append($"&scope=email%20profile");
        url.Append($"&state={state}");
        url.Append("&access_type=offline");
        url.Append("&prompt=consent");

        return url.ToString();
    }

    /// <inheritdoc/>
    public async Task<OAuthToken?> GetGoogleOauthToken(string code)
    {
        try
        {
            var content = new FormUrlEncodedContent(
                new[]
                {
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("client_id", _clientId),
                        new KeyValuePair<string, string>("client_secret", _clientSecret),
                        new KeyValuePair<string, string>("redirect_uri", _redirectUri),
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                }
            );

            using var httpClient = new HttpClient();

            var response = await httpClient.PostAsync("https://oauth2.googleapis.com/token", content);

            var responseString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<OAuthToken>(responseString);
            }

            throw new Exception("Error while trying to get google oauth token");
        }
        catch
        {
            throw;
        }
    }

    public async Task<OAuthUser> VerifyTokenAsync(string token)
    {
        if (string.IsNullOrEmpty(_clientSecret)) throw new NullReferenceException("The ClientSecret must be config in section Authentication:Google:ClientSecret before use GoogleAuthService");

        OAuthToken? oauthToken = await GetGoogleOauthToken(token);

        if (oauthToken == null) throw new Exception("Error while trying to get google oauth token");

        var payload = await GoogleJsonWebSignature.ValidateAsync(oauthToken.IdToken, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = [_clientId]
        });

        // Lấy thông tin user từ payload
        var email = payload.Email;
        var name = payload.Name;
        var picture = payload.Picture;
        var verified = payload.EmailVerified;

        return new OAuthUser(payload.Subject, email, name, verified, picture);
    }
}