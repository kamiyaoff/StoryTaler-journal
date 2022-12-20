using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StoryTaler.Models
{
    public class Year
    {
		public Guid YearId { get; set; }
		public int YearNumber;
		public ObservableCollection<Product> Games { get; set; }
		public ObservableCollection<Product> DropedGames { get; set; }
		public ObservableCollection<Product> Books { get; set; }
		public ObservableCollection<Product> Movies { get; set; }
		public ObservableCollection<Product> TVSeries { get; set; }
		public ObservableCollection<Product> Education { get; set; }
		public ObservableCollection<Product> Cities { get; set; }
		public ObservableCollection<Product> OnlineGames { get; set; }
		public Year(int yearNumber) {
			YearId= Guid.NewGuid();
			YearNumber = yearNumber;
			Games =	new ObservableCollection<Product>();
			DropedGames= new ObservableCollection<Product>();
			Books = new ObservableCollection<Product>();
			Movies = new ObservableCollection<Product>();
			TVSeries = new ObservableCollection<Product>();
			Education = new ObservableCollection<Product>();
			Cities = new ObservableCollection<Product>();
			OnlineGames = new ObservableCollection<Product>();
		}
	}
}
