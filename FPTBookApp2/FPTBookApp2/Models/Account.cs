namespace FPTBookApp2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Account
    {
        public Account()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public string AccID { get; set; }
        public string pass { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string tel { get; set; }

        public Nullable<int> state { get; set; }
    
        public virtual ICollection<Order> Orders { get; set; }
    }
}
