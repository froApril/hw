using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Business.Entities;

namespace BookStore.Business.Components.Interfaces
{
    public interface IPurchaseHistoryProvider
    {
        void createHistory(User user, Media media);

        Boolean hasRated(User user, Media media);

        void addLike(int media_id, int userid);

        void addDislike(int media_id, int userid);

        Boolean hasBought(User user, Media media);

        List<Media> getRecommendationList(int media_id, int user_id);

    }

    
}
