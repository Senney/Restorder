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
	/// Interaction logic for OrderBillControl.xaml
	/// </summary>
	public partial class OrderBillControl : UserControl
	{
        public static OrderBillControl Selected;
        private Brush oldBg;

		public OrderBillControl(string seatName)
		{
			this.InitializeComponent();

            this.Seat.Text = seatName;
            this.SubTotal.Text = (0.0).ToString("C");

            if (seatName == "Table")
            {
                this.SeatBillGrid.Children.Remove(this.RemoveTable);
            }

            oldBg = this.bgFill.Fill;
		}

        public void reset()
        {
            this.bgFill.Fill = oldBg;
            MainWindow.selectedSeat = "Table";
        }

		private void handleClick(object sender, System.Windows.RoutedEventArgs e)
		{
            string name = this.Seat.Text;
			
			if (BillItemControl.Selected != null)
			{
				MenuItem item = BillItemControl.Selected.Item;
	
				if (item == null)
					return;
	
				BillItemControl.Selected.Remove();
				MainWindow.getTableManager().CurrentTable.removeItem(item);
				MainWindow.getTableManager().CurrentTable.addItem(item, name);
			}
			else 
			{
                if (OrderBillControl.Selected != null)
                {
                    OrderBillControl.Selected.reset();
                    if (OrderBillControl.Selected == this)
                        OrderBillControl.Selected = null;
                    else
                        this.select();
                }
                else
                {
                    this.select();
                }
			}
		}

        private void select()
        {
            this.bgFill.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 30));
            OrderBillControl.Selected = this;
            MainWindow.selectedSeat = this.Seat.Text;
        }

		private void removeSeat(object sender, System.Windows.RoutedEventArgs e)
		{
            string newName = "Table";

            // Add the items in this element to the table instead.
            for (int i = 0; i < ItemList.Children.Count; i++)
            {
                BillItemControl bic = (BillItemControl)ItemList.Children[i];
                MainWindow.getTableManager().CurrentTable.removeItem(bic.Item);
                MainWindow.getTableManager().CurrentTable.addItem(bic.Item, newName);
            }

            // Clear and remove old data.
            this.SeatBillGrid.Children.Clear();
            (this.Parent as StackPanel).Children.Remove(this);
            MainWindow.selectedSeat = "Table"; // Reset the seat.
		}
	}
}