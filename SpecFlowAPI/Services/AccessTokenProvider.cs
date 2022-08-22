using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;

namespace SpecFlowAPI
{
	public class AccessTokenProvider : IAuthenticator
	{
		private readonly GlobalSettings _globalSettings;

		private struct TokenResponse
		{
			[JsonPropertyName("access_token")]
			public string AccessToken { get; set; }
			[JsonPropertyName("token_type")]
			public string TokenType { get; set; }
			[JsonPropertyName("expires_in")]
			public int ExpiresIn { get; set; }
		}

		public AccessTokenProvider(GlobalSettings globalSettings)
		{
			_globalSettings = globalSettings;
		}

		/// <summary>
		/// It authenticates in the Spotify API
		/// </summary>
		/// <returns>The Bearer token</returns>
		public string GetToken()
		{
			var webClient = new WebClient();

			var postparams = new NameValueCollection();
			postparams.Add("grant_type", "client_credentials");

			var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{_globalSettings.ClientId}:{_globalSettings.ClientSecret}"));
			webClient.Headers.Add(HttpRequestHeader.Authorization, $"Basic {authHeader}");

			var response = webClient.UploadValues(_globalSettings.AuthUrl, postparams);

			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
			var enumConverter = new JsonStringEnumConverter();
			options.Converters.Add(enumConverter);

			var tokenResponse = Encoding.UTF8.GetString(response);

			return JsonSerializer.Deserialize<TokenResponse>(tokenResponse, options).AccessToken;
		}

		public void Authenticate(IRestClient client, IRestRequest request)
		{
			_ = request.AddOrUpdateParameter("Authorization", $"Bearer {GetToken()}", ParameterType.HttpHeader);
		}
	}
}
