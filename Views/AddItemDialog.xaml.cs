using Microsoft.Win32;
using StoryTaler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StoryTaler.Views
{
    public partial class AddItemDialog : Window
    {
        Product? product = null;

		public AddItemDialog()
        {
            InitializeComponent();
        }

        public AddItemDialog(Product targetProduct) : this() {
            productName.Text = targetProduct.Name;
            productDesc.Text = targetProduct.Description;
            productImage.Text = targetProduct.Image ?? "";
            productRating.SelectedIndex = (int)targetProduct.Rating * 1;
        }

		private void BrowseImageBtn_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Images and Pictures|*.jpg;*.png;*.jpeg";
            bool? result = dlg.ShowDialog();
            if (result == true) {
                productImage.Text = dlg.FileName;
            }
		}

		private void OkBtn_Click(object sender, RoutedEventArgs e) {
            product = new Product();
            product.Name = productName.Text;
            ComboBoxItem typeItem = (ComboBoxItem)productRating.SelectedItem;
			product.Rating = Convert.ToDouble(typeItem.Content);
            product.Description = productDesc.Text;
            if (productImage.Text != "") {
                product.Image = productImage.Text;
            }
            if (productImage.Text == "") {
                product.Image = null;
            }
			this.DialogResult = true;
		}

        public Product? ReturnNewProduct() {
            if (product != null)
                return product;
            return null;
        }

		//public Product ReturnCar()
	}
}
