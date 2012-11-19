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
        private Brush oldBg;

        public MenuItem Item
        {
            get { return item; }
        }

        public static BillItemControl Selected = null;
		
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

            this.oldBg = this.bgColor.Fill;
		}

        public void Remove()
        {
            // Remove the item from the tables' bill.
            MainWindow.getTableManager().CurrentTable.removeItem(item);

            // Remove this item from the stack panel.
            (this.Parent as StackPanel).Children.Remove(this);

            // Nullify the singleton so that selecting OBC's does not attempt to move invalid item.
            BillItemControl.Selected = null;
        }

		private void removeItem(object sender, System.Windows.RoutedEventArgs e)
		{
            this.Remove();
		}

        public void reset()
        {
            this.bgColor.Fill = oldBg;
        }

		private void selectBillItem(object sender, System.Windows.RoutedEventArgs e)
		{
            if (OrderBillControl.Selected != null)
                OrderBillControl.Selected.reset();

            // Check if the current button has been pressed.
            if (BillItemControl.Selected == this)
            {
                this.reset();
                BillItemControl.Selected = null;
            }
            else
            {
                if (BillItemControl.Selected != null)
                    BillItemControl.Selected.reset();
                BillItemControl.Selected = this;

                this.bgColor.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 32));
            }
		}
	}
}