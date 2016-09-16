using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EmMS.Models
{
    public class CurrentDateIdentifier
    {
        public int ID { get; set; }
        public DateTime stored_Date { get; set; }

        public DbSet<CurrentDateIdentifier> CurrentDateIdentifiers { get; set; }
    }
}