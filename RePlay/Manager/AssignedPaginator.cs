using System.Collections.Generic;
using RePlay.Entity;

namespace RePlay.Manager
{
    public class AssignedPaginator : Paginator<Prescription>
    {
        AssignedPaginator(int itemsPerPage, List<Prescription> itemsList) : base(itemsPerPage, itemsList)
        {

        }

        public static AssignedPaginator NewInstance(int itemsPerPage, List<Prescription> itemsList){
            // Add a dummy item to the itemsList for the plus button card
            itemsList.Add(new Prescription(null, null, null, 0));

            return new AssignedPaginator(itemsPerPage, itemsList);
        }
    }
}
