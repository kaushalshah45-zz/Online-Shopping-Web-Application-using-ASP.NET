//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeAndForSale
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        public user()
        {
            this.products = new HashSet<product>();
        }
    
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userType { get; set; }
        public string sex { get; set; }
        public string phoneNumber { get; set; }
    
        public virtual ICollection<product> products { get; set; }
    }
}
