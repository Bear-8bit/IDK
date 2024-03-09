using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DigitalStore.Models.EF
{
    [Table("tb_Wishlist")]
    public class Wishlist
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public string UserName { get; set; }
        public DateTime? CreatedDate { get; set; }

        public virtual Game Game { get; set; }
    }
}