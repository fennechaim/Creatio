﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CreatioUsersData
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CREATIO_DB_PRODEntities : DbContext
    {
        public CREATIO_DB_PRODEntities()
            : base("name=CREATIO_DB_PRODEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Information> Informations { get; set; }
        public virtual DbSet<Avocat> Avocats { get; set; }
        public virtual DbSet<Commercial> Commercials { get; set; }
    }
}
