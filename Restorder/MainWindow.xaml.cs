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
using System.Windows.Shapes;

namespace Restorder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        Menu menu;

        // Table management singleton.
        private static TableManager tableManager;
        public static ItemButton selectedButton = null;
        public static SelectedItem selectedItem;

		public MainWindow()
		{
			this.InitializeComponent();

            // Add 12 tables to the manager.
            getTableManager().addTables(12);

            MainWindow.selectedItem = ItemDetail;

            // Initialize the menu 
            menu = new Menu();

            createMenu();
            displayMenu();
            updateItemDescription(menu.getItem("Chicken Alfredo"));
            tableManager.getTable(0).addItem(menu.getItem("Chicken Alfredo"), "Sean");
            tableManager.getTable(0).addItem(menu.getItem("T-Bone Steak"), "Sean");
            tableManager.getTable(0).addItem(menu.getItem("Sirloin Steak"), "Yosuke");
            tableManager.setCurrentTable(0);

            getTableManager().CurrentTable.BillChanged += new BillChangedEventHandler(updateBill);
            setupTableBill(0);

		}

        /// <summary>
        /// Singleton accessor for TableManager.
        /// </summary>
        /// <returns>TableManager singleton.</returns>
        public static TableManager getTableManager()
        {
            if (tableManager == null)
                tableManager = new TableManager();

            return tableManager;
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

        private void displayMenu()
        {
            foreach (string category in menu.MenuDict.Keys)
            {
                MenuExpander section = new MenuExpander();
                section.Header.Text = category;
                foreach (MenuItem item in menu.MenuDict[category])
                {
                    ItemButton iButton = new ItemButton(item);
                    iButton.ItemName.Text = item.Name;
                    iButton.ItemPrice.Text = item.Cost.ToString("C");

                    section.Children.Children.Add(iButton);
                }
                MenuStack.Children.Add(section);
            }
             
        }

        private void updateItemDescription(MenuItem item)
        {
            ItemDetail.FoodName.Text = item.Name;
        }

		private void openTableManager(object sender, System.Windows.RoutedEventArgs e)
		{
			Button parent = (Button)sender;
			MessageBox.Show("Table handler for " + parent.Content);
            getTableManager().setCurrentTable(int.Parse((string)parent.Content));
            getTableManager().CurrentTable.BillChanged += new BillChangedEventHandler(updateBill);
		}

        private void updateBill(object sender, EventArgs e)
        {
            BillChangeArgs a = (BillChangeArgs)e;

            if (a.type == 0)
            {
                TableBillStack.Children.Add(new BillItemControl((e as BillChangeArgs).item));
            }

            updateTotals();
        }

        private void updateTotals()
        {
            this.subTotalDisplay.Text = tableManager.CurrentTable.SubTotal.ToString("C");
            this.totalDisplay.Text = tableManager.CurrentTable.Total.ToString("C");
            this.taxDisplay.Text = tableManager.CurrentTable.Tax.ToString("C");

            foreach (KeyValuePair<string, OrderBillControl> entry in tableManager.CurrentTable.SeatControls)
            {
                double val = 0.0;
                foreach (MenuItem i in tableManager.CurrentTable.Bill[entry.Key])
                {
                    val += i.Cost;
                }
                entry.Value.SubTotal.Text = val.ToString("C");
            }
        }

        private void setupTableBill(int table)
        {
            // Remove all children.
            this.TableBillStack.Children.Clear();

            // Get the table that we want to set up.
            Table t = tableManager.getTable(table);

            // Loop over each person in the keys.
            foreach (string person in t.Bill.Keys)
            {
                OrderBillControl obc = new OrderBillControl(person);

                // Get the items that are attributed to that person.
                List<MenuItem> items = t.Bill[person];

                double total = 0;
                foreach (MenuItem i in items)
                {
                    // Add the new item to the bill.
                    obc.ItemList.Children.Add(new BillItemControl(i));
                    total += i.Cost;
                }

                obc.SubTotal.Text = total.ToString("C");
                this.TableBillStack.Children.Add(obc);
                t.SeatControls.Add(person, obc);
            }

            updateTotals();
        }
	}
}