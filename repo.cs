using System;
using System.Text.Json.Serialization;

namespace WebAPIClient
{
    public class Repository
    {
        // [JsonPropertyName] attribute added to specify how this property appears in the JSON.
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("html_url")]
        public Uri? GitHubHomeUrl { get; set; }

        [JsonPropertyName("homepage")]
        public Uri? Homepage { get; set; }

        [JsonPropertyName("watchers")]
        public int? Watchers { get; set; }

        // Added a public property for the UTC representation of the date and time from the JSON
        [JsonPropertyName("pushed_at")]
        public DateTime LastPushUtc { get; set; }

        // LastPush is a readonly property that returns the date converted to local time
        public DateTime LastPush => LastPushUtc.ToLocalTime();
    }
}