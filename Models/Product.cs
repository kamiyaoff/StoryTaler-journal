using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StoryTaler.Models
{
    public class Product : INotifyPropertyChanged
    {

        private string _name = string.Empty;
        public string Name {
            get => _name;
            set {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _description = string.Empty;
        public string Description {
			get => _description;
			set {
				_description = value;
				OnPropertyChanged();
			}
		}

		private double _rating;
        public double Rating {
			get => _rating;
			set {
				_rating = value;
				OnPropertyChanged();
			}
		}
		
        private string? _image = null;
		public string? Image {
			get => _image;
			set {
				_image = value;
				OnPropertyChanged();
			}
		}

		public Product() {
            Name = "Some Product";
            Description = "Some Description";
            Rating = 1.5;
            Image = null;
        }

		public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string propertyName = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
