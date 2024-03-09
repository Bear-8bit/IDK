using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace DigitalStore.Models.EF
{
    [Table("tn_Game")]
    public class Game : CommonAbstract
    {
        public Game()
        {
            this.GameImage = new HashSet<GameImage>();
            this.OrderDetails = new HashSet<OrderDetail>();
            this.Reviews = new HashSet<Review>();   
            this.Wishlists = new HashSet<Wishlist>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GameGenreId { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
        public string Image { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsNew { get; set; }
        public bool IsActive { get; set; }

        public virtual GameGenre GameGenres { get; set; }
        public virtual Publisher Publishers { get; set; }
        public virtual ICollection<GameImage> GameImage { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
        public ICollection<Review> Reviews { get; set; }


    }
}