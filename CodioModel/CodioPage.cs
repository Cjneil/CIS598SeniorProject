using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
    public class CodioPage : CodioChildElement
    {
        /// <summary>
        /// The unique identifier for the page within the book
        /// </summary>
        [JsonProperty("pageId")]
        public string PageId { get; set; }
    }
}
