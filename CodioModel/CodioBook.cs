using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
    public class CodioBook
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("children")]
        public List<CodioChapter> Chapters { get; set; }
    }
}
