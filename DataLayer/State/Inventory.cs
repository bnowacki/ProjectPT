﻿using DataLayer.Catalog;

namespace DataLayer.State
{
    public class Inventory
    {
        public Dictionary<Guid, int> Stock { get; init; } = new Dictionary<Guid, int>();

        public int AddProductToStock(Guid id, int n)
        {
            int prevStock = 0;
            if (Stock.ContainsKey(id))
            {
                prevStock = Stock[id];
            }

            int newStock = prevStock + n;
            if (newStock < 0)
            {
                throw new ArgumentException("Updating would result in negative stock.");
            }
            Stock[id] = newStock;
            return newStock;
        }

        public int GetProductStock(Guid id)
        {
            if (!Stock.ContainsKey(id))
            {
                return 0;
            }
            return Stock[id];
        }
    }
}
