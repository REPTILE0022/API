using ECommerceAPI.Data.Models;
using System;
using System.Collections.Generic;

namespace ECommerceAPI.Models
{
	public class Order
	{
		public int Id { get; set; } // Primary Key

		public int UserId { get; set; } // Foreign Key to the User table
		public User User { get; set; } // Navigation property to User

		public DateTime OrderDate { get; set; } // The date when the order was placed

		public decimal TotalAmount { get; set; } // Total price of the order

		// One-to-Many relationship with OrderItem
		public List<OrderItem> OrderItems { get; set; } // Collection of order items
	}

	public class OrderItem
	{
		public int Id { get; set; } // Primary Key

		public int OrderId { get; set; } // Foreign Key to the Order table
		public Order Order { get; set; } // Navigation property to Order

		public int ProductId { get; set; } // Foreign Key to the Product table
		public Product Product { get; set; } // Navigation property to Product

		public int Quantity { get; set; } // Number of products ordered

		public decimal UnitPrice { get; set; } // Price of a single product at the time of order

		public decimal TotalPrice { get; set; } // Total price for this product in the order (Quantity * UnitPrice)
	}
}
