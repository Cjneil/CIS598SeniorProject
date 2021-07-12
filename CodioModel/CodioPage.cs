using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
    public class CodioPage
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("pageId")]
        public string PageId { get; set; }

        public CodioPage(string title, string id, string type, string pageId)
        {
            Title = title;
            Id = id;
            Type = type;
            PageId = pageId;
        }
    }
}
