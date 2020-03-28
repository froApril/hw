using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookStore.Business.Components.Interfaces;
using BookStore.Business.Entities;
using System.Transactions;
using System.Data.Entity;

namespace BookStore.Business.Components
{
    public class OrderProvider : IOrderProvider
    {
        public void SubmitOrder(Entities.Order pOrder)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (BookStoreEntityModelContainer lContainer = new BookStoreEntityModelContainer())
            {
                AttachEntitiesToContext(lContainer, pOrder);

                pOrder.UpdateStockLevels();

                lContainer.SaveChanges();
                lScope.Complete();
            }
        }

        private void AttachEntitiesToContext(BookStoreEntityModelContainer pContainer, Order pOrder)
        {
 
            pContainer.Users.Attach(pOrder.Customer);
            pOrder.OrderItems.ToList().ForEach(p => pContainer.Media.Attach(p.Media));

            pContainer.Orders.Add(pOrder);
            //load stock entities so that we'll be able to update their details
            LoadEntitiesForOrderSubmission(pOrder, pContainer);
        }


        private void LoadEntitiesForOrderSubmission(Order pOrder, BookStoreEntityModelContainer pContainer)
        {
            foreach(OrderItem item in pOrder.OrderItems)
            {
                item.Media.Stocks = pContainer.Media.Include("Stocks").First(p => p.Id == item.Media.Id).Stocks;
            }
        }


    }
}
