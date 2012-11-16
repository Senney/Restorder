using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restorder
{
    public class Table
    {
        static double TAX = 1.05;

        private Dictionary<string, List<MenuItem>> tableBill;
        public Dictionary<string, List<MenuItem>> Bill
        {
            get { return tableBill; }
        }

        private int tableID;

        private double total;
        public double SubTotal
        {
            get { return total; }
        }

        public double Total
        {
            get { return total * TAX; }
        }

        public Table(int tableid)
        {
            this.tableID = tableid;
            this.tableBill = new Dictionary<string,List<MenuItem>>();
        }

        public void addItem(MenuItem item, string person = "Table")
        {
            if (!tableBill.ContainsKey(person))
                tableBill[person] = new List<MenuItem>();

            tableBill[person].Add(item);
            total += item.Cost;
        }

        public void removeItem(MenuItem item)
        {
            // Loop through all the entries in the dictionary and find the item that we want to remove.
            foreach (KeyValuePair<string, List<MenuItem>> entry in tableBill)
            {
                if (item.Equals(entry.Value))
                {
                    // Remove the item from the bill and subtract the cost.
                    tableBill[entry.Key].Remove(item);
                    total -= item.Cost;
                }
            }
        }

        public string getReceipt()
        {
            string receipt = "";

            receipt += "RESTORDER BILL\n\n";

            foreach (string person in tableBill.Keys.ToList())
            {
                receipt += person + "\n" + "====================\n";
                double subtotal = 0.0;

                foreach (MenuItem item in tableBill[person])
                {
                    subtotal += item.Cost;
                    receipt += item.Name + "\t\t" + item.Cost.ToString("C") + "\n"; // Convert to curency.
                }

                receipt += "Subtotal:\t" + subtotal.ToString("C") + "\n";
                receipt += "Tax:\t\t" + (subtotal * (TAX - 1.0f)).ToString("C") + "\n";
                receipt += "Total:\t\t" + (subtotal * TAX).ToString("C") + "\n";
            }

            receipt += "=-==-==-==-==-==-==-=\n";
            receipt += "Subtotal:\t" + SubTotal.ToString("C") + "\n";
            receipt += "Tax:\t\t" + (SubTotal * (TAX - 1.0f)).ToString("C") + "\n";
            receipt += "Total:\t\t" + Total.ToString("C") + "\n";

            return receipt;
        }
    }
}
