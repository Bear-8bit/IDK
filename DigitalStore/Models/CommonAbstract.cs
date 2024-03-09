using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace DigitalStore.Models
{
    public abstract class CommonAbstract
    {
        public string CreatedBy { get; set; }
        public System.Nullable<DateTime> CreatedDate { get; set; }
        public System.Nullable<DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}