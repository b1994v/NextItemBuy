﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NextItemBuyEntities : DbContext
    {
        public NextItemBuyEntities()
            : base("name=NextItemBuyEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<ItemCategory> ItemCategories { get; set; }
        public virtual DbSet<Item> Items { get; set; }
    }
}
