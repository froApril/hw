using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Services.Interfaces;
using BookStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;
using BookStore.Services.MessageTypes;

namespace BookStore.Services
{
    public class CatalogueService : ICatalogueService
    {

        private ICatalogueProvider CatalogueProvider
        {
            get
            {
                return ServiceFactory.GetService<ICatalogueProvider>();
            }
        }

        public List<Media> GetMediaItems(int pOffset, int pCount)
        {
            var internalResult = CatalogueProvider.GetMediaItems(pOffset, pCount);
            var externalResult = MessageTypeConverter.Instance.Convert<
                List<BookStore.Business.Entities.Media>,
                List<BookStore.Services.MessageTypes.Media>>(internalResult);

            return externalResult;
        }


        public Media GetMediaById(int pId)
        {
            var external = MessageTypeConverter.Instance.Convert<
                BookStore.Business.Entities.Media,
                BookStore.Services.MessageTypes.Media>(
                CatalogueProvider.GetMediaById(pId));
            return external;
        }
    }
}
