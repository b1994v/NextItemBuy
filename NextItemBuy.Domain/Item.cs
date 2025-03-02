//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NextItemBuy.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public int Id { get; set; }
        public System.Guid UserId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public System.DateTime Deadline { get; set; }
        public bool IsBuyed { get; set; }
        public System.DateTime NotificationDate { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
    
        public virtual ItemCategory ItemCategory { get; set; }
    }
}
