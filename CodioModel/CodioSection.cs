using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodioToHugoConverter.CodioModel
{
	
    public class CodioSection
    {
		[JsonProperty("id")]
		public string Id { get; set; }
		[JsonProperty("title")]
		public string Title { get; set; }
		[JsonProperty("files")]
		public List<object> Files { get; set; }
		[JsonProperty("path")]
		public List<object> Path { get; set; }
		[JsonProperty("type")]
		public string Type { get; set; }
		[JsonProperty("content-file")]
		public string ContentFile { get; set; }
		[JsonProperty("chapter")]
		public bool Chapter { get; set; }
		[JsonProperty("reset")]
		public List<object> Reset { get; set; }
		[JsonProperty("teacherOnly")]
		public bool TeacherOnly { get; set; }
		[JsonProperty("learningObjectives")]
		public string LearningObjectives { get; set; }
	}
}
