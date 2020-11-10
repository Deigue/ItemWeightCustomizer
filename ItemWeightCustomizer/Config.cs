using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItemWeightCustomizer
{
    public class Config
    {
        [JsonProperty("weightSettings")]
        private WeightSettings _weightSettings { get; set; }

        [JsonConstructor]
        private Config(WeightSettings weightSettings)
        {
            _weightSettings = weightSettings;
        }

        internal class WeightSettings
        {
            [JsonProperty("books")]
            public float books { get; set; } = -1;

            [JsonProperty("ingredients")]
            public float ingredients { get; set; } = -1;

            [JsonProperty("scrolls")]
            public float scrolls { get; set; } = -1;



        }

        public float getBookWeight()
        {
            return _weightSettings.books;
        }

        public float getIngredientWeight()
        {
            return _weightSettings.ingredients;
        }

        public float getScrollWeight()
        {
            return _weightSettings.scrolls;
        }
    }
}
