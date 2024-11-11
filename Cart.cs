using ECommerceAPI.Data.Models;
using System.Collections.Generic;

namespace ECommerceAPI.Models
{
	public class Cart
	{
		public int Id { get; set; } // Primary Key

		public int UserId { get; set; } // Foreign key linking to the User

		public List<CartItem> CartItems { get; set; } // List of items in the cart

		// Navigation property to the User
		public User User { get; set; }
	}

	public class CartItem
	{
		public int Id { get; set; } // Primary Key

		public int ProductId { get; set; } // Foreign key linking to the Product

		public int Quantity { get; set; } // Quantity of the product in the cart

		// Navigation properties
		public Product Product { get; set; } // The product associated with the cart item
		public int CartId { get; set; } // Foreign key linking to the Cart
		public Cart Cart { get; set; } // The cart to which this item belongs
	}
}
