using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    [JsonObject(ItemRequired = Required.Always)]
    public partial class Config
    {
        [JsonProperty("weightSettings")]
        public WeightSettings Weights { get; set; }
        
        [JsonProperty("categories")]
        public HashSet<WeightCategory> Categories { get; set; }

        [JsonConstructor]
        private Config(WeightSettings weights, HashSet<WeightCategory> categories)
        {
            Weights = weights;
            Categories = categories;
        }
    }
}