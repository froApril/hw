﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStore.Services.MessageTypes;

namespace BookStore.WebClient.ClientModels
{
    public class Cart
    {
        private List<OrderItem> mOrderItems = new List<OrderItem>();
        public IList<OrderItem> OrderItems { get { return mOrderItems.AsReadOnly(); } }

        public void AddItem(Media pMedia, int pQuantity)
        {
            var lItem = mOrderItems.FirstOrDefault(oi => oi.Media.Id == pMedia.Id);
            if (lItem == null)
            {
                mOrderItems.Add(new OrderItem() { Media = pMedia, Quantity = pQuantity });
            }
            else
            {
                lItem.Quantity += pQuantity;
            }
        }

        public decimal ComputeTotalValue()
        {
            return mOrderItems.Sum(oi => oi.Media.Price * oi.Quantity);
        }

        public void Clear()
        {
            mOrderItems.Clear();
        }

        public void SubmitOrderAndClearCart(UserCache pUserCache)
        {

            Order lOrder = new Order();
            lOrder.OrderDate = DateTime.Now;
            lOrder.Customer = pUserCache.Model;
            lOrder.Status = 0;
            foreach (OrderItem lItem in mOrderItems)
            {
                ServiceFactory.Instance.PurchaseHistoryService.createHistory(pUserCache.Model,lItem.Media);
                lOrder.OrderItems.Add(lItem);
            }


            ServiceFactory.Instance.OrderService.SubmitOrder(lOrder);
            pUserCache.UpdateUserCache();
            Clear();
        }

        public void RemoveLine(Media pMedia)
        {
            mOrderItems.RemoveAll(oi => oi.Media.Id == pMedia.Id);
        }
    }
}