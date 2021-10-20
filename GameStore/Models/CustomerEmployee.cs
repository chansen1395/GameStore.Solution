using System.Collections.Generic;
using System.Linq;

namespace GameStore.Models
{
  public class CustomerEmployee
    {       
        public int CustomerEmployeeId { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Employee Employee { get; set; }
    }
}