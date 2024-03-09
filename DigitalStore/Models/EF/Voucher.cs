using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.WebPages;

namespace DigitalStore.Models.EF
{
    [Table("tb_Voucher")]
    public class Voucher
    {
        public Voucher () 
        {
            this.Orders = new HashSet<Order> ();   
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("VoucherCategory")]
        public int VoucherType { get; set; }
        public string VoucherCode { get; set; }
        public int DiscountPrice { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}

        public virtual VoucherCategory VoucherCategory { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}