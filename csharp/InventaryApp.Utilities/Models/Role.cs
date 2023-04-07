using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventaryApp.Utilities.Models
{
    public class Role
    {
        public int? id { get; set; }
        public Guid role_guid { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
