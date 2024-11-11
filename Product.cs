namespace ECommerceAPI.Data.Models
{
	public class Product
	{
		public int Id { get; set; } // Unique identifier for each product
		public string Name { get; set; } // Product name
		public decimal Price { get; set; } // Product price
		public string Description { get; set; } // Product description
	}
}
