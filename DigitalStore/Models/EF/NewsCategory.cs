using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DigitalStore.Models.EF
{
    [Table("tb_NewsCategory")]
    public class NewsCategory : CommonAbstract
    {
        public NewsCategory()
        {
            this.GameNews = new HashSet<GameNews>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên loại báo không được để trống")]
        [StringLength(150, ErrorMessage = "Tên loại báo không được vượt quá 150 ký tự")]
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }

        public ICollection<GameNews> GameNews { get; set; }
    }
}