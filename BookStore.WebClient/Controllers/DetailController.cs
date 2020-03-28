using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStore.WebClient.ViewModels;
using BookStore.Services.MessageTypes;
using BookStore.WebClient.ClientModels;

namespace BookStore.WebClient.Controllers
{
    public class DetailController : Controller
    {

        Details details = null;
        // GET: Detail
        public ActionResult BookInformation(int pMediaId, int userId)
        {
            
            details = new Details(pMediaId, userId);
            return View(details);
        }

        public RedirectToRouteResult Like(int pMediaId, int userId)
        {
            var bought = ServiceFactory.Instance.PurchaseHistoryService.hasBought(FetchUserById(userId), FetchMediaById(pMediaId));
            if (bought) {
                ServiceFactory.Instance.PurchaseHistoryService.addLike(pMediaId, userId);
                return RedirectToAction("BookInformation", new { pMediaId,userId });
            }
            return RedirectToAction("RatingError");
        }

        public RedirectToRouteResult Dislike(int pMediaId, int userId)
        {

            var bought = ServiceFactory.Instance.PurchaseHistoryService.hasBought(FetchUserById(userId), FetchMediaById(pMediaId));
            if (bought)
            {
                ServiceFactory.Instance.PurchaseHistoryService.addDislike(pMediaId, userId);
                return RedirectToAction("BookInformation", new { pMediaId,userId });
            }
            return RedirectToAction("RatingError");
        }


        public ActionResult RatingError() {
            return View();
        }




        private Media FetchMediaById(int pId)
        {
            return ServiceFactory.Instance.CatalogueService.GetMediaById(pId);
        }


        private User FetchUserById(int uid){

            return ServiceFactory.Instance.UserService.ReadUserById(uid);
        }

    }
}