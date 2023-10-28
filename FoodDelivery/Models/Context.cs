using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FoodDelivery.Models
{
    public class Context: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Dish> Dishes{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<UserReview> RatingUserReviews { get; set; }
        public DbSet<LogoutTokens> LogoutTokens { get; set; }
        public DbSet<DishBasket> DishBasket { get; set; }
        public DbSet<DishOrder> DishOrder { get; set; }

        public Context(DbContextOptions<Context> options): base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Dish>().HasKey(x => x.Id);
            modelBuilder.Entity<Order>().HasKey(x => x.Id);
            modelBuilder.Entity<UserReview>().HasKey(x => x.Id);
            modelBuilder.Entity<LogoutTokens>().HasKey(x => x.Id);
            modelBuilder.Entity<DishBasket>().HasKey(x => x.Id);
            modelBuilder.Entity<DishOrder>().HasKey(x => x.Id);
        }

        public User? GetUserByToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var email = ((JwtSecurityToken)jsonToken).Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            return Users.Where(x => x.Email == email)
                .Include(x => x.Orders).ThenInclude(x => x.DishesInOrder)
                .Include(x => x.Cart)
                .FirstOrDefault();
        }

        public Dish? GetDishById(Guid id)
        {
            return Dishes.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
