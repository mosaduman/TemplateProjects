using BasicExtensions;

namespace OrderManagementSystem.WebApi.Models
{
    public class User
    {
        public int Id { get; set; } = 1;
        public string Username { get; set; } = "Username";
        public string Password { get; set; } = "Password";
        public string Title { get; set; } = "Order Management System";
        public string Role { get; set; } = "Admin";
        public string Email { get; set; } = "admin@nebil.com.tr";
    }
}
