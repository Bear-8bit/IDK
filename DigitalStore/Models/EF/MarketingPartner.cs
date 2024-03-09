using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalStore.Models.EF
{
    [Table("tb_MarketingPartner")]
    public class MarketingPartner : CommonAbstract
    {
        public MarketingPartner()
        {
            this.Contracts = new HashSet<Contract>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public ICollection<Contract> Contracts { get; set; }
    }
}