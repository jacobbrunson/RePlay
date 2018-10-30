using System;
using System.Collections.Generic;

namespace RePlay.WrapperActivities
{
    public class Paginator<T>
    {
        public readonly List<T> ItemsList;

        public readonly int TotalNumItems;
        public readonly int ItemsPerPage;
        public readonly int ItemsRemaining;
        public readonly int LastPage;

        public Paginator(int itemsPerPage, List<T> itemsList)
        {
            ItemsList = itemsList;
            TotalNumItems = ItemsList.Count;
            ItemsPerPage = itemsPerPage;
            ItemsRemaining = TotalNumItems % ItemsPerPage;
            LastPage = Math.Max((TotalNumItems - 1) / ItemsPerPage, 0);
        }

        public List<T> GeneratePage(int curr)
        {
            int start = curr * ItemsPerPage;

            List<T> data = new List<T>();

            for (int i = start; i < Math.Min(start + ItemsPerPage, ItemsList.Count); i++)
            {
                data.Add(ItemsList[i]);
            }

            return data;
        }

        public bool ContainsLast(int curr){
            return curr == LastPage;
        }
    }
}
