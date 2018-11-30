using System;
using System.Collections.Generic;

namespace RePlay.Manager
{
    // a generic class for returning a list of items to display on the current page
    public class Paginator<T>
    {
        protected readonly List<T> ItemsList;

        public int TotalNumItems;
        public readonly int ItemsPerPage;
        public int ItemsRemaining;
        public int LastPage;

        // create a paginator from a specified list of items and a number of items
        // to show on each page
        public Paginator(int itemsPerPage, List<T> itemsList)
        {
            ItemsList = itemsList;
            TotalNumItems = ItemsList.Count;
            ItemsPerPage = itemsPerPage;
            ItemsRemaining = TotalNumItems % ItemsPerPage;
            LastPage = Math.Max((TotalNumItems - 1) / ItemsPerPage, 0);
        }

        // Generate the current page of items with ItemsPerPage items per page
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

        // remove the item at the specified position from the list
        public T RemoveAt(int position)
        {
            T item = ItemsList[position];
            ItemsList.RemoveAt(position);
            TotalNumItems = ItemsList.Count;
            ItemsRemaining = TotalNumItems % ItemsPerPage;
            LastPage = Math.Max((TotalNumItems - 1) / ItemsPerPage, 0);

            return item;
        }

        // return whether this is the last page
        public bool ContainsLast(int curr)
        {
            return curr == LastPage;
        }
    }
}
