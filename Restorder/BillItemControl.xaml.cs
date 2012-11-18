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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restorder
{
	/// <summary>
	/// Interaction logic for BillItemControl.xaml
	/// </summary>
	public partial class BillItemControl : UserControl
	{
		private MenuItem item;
		
		public BillItemControl(MenuItem Item)
		{
			this.InitializeComponent();

            item = Item;
			
			// Set the UI elements.
            if (item != null)
            {
                this.ItemName.Text = item.Name;
                this.ItemTotal.Text = item.Cost.ToString("C");
            }
		}

		private void removeItem(object sender, System.Windows.RoutedEventArgs e)
		{
            // Remove the item from the tables' bill.
            MainWindow.getTableManager().CurrentTable.removeItem(item);

            // Remove this item from the stack panel.
            (this.Parent as StackPanel).Children.Remove(this);
		}
	}
}