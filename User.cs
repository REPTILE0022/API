using System;
using System.Collections.Generic;

namespace ECommerceAPI.Models
{
	public class User
	{
		public int Id { get; set; } // Primary Key

		public string FirstName { get; set; } // First name of the user
		public string LastName { get; set; } // Last name of the user

		public string Email { get; set; } // Email address (could be used as username)

		public string Password { get; set; } // Hashed password

		public string PhoneNumber { get; set; } // Phone number

		public DateTime DateJoined { get; set; } // The date the user registered

		// Relationship with Orders - A user can have many orders
		public List<Order> Orders { get; set; } // List of orders placed by the user
	}
}
