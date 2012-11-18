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
	/// Interaction logic for OrderBillControl.xaml
	/// </summary>
	public partial class OrderBillControl : UserControl
	{
		public OrderBillControl(string seatName)
		{
			this.InitializeComponent();

            this.Seat.Text = seatName;
            this.SubTotal.Text = (0.0).ToString("C");
		}
	}
}