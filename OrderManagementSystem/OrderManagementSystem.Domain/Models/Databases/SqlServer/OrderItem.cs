using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain.Models.Databases.SqlServer;

public class OrderItem : IBaseEntity
{
    [Key]
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }

}