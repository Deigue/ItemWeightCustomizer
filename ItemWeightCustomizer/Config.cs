using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public class Config
    {
        [JsonProperty("weightSettings")]
        private WeightSettings Weights { get; set; }

        [JsonConstructor]
        private Config(WeightSettings weights)
        {
            Weights = weights;
        }

        internal class WeightSettings
        {
            [JsonProperty("books")]
            public float Books { get; set; } = -1;

            [JsonProperty("ingredients")]
            public float Ingredients { get; set; } = -1;

            [JsonProperty("scrolls")]
            public float Scrolls { get; set; } = -1;

            [JsonProperty("soulgems")]
            public float Soulgems { get; set; } = -1;

        }

        public float GetBookWeight()
        {
            return Weights.Books;
        }

        public float GetIngredientWeight()
        {
            return Weights.Ingredients;
        }

        public float GetScrollWeight()
        {
            return Weights.Scrolls;
        }

        public float GetSoulGemWeight()
        {
            return Weights.Soulgems;
        }

    }
}
