using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public partial class Config
    {
        [JsonProperty("weightSettings", Required = Required.Always)]
        public WeightSettings Weights { get; set; }

        [JsonConstructor]
        private Config(WeightSettings weights)
        {
            Weights = weights;
        }
    }
}