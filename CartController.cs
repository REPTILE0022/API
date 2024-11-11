namespace ECommerceAPI.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using global::ECommerceAPI.Data;
	using global::ECommerceAPI.Models;

	namespace ECommerceAPI.Controllers
	{
		[Route("api/[controller]")]
		[ApiController]
		public class CartController : ControllerBase
		{
			private readonly AppDbContext _context;

			public CartController(AppDbContext context)
			{
				_context = context;
			}

			// GET: api/Cart/{userId}
			// Retrieve the cart for a specific user
			[HttpGet("{userId}")]
			public async Task<ActionResult<Cart>> GetCart(int userId)
			{
				var cart = await _context.Carts
					.Include(c => c.CartItems)
						.ThenInclude(ci => ci.Product)
					.FirstOrDefaultAsync(c => c.UserId == userId);

				if (cart == null)
				{
					return NotFound();
				}

				return Ok(cart);
			}

			// POST: api/Cart
			// Add a new item to the user's cart
			[HttpPost]
			public async Task<ActionResult<CartItem>> AddToCart(CartItem cartItem)
			{
				var cart = await _context.Carts
					.FirstOrDefaultAsync(c => c.UserId == cartItem.CartId);

				if (cart == null)
				{
					// Create a new cart if it doesn't exist
					cart = new Cart { UserId = cartItem.CartId, CartItems = new List<CartItem>() };
					_context.Carts.Add(cart);
					await _context.SaveChangesAsync();
				}

				cartItem.CartId = cart.Id;
				_context.CartItems.Add(cartItem);
				await _context.SaveChangesAsync();

				return CreatedAtAction(nameof(GetCart), new { userId = cart.UserId }, cartItem);
			}

			// PUT: api/Cart/{cartId}
			// Update item quantity in the cart
			[HttpPut("{cartId}")]
			public async Task<IActionResult> UpdateCartItem(int cartId, CartItem updatedCartItem)
			{
				var cartItem = await _context.CartItems.FindAsync(cartId);
				if (cartItem == null)
				{
					return NotFound();
				}

				cartItem.Quantity = updatedCartItem.Quantity;

				_context.Entry(cartItem).State = EntityState.Modified;
				await _context.SaveChangesAsync();

				return NoContent();
			}

			// DELETE: api/Cart/{itemId}
			// Remove an item from the cart
			[HttpDelete("{itemId}")]
			public async Task<IActionResult> RemoveFromCart(int itemId)
			{
				var cartItem = await _context.CartItems.FindAsync(itemId);
				if (cartItem == null)
				{
					return NotFound();
				}

				_context.CartItems.Remove(cartItem);
				await _context.SaveChangesAsync();

				return NoContent();
			}
		}
	}

}
