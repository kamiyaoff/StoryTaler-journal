using StoryTaler.Models;
using StoryTaler.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using Windows.UI.WindowManagement;

namespace StoryTaler {
	public partial class MainWindow : Window {
		public MainWindowViewModel ViewModel { get; set; }
		private ObservableCollection<Year> Years { get; set; }

		private Year? selectedYear;
		public MainWindow() {
			InitializeComponent();
			ViewModel= new MainWindowViewModel();
			Years = new ObservableCollection<Year>();
			Years.Add(new Year(2011));
			Years.Add(new Year(2012));
			Years.Add(new Year(2013));
			ShowYears();
			AddNewYear(2077);
			FillYear();
		}

		private void FillYear() {
			foreach (Year item in Years) {
				for (int i = 0; i < 30; i++) {
					item.Games.Add(new Product() { Name = i + "SomeGame " + item.YearNumber });
					item.TVSeries.Add(new Product() { Name = i + "SomeSeries" + item.YearNumber });
				}
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
			};
			myYearsCollection.Children.Add(btn);
		}

		private void AddNewYear(int yearNumber) {
			Year year = new Year(yearNumber);
			Years.Add(year);
			GenerateYearButton(year);
			selectedYear = Years.First(y => yearNumber == y.YearNumber);
			currentYearTextBlock.Text = selectedYear.YearNumber.ToString();
			productList.ItemsSource = selectedYear.Games;
		}

		public void ShowYears() {
			foreach (Year year in Years) {
				GenerateYearButton(year);
			}
			selectedYear = Years.Last();
			currentYearTextBlock.Text = selectedYear.YearNumber.ToString();
			productList.ItemsSource = selectedYear.Games;
		}

		private void CategoryButton_Click(object sender, RoutedEventArgs e) {
			if (selectedYear == null) {
				MessageBox.Show("Chose year first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			switch (((Button)sender).Content.ToString()) {
				case "Games":
					productList.ItemsSource = selectedYear.Games;
					break;
				case "Dropped Games":
					productList.ItemsSource = selectedYear.DropedGames;
					break;
				case "Movies":
					productList.ItemsSource = selectedYear.Movies;
					break;
				case "TV Series":
					productList.ItemsSource = selectedYear.TVSeries;
					break;
				case "Books":
					productList.ItemsSource = selectedYear.Books;
					break;
				case "Education":
					productList.ItemsSource = selectedYear.Education;
					break;
				case "Cities":
					productList.ItemsSource = selectedYear.Cities;
					break;
				case "Online Games":
					productList.ItemsSource = selectedYear.OnlineGames;
					break;
				default:
					MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					break;
			}
		}

		private void AddYearButton_Click(object sender, RoutedEventArgs e) {
			MessageBoxResult result = MessageBox.Show("Are you sure you want to add new year?", 
				"Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.No) 
				return;
			Year newYear = new Year(Years.Max(y => y.YearNumber) + 1);
			Years.Add(newYear);
			GenerateYearButton(newYear);
			selectedYear = Years.First(y => newYear.YearNumber == y.YearNumber);
			currentYearTextBlock.Text = selectedYear.YearNumber.ToString();
			productList.ItemsSource = selectedYear.Games;
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
	}
}
