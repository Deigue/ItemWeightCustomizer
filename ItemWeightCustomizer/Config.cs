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
        
        [JsonProperty("categorization")]
        public HashSet<WeightCategory> Categorizations { get; set; }

        [JsonConstructor]
        private Config(WeightSettings weights, HashSet<WeightCategory> categorizations)
        {
            Weights = weights;
            Categorizations = categorizations;
        }
    }
}