using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;
using System.Windows;

namespace StoryTaler.Models.Serialization {
	[Serializable]
	public class SerializableYear {
		public int yearNumber;
		public List<SerializableProduct> games;
		public List<SerializableProduct> dropedGames;
		public List<SerializableProduct> books;
		public List<SerializableProduct> movies;
		public List<SerializableProduct> tvSeries;
		public List<SerializableProduct> education;
		public List<SerializableProduct> cities;
		public List<SerializableProduct> onlineGames;
		public SerializableYear() {
			yearNumber = DateTime.Now.Year;
			games = new List<SerializableProduct>();
			dropedGames = new List<SerializableProduct>();
			books = new List<SerializableProduct>();
			movies = new List<SerializableProduct>();
			tvSeries = new List<SerializableProduct>();
			education = new List<SerializableProduct>();
			cities = new List<SerializableProduct>();
			onlineGames = new List<SerializableProduct>();
		}
	}
}
