using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
    class CodioMetadata
    {
		[JsonProperty("sections")]
		public List<CodioMetadataSection> Sections { get; set; }

		[JsonProperty("theme")]
		public string Theme { get; set; }

		[JsonProperty("scripts")]
		public List<object> Scripts { get; set; }

		[JsonProperty("lexikonTopic")]
		public string LexikonTopic { get; set; }

		[JsonProperty("suppressPageNumbering")]
		public bool SuppressPageNumbering { get; set; }

		[JsonProperty("useSubmitButtons")]
		public bool UseSubmitButtons { get; set; }

		[JsonProperty("useMarkAsComplete")]
		public bool UseMarkAsComplete { get; set; }

		[JsonProperty("hideMenu")]
		public bool HideMenu { get; set; }

		[JsonProperty("allowGuideClose")]
		public bool AllowGuideClose { get; set; }

		[JsonProperty("collapsedOnStart")]
		public bool CollapsedOnStart { get; set; }

		[JsonProperty("hideSectionsToggle")]
		public bool HideSectionsToggle { get; set; }

		[JsonProperty("hideBackToDashboard")]
		public bool HideBackToDashboard { get; set; }

		[JsonProperty("protectLayout")]
		public bool ProtectLayout { get; set; }

	}
}
