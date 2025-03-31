using System.Text.Json.Serialization;

namespace GenerateAccessToken.Model;

public class ModelClientCredentials
{
    [JsonPropertyName("client_id")]
    public string? ClientId { get; set; }

    [JsonPropertyName("client_secret")]
    public string? ClientSecret { get; set; }

    [JsonPropertyName("grant_type")]
    public string? GrantType { get; set; }

    [JsonPropertyName("scope")]
    public string? Scopes { get; set; }
}
