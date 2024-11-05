using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain.Models.Databases.SqlServer;

public class Customer : IBaseEntity
{
    [Key] public int Id { get; set; }
    public string Title { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}