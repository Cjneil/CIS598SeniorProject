using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
    /// <summary>
    /// Top-Level directory within a Codio Book. 
    /// Has CodioChildrenElements as children which can be CodioSections or CodioPages
    /// </summary>
    public class CodioChapter
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("children")]
        public List<CodioChildElement> Children { get; set; }

    }
}
