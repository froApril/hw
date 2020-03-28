using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Business.Components.Interfaces;
using BookStore.Business.Entities;
using System.Transactions;
using System.Data.Entity.Validation;

namespace BookStore.Business.Components
{
    class HistoryProvider : IPurchaseHistoryProvider
    {

        public void createHistory(User user, Media media)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {

                lContainer.Users.Attach(user);
                lContainer.Media.Attach(media);
                media.Stocks = lContainer.Media.Include("Stocks").First(p => p.Id == media.Id).Stocks;
                lContainer.PurchasedHistories.Add(new PurchasedHistory { User = user, Media = media, Rating=false, RateDate = DateTime.Now}) ;
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        public Boolean hasRated(User user, Media media)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                var h = from history in lContainer.PurchasedHistories
                        where history.Media.Id == media.Id && history.User.Id == user.Id
                        select history;
                if (h.Count() > 0) {
                    return (bool)h.First().Rating;
                }
                return false;
               
            }
        }

        public Boolean hasBought(User user, Media media) {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                var h = from history in lContainer.PurchasedHistories
                        where history.Media.Id == media.Id && history.User.Id == user.Id
                        select history;
                if (h.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public void addDislike(int media_id, int userid)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                var h = from history in lContainer.PurchasedHistories
                        where history.Media.Id == media_id && history.User.Id == userid
                        select history;
                var m = lContainer.Media.Include("Stocks").First(p => p.Id == media_id);
                var val = h.First();
                if (val.Rating == false)
                {
                    m.Dislike += 1;
                    val.Rating = true;
                }
                else
                {
                    if (val.RateValue == 1)
                    {
                        m.Like -= 1;
                        m.Dislike += 1;
                    }
                }
                val.RateValue = 0;
                val.RateDate = DateTime.Now;
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        public void addLike(int media_id, int userid)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                var h = from history in lContainer.PurchasedHistories
                        where history.Media.Id == media_id && history.User.Id == userid
                        select history;
                var m = lContainer.Media.Include("Stocks").First(p => p.Id == media_id);
                var val = h.First();
                if (val.Rating == false)
                {
                    m.Like += 1;
                    val.Rating = true;
                }
                else {
                    if (val.RateValue == 0) {
                        m.Like += 1;
                        m.Dislike -= 1;
                    }
                }
                val.RateValue = 1;
                val.RateDate = DateTime.Now;
                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        public List<Media> getRecommendationList(int media_id, int user_id)
        {
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                var usersWithTheSameRate = from history in lContainer.PurchasedHistories
                        where history.Media.Id == media_id && history.User.Id != user_id && history.RateValue==1
                        select history.User.Id;


                List<Media> result = new List<Media>();
                foreach (int userid in usersWithTheSameRate.ToList()) {
                    var h = from history in lContainer.PurchasedHistories
                            where history.Media.Id != media_id && history.User.Id == userid && history.RateValue == 1
                            orderby history.RateDate descending
                            select history.Media.Id;
                    foreach(int t in h.ToList()) {
                        var m = lContainer.Media.Include("Stocks").First(p => p.Id == t);
                        result.Add(m);
                    }
                }

                
                lContainer.SaveChanges();
                return result;
            }
        }
    }
}
