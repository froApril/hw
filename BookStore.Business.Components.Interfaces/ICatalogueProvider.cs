using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Business.Entities;

namespace BookStore.Business.Components.Interfaces
{
    public interface ICatalogueProvider
    {
        List<Business.Entities.Media> GetMediaItems(int pOffset, int pCount);
        Media GetMediaById(int pId);


    }
}
