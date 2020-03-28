using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Services.Interfaces;
using BookStore.Services.MessageTypes;

namespace BookStore.WebClient.ViewModels
{
    public class CatalogueViewModel
    {

        private ICatalogueService CatalogueService
        {
            get
            {
                return  ServiceFactory.Instance.CatalogueService;
            }
        }

        public List<Media> Items
        {
            get
            {
                return CatalogueService.GetMediaItems(0, Int32.MaxValue);
            }
        }
    }
}