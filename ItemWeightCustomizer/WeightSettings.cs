using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public class WeightSettings
    {
        [JsonProperty("books")] public float Books { get; set; } = 1.5f;
        [JsonProperty("notes")] public float Notes { get; set; } = 0.5f;
        [JsonProperty("ingredients")] public float Ingredients { get; set; } = -1;
        [JsonProperty("scrolls")] public float Scrolls { get; set; } = 0.25f;
        [JsonProperty("soulgems")] public float Soulgems { get; set; } = -1;
        [JsonProperty("armors")] public float Armors { get; set; } = -1;
        [JsonProperty("lightCuirasses")] public float LightCuirasses { get; set; } = 2f;
        [JsonProperty("lightOthers")] public float LightOthers { get; set; } = -1;
        [JsonProperty("clothesBody")] public float ClothesBody { get; set; } = 2f;
        [JsonProperty("clothesFeet")] public float ClothesFeet { get; set; } = 2f;
        [JsonProperty("clothingCirclets")] public float ClothingCirclets { get; set; } = 0.5f;
        [JsonProperty("jewels")] public float Jewels { get; set; } = 0.1f;
        [JsonProperty("weapons")] public float Weapons { get; set; } = 0.33f;
        [JsonProperty("foods")] public float Foods { get; set; } = -1;
        [JsonProperty("winesBrandys")] public float WinesBrandys { get; set; } = 3f;
        [JsonProperty("alesMeads")] public float AlesMeads { get; set; } = 1.25f;
        [JsonProperty("breadsFlours")] public float BreadsFlours { get; set; } = 2f;
        [JsonProperty("vegetables")] public float Vegetables { get; set; } = 1.5f;
        [JsonProperty("soups")] public float Soups { get; set; } = 2;
        [JsonProperty("meats")] public float Meats { get; set; } = -1;
        [JsonProperty("seafoods")] public float SeaFoods { get; set; } = -1;
        [JsonProperty("drinks")] public float Drinks { get; set; } = 2f;
        [JsonProperty("potions")] public float Potions { get; set; } = -1;
        [JsonProperty("ingots")] public float Ingots { get; set; } = -1;
        [JsonProperty("gems")] public float Gems { get; set; } = 0.1f;
        [JsonProperty("tools")] public float Tools { get; set; } = -1;
        [JsonProperty("tents")] public float Tents { get; set; } = 2f;
        [JsonProperty("silverware")] public float Silverware { get; set; } = -1;
        [JsonProperty("emptyBottles")] public float EmptyBottles { get; set; } = 1f;
        [JsonProperty("animalPelts")] public float AnimalPelts { get; set; } = 7.5f;
        [JsonProperty("animalParts")] public float AnimalParts { get; set; } = -1;
        [JsonProperty("animalHides")] public float AnimalHides { get; set; } = 7.5f;
        [JsonProperty("clutter")] public float Clutter { get; set; } = -1;
        [JsonProperty("miscItems")] public float MiscItems { get; set; } = -1;
    }
}