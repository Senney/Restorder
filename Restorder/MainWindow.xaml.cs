using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restorder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        Menu menu;
        TableManager tableManager;

		public MainWindow()
		{
			this.InitializeComponent();

            // Initialize the menu 
            tableManager = new TableManager(12);
            menu = new Menu();

            createMenu();

            tableManager.getTable(0).addItem(menu.getItem("Chicken Alfredo"), "Sean");
            tableManager.getTable(0).addItem(menu.getItem("T-Bone Steak"), "Sean");
            tableManager.getTable(0).addItem(menu.getItem("Sirloin Steak"), "Yosuke");
            setupTableBill(0);
		}

        private void createMenu()
        {
            List<MenuItem> pastas = new List<MenuItem>();
            pastas.Add(new MenuItem("Chicken Alfredo", 8.99));
            pastas.Add(new MenuItem("Seafood Pasta", 11.45));
            pastas.Add(new MenuItem("Ravioli", 10.50));
            pastas.Add(new MenuItem("Lasagna", 8.99));

            List<MenuItem> steaks = new List<MenuItem>();
            steaks.Add(new MenuItem("T-Bone Steak", 24.99));
            steaks.Add(new MenuItem("Sirloin Steak", 20.99));

            List<MenuItem> veggie = new List<MenuItem>();
            veggie.Add(new MenuItem("Garden Salad", 7.50));
            veggie.Add(new MenuItem("Caesar Salad", 8.50));
            veggie.Add(new MenuItem("Tofu Explosion", 10.99));

            menu.addItems("Pasta", pastas);
            menu.addItems("Steak", steaks);
            menu.addItems("Vegitarian", veggie);
        }

		private void openTableManager(object sender, System.Windows.RoutedEventArgs e)
		{
			Button parent = (Button)sender;
			MessageBox.Show("Table handler for " + parent.Content);
		}

        private void setupTableBill(int table)
        {
            Table t = tableManager.getTable(table);

            // Loop over each person in the keys.
            foreach (string person in t.Bill.Keys)
            {
                TextBlock personBlock = new TextBlock();
                personBlock.Text = person;
                this.OrderBill.Children.Add(personBlock);

                // Get the items that are attributed to that person.
                List<MenuItem> items = t.Bill[person];
                foreach (MenuItem i in items)
                {
                    // Add the new item to the bill.
                    this.OrderBill.Children.Add(new BillItemControl(i, t));
                }
            }
        }
	}
}