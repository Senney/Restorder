﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restorder
{
	/// <summary>
	/// Interaction logic for SelectedItem.xaml
	/// </summary>
	public partial class SelectedItem : UserControl
	{
		public SelectedItem()
		{
            this.InitializeComponent();
		}

        public void setItem(MenuItem item)
        {
            this.FoodName.Text = item.Name;
            this.Description.Text = item.Descriptor;
            this.itemPrice.Text = item.Cost.ToString("C");
           
            this.ingredients.Text = "Ingredients\n";
            foreach (string ing in item.Ingredients)
            {
                this.ingredients.Text += ing + "\n";
            }
        }

        private void addToOrder(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MainWindow.selectedButton != null)
            {
                MainWindow.selectedButton.addToOrder();
                MainWindow.selectedButton.reset();
            }
        }
	}
}