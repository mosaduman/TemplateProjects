namespace OrderManagementSystem.WebUI.Api.Models;

public class Order
{
    public int Id { get; set; }
    public string CustomId { get; set; }
    public int CustomerId { get; set; }
    public int ShipmentAddressId { get; set; }
    public int InvoiceAddressId { get; set; }
    public int Amount { get; set; }
    public int Discount { get; set; }
}