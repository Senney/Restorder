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
	/// Interaction logic for ItemButton.xaml
	/// </summary>
	public partial class ItemButton : UserControl
	{
        private MenuItem menuItem;

		public ItemButton(MenuItem item)
		{
			this.InitializeComponent();

            menuItem = item;
		}

        public void reset()
        {
            this.bgRect.Fill = new SolidColorBrush(Color.FromRgb(244, 244, 245));
            MainWindow.selectedButton = null;
        }

        public void addToOrder()
        {
            MainWindow.getTableManager().CurrentTable.addItem(menuItem, MainWindow.selectedSeat);
        }

		private void overlayClick(object sender, System.Windows.RoutedEventArgs e)
		{
            // Check if the current button has been pressed.
            if (MainWindow.selectedButton == this)
            {
                this.addToOrder();
                this.reset();
            }
            else
            {
                if (MainWindow.selectedButton != null)
                    MainWindow.selectedButton.reset();
                MainWindow.selectedButton = this;
                MainWindow.selectedItem.setItem(menuItem);
                this.bgRect.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 32));
            }
		}
	}
}