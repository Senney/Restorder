using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace Restorder
{
    public class MenuItem : IEquatable<MenuItem>
    {
        private string itemName;
        public string Name
        {
            get { return this.itemName; }
            set { this.itemName = value; }
        }

        private double itemCost;
        public double Cost
        {
            get { return this.itemCost; }
            set { this.itemCost = value; }
        }

        private List<string> itemIngredients;
        public List<string> Ingredients
        {
            get { return itemIngredients; }
        }

        private Image itemPicture;
        public Image Picture
        {
            get { return itemPicture; }
        }

        private int m_id;
        public int ID
        {
            get { return m_id; }
            set { m_id = value; }
        }

        public MenuItem(string name, double cost, string picture = null, string[] ingredients = null)
        {
            this.itemName = name;
            this.itemCost = cost;

            if (picture != null)
            {
                if (System.IO.File.Exists(picture))
                    this.itemPicture = Image.FromFile(picture);
                else
                    throw new System.IO.FileNotFoundException("Could not locate image: " + picture);
            }

            this.itemIngredients = new List<string>();

            if (ingredients != null)
                this.itemIngredients.AddRange(ingredients);
        }


        public bool Equals(MenuItem other)
        {
            return (other.Name == this.Name && other.Cost == this.Cost && other.ID == this.ID);
        }
    }
}
