using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalStore.Models.EF
{
    [Table("tb_Publisher")]
    public class Publisher : CommonAbstract
    {
        public Publisher()
        {
            this.Games = new HashSet<Game>();
            this.GameNewss = new HashSet<GameNews>();
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

        public ICollection<Game> Games { get; set; }
        public ICollection<GameNews> GameNewss { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}