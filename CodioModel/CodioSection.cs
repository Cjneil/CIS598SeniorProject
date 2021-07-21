using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
    public class CodioSection : CodioChildElement
    {
        [JsonProperty("children")]
        public List<CodioChildElement> Children { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
