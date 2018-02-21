namespace ShoppingCartDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class InventoryList
    {
        private List<Inventory> _inventoryList;
        public List<Inventory> List
        {
            get
            {
                return _inventoryList;
            }
        }

        public InventoryList()
        {
            _inventoryList = new List<Inventory>();
            InitializeList();
        }

        /// <summary>
        /// Add inventory to the list
        /// </summary>
        private void InitializeList()
        {
            this._inventoryList.Add(new Inventory("Apple", 1));
            this._inventoryList.Add(new Inventory("Grapes", 2));
            this._inventoryList.Add(new Inventory("Mango", 3));
        }
    }
}
