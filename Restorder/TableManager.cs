using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restorder
{
    public class TableManager
    {
        private List<Table> tables;
        private Table currentTable;
        public Table CurrentTable
        {
            get { return currentTable; }
        }

        /// <summary>
        /// Creates the TableManager class. Initializes the table manager with 0 elements.
        /// </summary>
        public TableManager()
        {
            tables = new List<Table>();
            currentTable = null;
        }

        /// <summary>
        /// Sets the currently active table to the table-index i.
        /// </summary>
        /// <param name="index">The index of table (table id)</param>
        public void setCurrentTable(int index)
        {
            setCurrentTable(getTable(index));
        }

        /// <summary>
        /// Sets the currently actve table to the table t.
        /// </summary>
        /// <param name="t"></param>
        public void setCurrentTable(Table t)
        {
            currentTable = t;
        }

        /// <summary>
        /// Initializes the TableManager class. The table manager is initialized with num elements.
        /// </summary>
        /// <param name="num">The number of elements to initialize to the dict.</param>
        public TableManager(int num)
        {
            tables = new List<Table>(num);

            for (int i = 0; i < num; i++)
            {
                tables.Add(new Table(i + 1));
            }
        }

        /// <summary>
        /// Returns a reference to the Table element at the specified index.
        /// </summary>
        /// <param name="index">The table to access. 0 based.</param>
        /// <returns>A reference to the requested table.</returns>
        public Table getTable(int index)
        {
            return tables[index];
        }

        /// <summary>
        /// Adds a table to the manager.
        /// </summary>
        /// <returns>Returns the table that was added to the manager.</returns>
        public Table addTable()
        {
            tables.Add(new Table(tables.Count + 1));
            return tables.Last();
        }

        /// <summary>
        /// Adds multiple tables to the table manager.
        /// </summary>
        /// <param name="num">The number of tables to add.</param>
        public void addTables(int num)
        {
            for (int i = 0; i < num; i++)
                tables.Add(new Table(tables.Count + 1));
        }
    }
}
