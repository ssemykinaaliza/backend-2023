using FoodDelivery.Models;
using FoodDelivery.Models.DTO;
using FoodDelivery.Models.Enum;

namespace FoodDelivery.Services
{
    public class ConverterDTO
    {
        public static LoginCredentials Login(UserRegisterDTO model)
        {
            return new LoginCredentials { Email = model.Email, Password = model.Password };
        }

        public static User Register(UserRegisterDTO model)
        {
            return new User
            {
                FullName = model.FullName,
                BirthDate = model.BirthDate,
                Gender = model.Gender,
                Address = model.Address,
                Email = model.Email,
                Password = model.Password,
                PhoneNumber = model.PhoneNumber
            };
        }

        public static UserDTO Profile(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                BirthDate = user.BirthDate,
                Gender = user.Gender,
                Address = user.Address,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        public static ICollection<DishDTO?> Dishes(ICollection<Dish> dishes)
        {
            ICollection<DishDTO?> resultDishes = new List<DishDTO?>();
            foreach (var dish in dishes)
            {
                resultDishes.Add(new DishDTO
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Description = dish.Description,
                    Price = dish.Price,
                    Image = dish.Image,
                    Vegetarian = dish.Vegetarian,
                    Rating = dish.Rating,
                    Dish = dish.DishCategory
                });
            }

            return resultDishes;
        }

        public static DishDTO? Dish(Dish dish)
        {
            return new DishDTO
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Price,
                Image = dish.Image,
                Vegetarian = dish.Vegetarian,
                Rating = dish.Rating,
                Dish = dish.DishCategory
            };
        }

        public static DishBasket? DishInBasket(Dish dish)
        {
            return new DishBasket
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                TotalPrice = dish.Price * 1,
                Amount = 1,
                Image = dish.Image
            };
        }

        public static BasketDTO? Cart(ICollection<DishBasket> basket)
        {
            ICollection<DishBasketDTO> resultDishes = new List<DishBasketDTO>();
            foreach (var dish in basket)
            {
                resultDishes.Add(new DishBasketDTO
                {
                    Id = dish.IdOfDish,
                    Name = dish.Name,
                    Price = dish.Price,
                    TotalPrice = dish.TotalPrice,
                    Amount = dish.Amount,
                    Image = dish.Image
                });
            }

            return new BasketDTO
            {
                Dishes = resultDishes
            };
        }

        public static ICollection<DishOrder> DishInOrders(ICollection<DishBasket> dishBaskets)
        {
            ICollection<DishOrder> resultDishes = new List<DishOrder>();
            foreach (var dish in dishBaskets)
            {
                resultDishes.Add(new DishOrder
                {
                    IdOfDish = dish.IdOfDish.ToString(),
                    Name = dish.Name,
                    Price = dish.Price,
                    TotalPrice = dish.TotalPrice,
                    Amount = dish.Amount,
                    Image = dish.Image
                });
            }

            return resultDishes;
        }

        public static Order? Order(ICollection<DishBasket> basket, string address)
        {
            return new Order
            {
                DeliveryTime = DateTime.Now.AddMinutes(30),
                OrderTime = DateTime.Now,
                Status = OrderStatus.InProcess,
                Address = address,
                Price = 0,
                DishesInOrder = ConverterDTO.DishInOrders(basket)
            };
        }

        public static OrderDTO? OrderById(Order order)
        {
            ICollection<DishBasketDTO> resultDishes = new List<DishBasketDTO>();
            foreach (var dish in order.DishesInOrder)
            {
                resultDishes.Add(new DishBasketDTO
                {
                    Id = dish.IdOfDish.ToString(),
                    Name = dish.Name,
                    Price = dish.Price,
                    TotalPrice = dish.TotalPrice,
                    Amount = dish.Amount,
                    Image = dish.Image
                });
            }
            return new OrderDTO
            {
                Id = order.Id,
                DeliveryTime = order.DeliveryTime,
                OrderTime = order.OrderTime,
                Status = order.Status,
                Address = order.Address,
                Price = order.Price,
                Dishes = resultDishes
            };
        }

        public static OrderInfoDTO? OrderInfo(Order order)
        {
            return new OrderInfoDTO
            {
                Id = order.Id,
                DeliveryTime = order.DeliveryTime,
                OrderTime = order.OrderTime,
                Status = order.Status,
                Price = order.Price
            };
        }

        public static UserReview Review(User user, Dish dish, int rating)
        {
            return new UserReview
            {
                User = user,
                Rating = rating,
                Dish = dish
            };
        }
    }
}
