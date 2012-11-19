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

        // Table management singleton.
        private static TableManager tableManager;
        public static ItemButton selectedButton = null;
        public static SelectedItem selectedItem;
        public static string selectedSeat = "Table";

        private int seatID;

		public MainWindow()
		{
			this.InitializeComponent();

            // Add 12 tables to the manager.
            getTableManager().addTables(12);

            // Setup the item detail singleton.
            MainWindow.selectedItem = ItemDetail;

            // Set the initial seat id.
            seatID = 1;

            // Initialize the menu 
            menu = new Menu();
            createMenu();
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
            // Clear the old children.
            MenuStack.Children.Clear();
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

            this.TablePage.Visibility = System.Windows.Visibility.Hidden;
            this.SelectedTable.Visibility = System.Windows.Visibility.Visible;
            getTableManager().setCurrentTable(int.Parse((string)parent.Content));

            if (!getTableManager().CurrentTable.HasHandler)
            {
                getTableManager().CurrentTable.BillChanged += new BillChangedEventHandler(updateBill);
                getTableManager().CurrentTable.HasHandler = true;
            }

            displayMenu();
            setupTableBill(int.Parse((string)parent.Content));
		}

        private void updateBill(object sender, EventArgs e)
        {
            BillChangeArgs a = (BillChangeArgs)e;

            if (a.type == 0)
            {
                tableManager.CurrentTable.SeatControls[a.seat].ItemList.Children.Add(new BillItemControl(a.item));
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

                if (tableManager.CurrentTable.Bill.ContainsKey(entry.Key))
                {
                    List<MenuItem> items = tableManager.CurrentTable.Bill[entry.Key];
                    foreach (MenuItem i in items)
                    {
                        val += i.Cost;
                    }
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
            t.SeatControls.Clear();

            // Create seat for table.
            if (!t.Bill.ContainsKey("Table")) 
            {
                OrderBillControl tbc = new OrderBillControl("Table");
                this.TableBillStack.Children.Add(tbc);
                t.SeatControls.Add("Table", tbc);
            }

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

        private void addSeat(object sender, System.Windows.RoutedEventArgs e)
        {
            string seatName = "Seat " + (seatID++).ToString();
            OrderBillControl obc = new OrderBillControl(seatName);

            this.TableBillStack.Children.Add(obc);
            tableManager.CurrentTable.SeatControls.Add(seatName, obc);
        }

        private void backButton(object sender, System.Windows.RoutedEventArgs e)
        {
            this.TablePage.Visibility = System.Windows.Visibility.Visible;
            this.SelectedTable.Visibility = System.Windows.Visibility.Hidden;
        }
	}
}