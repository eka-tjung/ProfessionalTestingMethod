namespace ShoppingCartDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// General class to keep track of inventory
    /// </summary>
    class Inventory
    {
        private String Name { get; set; }

        private int _price;
        public int Price {
            get
            {
                return this._price;
            }
        }

        public Inventory(String name, int price)
        {
            this.Name = name;
            this._price = price;
        }

        /// <summary>
        /// Displays the name of this inventory, complete with price
        /// </summary>
        /// <returns>The proper way to display this inventory</returns>
        public String DisplayName()
        {
            return String.Format("{0} (${1})", this.Name, this.Price);
        }
    }
}
