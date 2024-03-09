using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DigitalStore.Models.EF
{
    [Table("tb_Contract")]
    public class Contract : CommonAbstract
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ContractCategoryId { get; set; }
        public string Contract_Code { get; set; }
        public string Name { get; set; }
        public string NameSideA {  get; set; }
        public string NameSideB { get; set; }
        public decimal Price { get; set; }
        [Display(Name = "Ngày có hiệu lực")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.Nullable<DateTime> ActiveDate { get; set; }
        [Display(Name = "Ngày hết hiệu lực")]
        [DisplayFormat(DataFormatString = "{0: dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public System.Nullable<DateTime> ExpireDate { get; set; }


        public virtual ContractCategory ContractCategories { get; set; }
    }
}