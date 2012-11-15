using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace Restorder
{
    class MenuItem
    {
        private string itemName;
        public string Name
        {
            get { return this.itemName; }
            set { this.itemName = value; }
        }

        private float itemCost;
        public float Cost
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

        public MenuItem(string name, float cost, string picture = null, string[] ingredients = null)
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
    }
}
