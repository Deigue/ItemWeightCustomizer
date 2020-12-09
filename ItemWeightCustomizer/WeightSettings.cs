using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public class WeightSettings
    {
        [JsonProperty("books")] public float Books { get; set; } = -1;
        [JsonProperty("ingredients")] public float Ingredients { get; set; } = -1;
        [JsonProperty("scrolls")] public float Scrolls { get; set; } = -1;
        [JsonProperty("soulgems")] public float Soulgems { get; set; } = -1;
        [JsonProperty("armors")] public float Armors { get; set; } = -1;
        [JsonProperty("weapons")] public float Weapons { get; set; } = -1;
    }
}