using FoodDelivery.Models.DTO;
using FoodDelivery.Models;
using FoodDelivery.Models.Enum;

namespace FoodDelivery.Services
{
    public interface IOrderService
    {
        OrderCreateDTO? CreateOrderFromBasket(string token);
        OrderDTO? GetOrderById(Guid id, string token);
        OrderListDTO GetOrderList(string token);
        string ConfirmDelivery(string token, Guid id);
    }

    public class OrderService : IOrderService
    {
        private readonly Context _context;

        public OrderService(Context context)
        {
            _context = context;
        }

        public OrderCreateDTO? CreateOrderFromBasket(string token)
        {
            var user = _context.GetUserByToken(token);
            if (user == null)
                return null;

            var basket = user.Cart;
            if (basket.Count == 0)
                return null;

            var order = ConverterDTO.Order(basket, user.Address);

            //Проверить price
            double price = 0;
            foreach(var dish in ConverterDTO.DishInOrders(basket))
            {
                price += dish.TotalPrice;
                _context.DishOrder.Add(dish);
            }
            order.Price = price;
            user.Orders.Add(order);
            user.Cart.Clear();
            /*var dishinbasket = _context.DishBasket.ToList();
            foreach (var entity in dishinbasket)
            {
                dishinbasket.Remove(entity);
            }*/
            _context.SaveChanges();

            return new OrderCreateDTO
            {
                Address = order.Address,
                DeliveryTime = order.DeliveryTime
            };
        }

        public OrderDTO? GetOrderById(Guid id, string token)
        {
            var user = _context.GetUserByToken(token);
            if (user == null)
                return null;

            var orders = user.Orders;
            if(orders.Count == 0)
                return null;

            foreach(Order order in orders)
            {
                if(order.Id == id)
                {
                    return ConverterDTO.OrderById(order);
                }
            }

            return null;
        }

        public OrderListDTO? GetOrderList(string token)
        {
            var user = _context.GetUserByToken(token);
            if (user == null)
                return null;

            var orders = user.Orders;
            if (orders.Count == 0)
                return null;

            OrderListDTO? orderlist = new();

            //ICollection<OrderInfoDTO?> orderList = new List<OrderInfoDTO?>();

            foreach (Order order in orders)
            {
                //orderList.Add(ConverterDTO.OrderInfo(order));
                orderlist.OrderList.Add(ConverterDTO.OrderInfo(order));
            }

            return orderlist;
        }

        public string ConfirmDelivery(string token, Guid id)
        {
            var user = _context.GetUserByToken(token);
            if (user == null)
                return "user not found";

            var orders = user.Orders;
            if (orders.Count == 0)
                return "orders not found";

            foreach (Order order in orders)
            {
                if (order.Id == id)
                {
                    order.Status = OrderStatus.Delivered;
                    _context.SaveChanges();
                    return "confirmed";
                }
            }

            return "order not found";
        }
    }
}
