using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain.Models.Databases.SqlServer
{
    public class Order : IBaseEntity
    {
        [Key] public int Id { get; set; }
        public string CustomId { get; set; }
        public int CustomerId { get; set; }
        public int ShipmentAddressId { get; set; }
        public int InvoiceAddressId { get; set; }
        public decimal Amount { get; set; }
        public decimal Discount { get; set; }
    }
}
