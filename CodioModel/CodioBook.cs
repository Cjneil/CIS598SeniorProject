using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
    /// <summary>
    /// Model representing the entire book as a whole. Chapters make up the top-level within it and that's about it.
    /// </summary>
    public class CodioBook
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("children")]
        public List<CodioChapter> Chapters { get; set; }
    }
}
