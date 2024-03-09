using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DigitalStore.Models.EF
{
    [Table("tb_VoucherCategory")]
    public class VoucherCategory
    {
        public VoucherCategory() 
        {
            this.Vouchers = new HashSet<Voucher>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Voucher> Vouchers { get; set; }
    }
}