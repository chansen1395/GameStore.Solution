using System.Collections.Generic;

namespace GameStore.Models
{
    public class Customer
    {
        public Customer()
        {
            this.JoinEntities = new HashSet<CustomerEmployee>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool CustomerCheckout { get; set; }
        public int CustomerPurchase { get; set; }
        public int CheckoutDate { get; set; }
        public virtual ApplicationUser User { get; set; } //new line

        public virtual ICollection<CustomerEmployee> JoinEntities { get;}
    }
}