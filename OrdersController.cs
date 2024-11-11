using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly AppDbContext _context;

		public OrderController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/Order/{userId}
		// Retrieve all orders for a specific user
		[HttpGet("{userId}")]
		public async Task<ActionResult<List<Order>>> GetOrdersByUser(int userId)
		{
			var orders = await _context.Orders
				.Where(o => o.UserId == userId)
				.Include(o => o.OrderItems)
					.ThenInclude(oi => oi.Product)
				.ToListAsync();

			if (orders == null || !orders.Any())
			{
				return NotFound("No orders found for this user.");
			}

			return Ok(orders);
		}

		// POST: api/Order
		// Create a new order
		[HttpPost]
		public async Task<ActionResult<Order>> CreateOrder(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetOrdersByUser), new { userId = order.UserId }, order);
		}

		// PUT: api/Order/{orderId}
		// Update an existing order
		[HttpPut("{orderId}")]
		public async Task<IActionResult> UpdateOrder(int orderId, Order updatedOrder)
		{
			if (orderId != updatedOrder.Id)
			{
				return BadRequest("Order ID mismatch.");
			}

			_context.Entry(updatedOrder).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(orderId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// DELETE: api/Order/{orderId}
		// Delete an order
		[HttpDelete("{orderId}")]
		public async Task<IActionResult> DeleteOrder(int orderId)
		{
			var order = await _context.Orders.FindAsync(orderId);
			if (order == null)
			{
				return NotFound();
			}

			_context.Orders.Remove(order);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// Helper method to check if an order exists
		private bool OrderExists(int id)
		{
			return _context.Orders.Any(e => e.Id == id);
		}
	}
}
