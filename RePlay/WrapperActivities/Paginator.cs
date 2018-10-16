using System;
using System.Collections.Generic;

namespace RePlay.WrapperActivities
{
    public class Paginator
    {
        public List<Game> ItemsList;
        public static int TOTAL_NUM_ITEMS;
        public static int ITEMS_PER_PAGE;
        public static int ITEMS_REMAINING;
        public static int LAST_PAGE;

        public Paginator(int items_per_page)
        {

            // InitializeItemsList(); // Not Implemented yet

            ItemsList = new List<Game> { new Game(0, "Breakout"), new Game(1, "Traffic Racer"),
            new Game(2, "Fruit Archery"), new Game(3, "Temple Run"), new Game(4, "Crossy Road"),
            new Game(5, "Typer Shark"), new Game(6, "Handwriting"), new Game(7, "Rep Mode")};

            TOTAL_NUM_ITEMS = ItemsList.Count;
            ITEMS_PER_PAGE = items_per_page;
            ITEMS_REMAINING = TOTAL_NUM_ITEMS % ITEMS_PER_PAGE;
            LAST_PAGE = TOTAL_NUM_ITEMS / ITEMS_PER_PAGE;
        }

        public List<Game> GeneratePage(int curr)
        {
            int start = curr * ITEMS_PER_PAGE;

            List<Game> data = new List<Game>();

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

        private void InitializeItemsList()
        {
            // This method should initialze ItemsList to either be a list of exercises or a list of games
            throw new NotImplementedException();
        }
    }
}
