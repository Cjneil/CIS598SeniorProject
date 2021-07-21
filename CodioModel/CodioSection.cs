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
        /// <summary>
        /// List of children of the CodioSection. Can be CodioSections or CodioPages but not CodioChapters or CodioBook
        /// </summary>
        [JsonProperty("children")]
        public List<CodioChildElement> Children { get; set; }

        /// <summary>
        /// The type of the ChildElement. Either "section" or "page"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
