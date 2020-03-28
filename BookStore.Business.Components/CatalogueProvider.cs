using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Business.Components.Interfaces;
using BookStore.Business.Entities;

namespace BookStore.Business.Components
{
    public class CatalogueProvider : ICatalogueProvider
    {
        public List<Entities.Media> GetMediaItems(int pOffset, int pCount)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                return (from MediaItem in lContainer.Media.Include("Stocks")
                       orderby MediaItem.Id
                       select MediaItem).Skip(pOffset).Take(pCount).ToList();
            }
        }


        public Media GetMediaById(int pId)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {   
                return lContainer.Media.Include("Stocks").First(p => p.Id == pId); 
            }
        }

    }
}
