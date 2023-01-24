using Newtonsoft.Json;

public class GravatarResponse
{
    [JsonProperty("entry")]
    public Entry[] Entry { get; set; }
}
public class Entry
{



    [JsonProperty("displayName")]
    public string DisplayName { get; set; }


}