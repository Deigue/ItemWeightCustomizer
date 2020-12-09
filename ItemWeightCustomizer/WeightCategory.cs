using System.Collections.Generic;
using Newtonsoft.Json;

namespace ItemWeightCustomizer
{
    public class WeightCategory
    {
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
        
        [JsonProperty("types")]
        public HashSet<string> Types { get; set; }
        
        [JsonProperty("editorIds")]
        public HashSet<string> EditorIds { get; set; }
        
        [JsonConstructor]
        public WeightCategory(string name, HashSet<string> types, HashSet<string> editorIds)
        {
            Name = name;
            Types = types;
            EditorIds = editorIds;
        }
    }
}