using System.Collections.Generic;

namespace GameStore.Models
{
  public class Employee
    {
        public Employee()
        {
            this.JoinEntities = new HashSet<CustomerEmployee>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public virtual ICollection<CustomerEmployee> JoinEntities { get; set; }
    }
}