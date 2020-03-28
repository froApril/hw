using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Services.Interfaces;
using BookStore.Business.Components.Interfaces;
using Microsoft.Practices.ServiceLocation;
using BookStore.Services.MessageTypes;


using BookStore.Business.Entities;

namespace BookStore.Services
{
    public class PurchaseHistoryService : IPurchaseHistoryService
    {
        private IPurchaseHistoryProvider PurchaseHistoryProvider
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IPurchaseHistoryProvider>();
            }
        }

        public void addDislike(int media_id, int userid)
        {
            PurchaseHistoryProvider.addDislike(media_id,userid );
        }

        public void addLike(int media_id, int userid)
        {
            PurchaseHistoryProvider.addLike(media_id, userid);
        }
        
        public void createHistory(MessageTypes.User user, MessageTypes.Media media)
        {
            var internalType_u = MessageTypeConverter.Instance.Convert<
                BookStore.Services.MessageTypes.User,
                BookStore.Business.Entities.User>(
                user);
            var internalType_m = MessageTypeConverter.Instance.Convert<
                BookStore.Services.MessageTypes.Media,
                BookStore.Business.Entities.Media>(
                media);
            PurchaseHistoryProvider.createHistory(internalType_u,internalType_m);
        }

        public bool hasRated(MessageTypes.User user, MessageTypes.Media media)
        {
            var internalType_u = MessageTypeConverter.Instance.Convert<
                BookStore.Services.MessageTypes.User,
                BookStore.Business.Entities.User>(
                user);
            var internalType_m = MessageTypeConverter.Instance.Convert<
                BookStore.Services.MessageTypes.Media,
                BookStore.Business.Entities.Media>(
                media);
            return PurchaseHistoryProvider.hasRated(internalType_u, internalType_m);

        }

        public bool hasBought(MessageTypes.User user, MessageTypes.Media media)
        {
            var internalType_u = MessageTypeConverter.Instance.Convert<
               BookStore.Services.MessageTypes.User,
               BookStore.Business.Entities.User>(
               user);
            var internalType_m = MessageTypeConverter.Instance.Convert<
                BookStore.Services.MessageTypes.Media,
                BookStore.Business.Entities.Media>(
                media);

            return PurchaseHistoryProvider.hasBought(internalType_u, internalType_m);
        }

        public List<MessageTypes.Media> getRecommendationList(int media, int user)
        {
            List<BookStore.Business.Entities.Media> list = PurchaseHistoryProvider.getRecommendationList(media, user);
            var externalResult = MessageTypeConverter.Instance.Convert<
               List<BookStore.Business.Entities.Media>,
               List<BookStore.Services.MessageTypes.Media>>(list);

            return externalResult;
        }
    }
}
