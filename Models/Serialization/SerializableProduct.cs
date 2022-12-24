using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTaler.Models.Serialization {
	[Serializable]
	public class SerializableProduct {
		public string name = "";
		public string description = "";
		public double rating = 0;
		public string? image = "";
	}
}
