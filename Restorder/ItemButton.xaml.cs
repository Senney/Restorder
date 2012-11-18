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

		private void overlayClick(object sender, System.Windows.RoutedEventArgs e)
		{
            MainWindow.getTableManager().CurrentTable.addItem(menuItem);
		}
	}
}