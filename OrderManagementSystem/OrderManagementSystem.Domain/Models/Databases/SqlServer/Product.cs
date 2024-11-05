using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain.Models.Databases.SqlServer;

public class Product : IBaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Sku { get; set; }
    public string Barcode { get; set; }
    public string Title { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Vat { get; set; }
}