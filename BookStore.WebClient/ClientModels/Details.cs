using BookStore.Services.MessageTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.WebClient.ClientModels
{
    public class Details
    {
        public int mediaId { get; set; }

        public int userId { get; set; }

        public List<Media> recommendation = new List<Media>();

        public Details(int media, int user) {
            mediaId = media;
            userId = user;
            pmedia = FetchMediaById(media);
            puser = FetchUserById(user);
            setRecommendationList();
        }

        public Details() { }

        public Media pmedia { get; set; }
        public User puser { get; set; }

        public void setRecommendationList() {
            recommendation = ServiceFactory.Instance.PurchaseHistoryService.getRecommendationList(mediaId, userId);
        }


        private Media FetchMediaById(int pId)
        {
            return ServiceFactory.Instance.CatalogueService.GetMediaById(pId);
        }


        private User FetchUserById(int uid)
        {

            return ServiceFactory.Instance.UserService.ReadUserById(uid);
        }







    }
}