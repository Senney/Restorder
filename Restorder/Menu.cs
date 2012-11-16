using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restorder
{
    public class Menu
    {
        private Dictionary<string, List<MenuItem>> m_menu;
        public Dictionary<string, List<MenuItem>> MenuDict
        {
            get { return m_menu; }
        }

        public Menu()
        {
            m_menu = new Dictionary<string, List<MenuItem>>();
        }

        public void addItem(string category, MenuItem item)
        {
            if (!m_menu.ContainsKey(category))
                m_menu[category] = new List<MenuItem>();

            m_menu[category].Add(item);
        }

        public void addItems(string category, List<MenuItem> items)
        {
            if (!m_menu.ContainsKey(category))
                m_menu[category] = items;
            else
                m_menu[category].AddRange(items);
        }

        public List<MenuItem> getItems(string category)
        {
            return m_menu[category];
        }

        public MenuItem getItem(string name)
        {
            // Loop through all items and find the specific item.
            foreach (KeyValuePair<string, List<MenuItem>> entry in m_menu)
            {
                foreach (MenuItem k in entry.Value)
                {
                    if (k.Name == name)
                        return k;
                }
            }

            return null;
        }
    }
}
