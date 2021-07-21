using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
    /// <summary>
    /// Abstract element representing children of a chapter. 
    /// This is extended by CodioPage and CodioSection hence the subtyping to make json work
    /// This uses JsonSubTypes found at https://github.com/manuc66/JsonSubTypes or in the NuGet Package Manager
    /// </summary>
    [JsonConverter(typeof(JsonSubtypes), "type")]
    [JsonSubtypes.KnownSubType(typeof(CodioSection), "section")]
    [JsonSubtypes.KnownSubType(typeof(CodioPage), "page")]
    public abstract class CodioChildElement
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
