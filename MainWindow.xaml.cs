using StoryTaler.Models;
using StoryTaler.Models.Serialization;
using StoryTaler.ViewModels;
using StoryTaler.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;
using Windows.UI.WindowManagement;

namespace StoryTaler {
	public partial class MainWindow : Window {
		public MainWindowViewModel ViewModel { get; set; }
		private ObservableCollection<Year> Years { get; set; } = new ObservableCollection<Year>();

		private Year? selectedYear;
		public MainWindow() {
			InitializeComponent();
			ViewModel = new MainWindowViewModel();
			ApplicationRecoverData();
			Application.Current.Exit += ApplicationSaveData;
			productList.SelectionChanged += ProductList_SelectionChanged;
			this.Loaded += ShowYears;
		}

#pragma warning disable SYSLIB0011
		private void ApplicationSaveData(object sender, ExitEventArgs e) {
			IFormatter formatter = new BinaryFormatter();
			using Stream stream = new FileStream("userdata.bin", FileMode.Create, FileAccess.Write, FileShare.None);
			List<SerializableYear> data = new List<SerializableYear>();
			string content = "";
			foreach (var year in Years) {
				var temp = new SerializableYear {
					yearNumber = year.YearNumber
				};
				content += $"\n\n\nYear: {year.YearNumber}\n\tGames\n";
				foreach (var product in year.Games) {
					temp.games.Add(new SerializableProduct {
						name = product.Name,
						description = product.Description,
						rating = product.Rating,
						image = product.Image
					});
					content += $"\t\t{product.Name} [{product.Rating}/10]\n";
				}
				content += "\n\n\tBooks\n";
				foreach (var product in year.Books) {
					temp.books.Add(new SerializableProduct {
						name = product.Name,
						description = product.Description,
						rating = product.Rating,
						image = product.Image
					});
					content += $"\t\t{product.Name} [{product.Rating}/10]\n";
				}
				content += "\n\n\tMovies\n";
				foreach (var product in year.Movies) {
					temp.movies.Add(new SerializableProduct {
						name = product.Name,
						description = product.Description,
						rating = product.Rating,
						image = product.Image
					});
					content += $"\t\t{product.Name} [{product.Rating}/10]\n";
				}
				content += "\n\n\tDroped Games\n";
				foreach (var product in year.DropedGames) {
					temp.dropedGames.Add(new SerializableProduct {
						name = product.Name,
						description = product.Description,
						rating = product.Rating,
						image = product.Image
					});
					content += $"\t\t{product.Name} [{product.Rating}/10]\n";
				}
				content += "\n\n\tOnline Games\n";
				foreach (var product in year.OnlineGames) {
					temp.onlineGames.Add(new SerializableProduct {
						name = product.Name,
						description = product.Description,
						rating = product.Rating,
						image = product.Image
					});
					content += $"\t\t{product.Name} [{product.Rating}/10]\n";
				}
				content += "\n\n\tCities\n";
				foreach (var product in year.Cities) {
					temp.cities.Add(new SerializableProduct {
						name = product.Name,
						description = product.Description,
						rating = product.Rating,
						image = product.Image
					});
					content += $"\t\t{product.Name} [{product.Rating}/10]\n";
				}
				content += "\n\n\tEducation\n";
				foreach (var product in year.Education) {
					temp.education.Add(new SerializableProduct {
						name = product.Name,
						description = product.Description,
						rating = product.Rating,
						image = product.Image
					});
					content += $"\t\t{product.Name} [{product.Rating}/10]\n";
				}
				content += "\n\nTVSeries\n";
				foreach (var product in year.TVSeries) {
					temp.tvSeries.Add(new SerializableProduct {
						name = product.Name,
						description = product.Description,
						rating = product.Rating,
						image = product.Image
					});
					content += $"\t\t{product.Name} [{product.Rating}/10]\n";
				}
				data.Add(temp);
				using StreamWriter writer = File.CreateText("userdata.txt");
				writer.Write(content);
			}
			formatter.Serialize(stream, data);
		}

		private void ApplicationRecoverData() {
			try {
				BinaryFormatter formatter = new BinaryFormatter();
				using Stream stream = new FileStream("userdata.bin", FileMode.Open, FileAccess.Read);
				List<SerializableYear>? data = formatter.Deserialize(stream) as List<SerializableYear>;
				if (data != null) {
					foreach (var year in data) {
						var temp = new Year {
							YearNumber = year.yearNumber
						};
						foreach (var product in year.games) {
							temp.Games.Add(new Product {
								Name = product.name,
								Description = product.description,
								Rating = product.rating,
								Image = product.image
							});
						}
						foreach (var product in year.books) {
							temp.Books.Add(new Product {
								Name = product.name,
								Description = product.description,
								Rating = product.rating,
								Image = product.image
							});
						}
						foreach (var product in year.movies) {
							temp.Movies.Add(new Product {
								Name = product.name,
								Description = product.description,
								Rating = product.rating,
								Image = product.image
							});
						}
						foreach (var product in year.dropedGames) {
							temp.DropedGames.Add(new Product {
								Name = product.name,
								Description = product.description,
								Rating = product.rating,
								Image = product.image
							});
						}
						foreach (var product in year.onlineGames) {
							temp.OnlineGames.Add(new Product {
								Name = product.name,
								Description = product.description,
								Rating = product.rating,
								Image = product.image
							});
						}
						foreach (var product in year.cities) {
							temp.Cities.Add(new Product {
								Name = product.name,
								Description = product.description,
								Rating = product.rating,
								Image = product.image
							});
						}
						foreach (var product in year.education) {
							temp.Education.Add(new Product {
								Name = product.name,
								Description = product.description,
								Rating = product.rating,
								Image = product.image
							});
						}
						foreach (var product in year.tvSeries) {
							temp.TVSeries.Add(new Product {
								Name = product.name,
								Description = product.description,
								Rating = product.rating,
								Image = product.image
							});
						}
						Years.Add(temp);
					}
					return;
				}
			}
			catch {
				MessageBox.Show("Data was corrupted, unable to upload data.", "Error",
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Product item = (Product)productList.SelectedItem;
			if (item?.Image != null) {
				Uri imageUri = new Uri(item.Image);
				BitmapImage image = new BitmapImage(imageUri);
				myImage.Source = image;
				return;
			}
			myImage.Source = null;
		}

		private void ProductInfoStyleChange(object sender, SelectionChangedEventArgs e) {
			double rating;
			try {
				rating = Convert.ToDouble(productRatingBegin.Text);
			}
			catch {
				return;
			}
			if (rating < 8 && rating >= 6) {
				productRatingBegin.Style = (Style)Application.Current.FindResource("productRatingStyle2");
				productRatingEnd.Style = (Style)Application.Current.FindResource("productRatingStyle2");
			}
			else if (rating < 6) { 
				productRatingBegin.Style = (Style)Application.Current.FindResource("productRatingStyle3");
				productRatingEnd.Style = (Style)Application.Current.FindResource("productRatingStyle3");
			}
			else {
				productRatingBegin.Style = (Style)Application.Current.FindResource("productRatingStyle");
				productRatingEnd.Style = (Style)Application.Current.FindResource("productRatingStyle");
			}

		}

		private void GenerateYearButton(Year year) {
			Button btn = new Button {
				Style = (Style)Application.Current.FindResource("YearsButtonStyle"),
				Content = year.YearNumber,
				DataContext = year
			};
			btn.Click += delegate {
				mainContentTemplate.DataContext = btn.DataContext;
				productList.ItemsSource = year.Games;
				selectedYear = year;
				currentYearTextBlock.Text = selectedYear.YearNumber.ToString();
				selectedCategoryText.Text = "Games";
			};
			myYearsCollection.Children.Add(btn);
		}

		private void ShowYears(object sender, RoutedEventArgs e) {
			foreach (Year year in Years) {
				GenerateYearButton(year);
			}
			UpdateYearsList();
			if (Years.Count == 0) {
				return;
			}
			selectedYear = Years.Last();
			currentYearTextBlock.Text = selectedYear.YearNumber.ToString();
			productList.ItemsSource = selectedYear.Games;
			selectedCategoryText.Text = "Games";
		}

		private void UpdateYearsList() {
			if (Years.Count == 0)
				yearEmptyTextBlock.Visibility = Visibility.Visible;
			else
				yearEmptyTextBlock.Visibility = Visibility.Collapsed;
			if (((ObservableCollection<Product>)productList.ItemsSource)?.Count == 0)
				yearIsEmptyTextBlock.Visibility = Visibility.Visible;
			else
				yearIsEmptyTextBlock.Visibility = Visibility.Collapsed;
		}

		private void CategoryButton_Click(object sender, RoutedEventArgs e) {
			if (selectedYear == null) {
				MessageBox.Show("Chose year first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			switch (((Button)sender).Content.ToString()) {
				case "Games":
					productList.ItemsSource = selectedYear.Games;
					selectedCategoryText.Text = "Games";
					break;
				case "Dropped Games":
					productList.ItemsSource = selectedYear.DropedGames;
					selectedCategoryText.Text = "Dropped Games";
					break;
				case "Movies":
					productList.ItemsSource = selectedYear.Movies;
					selectedCategoryText.Text = "Movies";
					break;
				case "TV Series":
					productList.ItemsSource = selectedYear.TVSeries;
					selectedCategoryText.Text = "TV Series / Anime";
					break;
				case "Books":
					productList.ItemsSource = selectedYear.Books;
					selectedCategoryText.Text = "Books";
					break;
				case "Education":
					productList.ItemsSource = selectedYear.Education;
					selectedCategoryText.Text = "Education";
					break;
				case "Cities":
					productList.ItemsSource = selectedYear.Cities;
					selectedCategoryText.Text = "Cities";
					break;
				case "Online Games":
					productList.ItemsSource = selectedYear.OnlineGames; 
					selectedCategoryText.Text = "Online Games";
					break;
				default:
					MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
			}
			UpdateYearsList();
		}

		private void AddYearButton_Click(object sender, RoutedEventArgs e) {
			MessageBoxResult result = MessageBox.Show("Are you sure you want to add new year?", 
				"Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.No) return;
			Year newYear;
			if (Years.Count == 0) {
				newYear = new Year(DateTime.Now.Year);
			}
			else
				newYear = new Year(Years.Max(y => y.YearNumber) + 1);
			Years.Add(newYear);
			GenerateYearButton(newYear);
			selectedYear = Years.First(y => newYear.YearNumber == y.YearNumber);
			currentYearTextBlock.Text = selectedYear.YearNumber.ToString();
			productList.ItemsSource = selectedYear.Games;
			selectedCategoryText.Text = "Games";
			UpdateYearsList();
		}

		private void DeleteYearButton_Click(object sender, RoutedEventArgs e) {
			if (selectedYear != null) {
				MessageBoxResult result = MessageBox.Show("Are you sure you want to delete year?",
					"Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.No) return;
				result = MessageBox.Show("ARE YOU ABSOLUTELY SURE YOU WANT TO DELETE THIS YEAR?",
					"Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.No) return;
				Years.Remove(selectedYear);
				for (int i = 0; i < myYearsCollection.Children.Count; i++) {
					if (myYearsCollection.Children[i] is Button btn && (int)btn.Content == selectedYear.YearNumber) {
						myYearsCollection.Children.Remove(btn);
						selectedYear = null;
						UpdateYearsList();
						return;
					}
				}
			}
			MessageBox.Show("Year is not selected.",
					"Warning", MessageBoxButton.OK, MessageBoxImage.Question);
		}

		private void DateTimeTextBlock_Loaded(object sender, RoutedEventArgs e) {
			dateTimeTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
			DispatcherTimer dispatcherTimer = new DispatcherTimer();
			dispatcherTimer.Interval = TimeSpan.FromSeconds(20);
			dispatcherTimer.Tick += delegate {
				dateTimeTextBlock.Text = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
			};
			dispatcherTimer.Start();
		}

		private void AddItemButton_Click(object sender, RoutedEventArgs e) {
			if (productList.ItemsSource == null) { 
				MessageBox.Show("Unable to add item to list. Year or category not selected.",
					"Error", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			AddItemDialog dialog = new AddItemDialog();
			dialog.Owner = this;
			dialog.Title = "Add item";
			dialog.Icon = this.Icon;
			bool? result = dialog.ShowDialog();
			if (result == true) { 
				Product? newProduct = dialog.ReturnNewProduct();
				var category = productList.ItemsSource as ObservableCollection<Product>;
				if (newProduct != null) {
					category?.Add(newProduct);
					UpdateYearsList();
					return;
				}
			}
		}

		private void EditItemButton_Click(object sender, RoutedEventArgs e) {
			if (productList.SelectedItem == null) {
				MessageBox.Show("Select item from list.",
					"Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			Product myItem = (Product)productList.SelectedItem;
			AddItemDialog dialog = new AddItemDialog(myItem);
			dialog.Owner = this;
			dialog.Title = "Edit item";
			dialog.Icon = this.Icon;
			bool? result = dialog.ShowDialog();
			if (result == true) {
				Product? newProduct = dialog.ReturnNewProduct();
				var category = productList.ItemsSource as ObservableCollection<Product>;
				if (newProduct != null) {
					myItem.Name = newProduct.Name;
					myItem.Description = newProduct.Description;
					myItem.Image = newProduct.Image;
					myItem.Rating = newProduct.Rating;
					UpdateYearsList();
					return;
				}
			}
			
		}

		private void DeleteItemButton_Click(object sender, RoutedEventArgs e) {
			var product = productList.SelectedItem as Product;
			var products = productList.ItemsSource as ObservableCollection<Product>;
			if (product != null)
				products?.Remove(product);
			UpdateYearsList();
		}
	}
}