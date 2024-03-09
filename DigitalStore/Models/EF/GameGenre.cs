using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalStore.Models.EF
{
    [Table("tb_GameGenre")]
    public class GameGenre : CommonAbstract
    {
        public GameGenre()
        {
            this.Games = new HashSet<Game>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên thể loại Game không được để trống")]
        [StringLength(150, ErrorMessage = "Tên thể loại Game không được vượt quá 150 ký tự")]
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public ICollection<Game> Games { get; set; }
    }
}