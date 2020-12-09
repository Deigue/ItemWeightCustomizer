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
        [JsonProperty("foods")] public float Foods { get; set; } = -1;
        [JsonProperty("potions")] public float Potions { get; set; } = -1;
        [JsonProperty("ingots")] public float Ingots { get; set; } = -1;
        [JsonProperty("gems")] public float Gems { get; set; } = -1;
        [JsonProperty("animalParts")] public float AnimalParts { get; set; } = -1;
        [JsonProperty("animalHides")] public float AnimalHides { get; set; } = -1;
        [JsonProperty("clutter")] public float Clutter { get; set; } = -1;
        [JsonProperty("miscItems")] public float MiscItems { get; set; } = -1;
    }
}