using FoodDelivery.Models;

namespace FoodDelivery.Services
{
    public interface ILogoutService
    {
        bool IsUserLogout(string token);

        Task Logout(string token);
    }

    public class LogoutService : ILogoutService
    {
        private readonly Context _context;

        public LogoutService(Context context)
        {
            _context = context;
        }

        public bool IsUserLogout(string token)
        {
            if (_context.LogoutTokens.FirstOrDefault(t => t.Token == token) == null)
                return false;
            return true;
        }

        public async Task Logout(string token)
        {
            await _context.LogoutTokens.AddAsync(new LogoutTokens { Token = token });
            await _context.SaveChangesAsync();
        }
    }
}
