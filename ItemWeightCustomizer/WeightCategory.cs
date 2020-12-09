using System.Collections.Generic;
using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public class WeightCategory
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty("types")] public HashSet<string> Types { get; set; } = new HashSet<string>();

        [JsonProperty("editorIds")] public HashSet<string> EditorIds { get; set; } = new HashSet<string>();

        [JsonProperty("weight")] public float Weight { get; set; } = -1;

        [JsonConstructor]
        public WeightCategory(string name)
        {
            Name = name;
        }
    }
}