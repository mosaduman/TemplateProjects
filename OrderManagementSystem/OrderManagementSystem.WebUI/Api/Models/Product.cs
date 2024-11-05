namespace OrderManagementSystem.WebUI.Api.Models;

public class Product
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public string Barcode { get; set; }
    public string Title { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int Vat { get; set; }
}