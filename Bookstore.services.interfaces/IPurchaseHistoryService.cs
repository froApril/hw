using BookStore.Services.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Services.Interfaces
{

    [ServiceContract]
    public interface IPurchaseHistoryService
    {
        [OperationContract]
        void createHistory(User user, Media media);
        [OperationContract]
        Boolean hasRated(User user, Media media);
        [OperationContract]
        Boolean hasBought(User user, Media media);
        [OperationContract]
        void addLike(int media_id, int userid);
        [OperationContract]
        void addDislike(int media_id, int userid );
        [OperationContract]
        List<Media> getRecommendationList(int media, int user);


    }
}
