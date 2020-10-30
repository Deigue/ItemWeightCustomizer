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
        }

        public float getBookWeight()
        {
            return _weightSettings.books;
        }
    }
}
