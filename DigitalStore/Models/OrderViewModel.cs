using DigitalStore.Models.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DigitalStore.Models
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        public string Address { get; set; }
        public string CustomerId { get; set; }
        public int Quantity { get; set; }
        public int TypePayment { get; set; }
        public int TypePaymentVN { get; set; }
        public decimal DiscountPrice { get; set; }
        public string DiscountCode { get; set; }
    }
}