﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webinar.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PISchologistDBEntities : DbContext
    {
        public PISchologistDBEntities()
            : base("name=PISchologistDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tblCardInfo> tblCardInfoes { get; set; }
        public DbSet<tblCountry> tblCountries { get; set; }
        public DbSet<tblRole> tblRoles { get; set; }
        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<tblWebinarDetail> tblWebinarDetails { get; set; }
        public DbSet<tblZoomInstaller> tblZoomInstallers { get; set; }
    }
}
