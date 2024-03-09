using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using DigitalStore.Controllers;
using DigitalStore.Models.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DigitalStore.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ChuoiKN", throwIfV1Schema: false)
        {
        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameImage> GameImages { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<GameNews> GameNews { get; set; }
        public DbSet<NewsCategory> NewsCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<ContractCategory> ContractCategories { get; set; }
        public DbSet<Contract> Contracts {  get; set; } 
        public DbSet<MarketingPartner> MarketingPartners { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<VoucherCategory> VoucherCategories { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }    
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}