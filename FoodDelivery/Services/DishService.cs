using Microsoft.EntityFrameworkCore;
using FoodDelivery.Models.DTO;
using FoodDelivery.Models;
using FoodDelivery.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FoodDelivery.Services
{
    public interface IDishService
    {
        DishPagedListDTO? GetDishPagedList(DishCategory[] categories, bool vegetarian,
            DishSorting sorting, int page);
        DishDTO? GetDish(Guid id);
        string Check(Guid id, string token);
        string Set(Guid id, string token, int rating);
    }
    public class DishService : IDishService
    {
        private readonly Context _context;

        public DishService(Context context)
        {
            _context = context;
        }

        public DishPagedListDTO? GetDishPagedList(DishCategory[] categories, bool vegetarian, DishSorting sorting, int page)
        {
            IQueryable<Dish> query = _context.Dishes;

            if (vegetarian)
            {
                query = query.Where(x => x.Vegetarian == true);
            }

            if (categories.Length > 0)
            {
                query = query.Where(x => categories.Contains(x.DishCategory));
            }

            query = sorting switch
            {
                DishSorting.NameAsc => query.OrderBy(s => s.Name),
                DishSorting.NameDesc => query.OrderByDescending(s => s.Name),
                DishSorting.PriceAsc => query.OrderBy(s => s.Price),
                DishSorting.PriceDesc => query.OrderByDescending(s => s.Price),
                DishSorting.RatingAsc => query.OrderBy(s => s.Rating),
                DishSorting.RatingDesc => query.OrderByDescending(s => s.Rating),
                _ => query
            };

            int pageSize = 5;
            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (page < 1 || page > totalPages)
            {
                return null;
            }

            List<Dish> dishesOfPage = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new DishPagedListDTO
            {
                Dishes = ConverterDTO.Dishes(dishesOfPage),
                Pagination = new PageInfoDTO
                {
                    Size = pageSize,
                    Count = totalPages,
                    Current = page
                }
            };
        }


        public DishDTO? GetDish(Guid id)
        {
            var dish = _context.GetDishById(id);

            if (dish == null)
                return null;

            return ConverterDTO.Dish(dish);
        }

        public string Check(Guid id, string token)
        {
            var user = _context.GetUserByToken(token);
            if (user == null)
                return null;

            var dishcheck = _context.GetDishById(id);
            if (dishcheck == null)
                return "dish is not found";

            var orders = user.Orders;
            if (orders.Count == 0)
                return "false";

            foreach (var order in orders) 
            { 
                foreach(var dish in order.DishesInOrder)
                {
                    if (dish.IdOfDish == id.ToString())
                    {
                        return "true";
                    }
                }
            }

            return "false";
        }

        public string Set(Guid id, string token, int rating)
        {
            var user = _context.GetUserByToken(token);
            if (user == null)
                return "user not found";

            var dish = _context.GetDishById(id);
            if (dish == null)
                return "dish not found";

            if (Check(id, token) == "true")
            {
                var review = ConverterDTO.Review(user, dish, rating);
                double avgrating = 0;
                int count = 0;
                //проверка на существующий отзыв
                var reviews = _context.RatingUserReviews.Include(x => x.User).Include(x => x.Dish).ToList();
                foreach (var userreview in reviews)
                {
                    if(userreview.User.Id == user.Id && userreview.Dish.Id == dish.Id)
                    {
                        userreview.Rating = rating;
                        avgrating = 0;
                        count = 0;
                        foreach (var reviewscore in reviews)
                        {
                            if (reviewscore.Dish.Id == id)
                            {
                                avgrating += reviewscore.Rating;
                                count++;
                            }
                        }
                        avgrating = avgrating / count;
                        dish.Rating = avgrating;
                        _context.SaveChanges();
                        return "rating changed";
                    }
                }
                _context.RatingUserReviews.Add(review);
                reviews.Add(review);
                _context.SaveChanges();
                avgrating = 0;
                //reviews = _context.RatingUserReviews.Include(x => x.User).Include(x => x.Dish).ToList();
                count = 0;
                foreach (var reviewscore in reviews)
                {
                    if (reviewscore.Dish.Id == id)
                    {
                        avgrating += reviewscore.Rating;
                        count++;
                    }
                }
                avgrating = avgrating / count;
                dish.Rating = avgrating;
                _context.SaveChanges();
                //return reviews.Count().ToString();
                return "the rating is set";
            }

            return "error";
        }
    }
}
