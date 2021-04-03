using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public partial class Settings
    {
        [JsonProperty("weightSettings")]
        public WeightSettings WeightSettings { get; set; } = new WeightSettings();

        [JsonProperty("categories")]
        public HashSet<WeightCategory> Categories { get; set; } = new HashSet<WeightCategory>();
    }
}