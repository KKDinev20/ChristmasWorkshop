using Newtonsoft.Json;

namespace ChristmasApi.Main.Requests;

public class DescriptionRequest
{
    [JsonProperty("desc")]
    public string? Desc { get; set; }
}