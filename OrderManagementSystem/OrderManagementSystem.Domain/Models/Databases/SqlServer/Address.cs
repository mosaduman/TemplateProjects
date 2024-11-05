using System.ComponentModel.DataAnnotations;

namespace OrderManagementSystem.Domain.Models.Databases.SqlServer;

public class Address : IBaseEntity
{
    [Key] public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Title { get; set; }
    public string City { get; set; }
    public string Distrinct { get; set; }
    public string FullAddress { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string TaxNumber { get; set; }
    public string TaxOffice { get; set; }
}