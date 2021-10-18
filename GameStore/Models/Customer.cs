using System.Collections.Generic;
using System;

namespace GameStore.Models
{
    public class Customer
    {
        public Customer()
        {
            this.JoinEntities = new HashSet<CustomerEmployee>();
            this.JoinCustomerGame = new HashSet<CustomerGame>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool CustomerCheckout { get; set; }
        public int CustomerPurchase { get; set; }
        public DateTime CheckoutDate { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<CustomerEmployee> JoinEntities { get;}
        public virtual ICollection<CustomerGame> JoinCustomerGame { get;}
    }
}