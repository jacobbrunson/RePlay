using System;
using System.Collections.Generic;

namespace RePlay.WrapperActivities
{
    public class Paginator<T>
    {
        public List<T> ItemsList;
        public int TOTAL_NUM_ITEMS;
        public int ITEMS_PER_PAGE;
        public int ITEMS_REMAINING;
        public int LAST_PAGE;

        public Paginator(int items_per_page, List<T> list)
        {
            ItemsList = list;
            TOTAL_NUM_ITEMS = ItemsList.Count;
            ITEMS_PER_PAGE = items_per_page;
            ITEMS_REMAINING = TOTAL_NUM_ITEMS % ITEMS_PER_PAGE;
            LAST_PAGE = TOTAL_NUM_ITEMS / ITEMS_PER_PAGE;
        }

        public List<T> GeneratePage(int curr)
        {
            int start = curr * ITEMS_PER_PAGE;

            List<T> data = new List<T>();

            if (ITEMS_REMAINING > 0 && curr == LAST_PAGE)
            {
                for (int i = start; i < start + ITEMS_REMAINING; i++)
                {
                    data.Add(ItemsList[i]);
                }
            }
            else
            {
                for (int i = start; i < start + ITEMS_PER_PAGE; i++)
                {
                    data.Add(ItemsList[i]);
                }
            }

            return data;
        }

        public bool ContainsLast(int curr){
            return curr == LAST_PAGE;
        }
    }
}
