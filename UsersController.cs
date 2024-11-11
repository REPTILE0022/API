﻿using Microsoft.AspNetCore.Mvc;
using ECommerceAPI.Data;
using ECommerceAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly AppDbContext _context;

		public UserController(AppDbContext context)
		{
			_context = context;
		}

		// GET: api/User
		// Retrieve all users
		[HttpGet]
		public async Task<ActionResult<IEnumerable<User>>> GetUsers()
		{
			var users = await _context.Users.ToListAsync();
			return Ok(users);
		}

		// GET: api/User/5
		// Retrieve a user by their ID
		[HttpGet("{id}")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		// POST: api/User
		// Create a new user
		[HttpPost]
		public async Task<ActionResult<User>> CreateUser(User user)
		{
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
		}

		// PUT: api/User/5
		// Update user details
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, User updatedUser)
		{
			if (id != updatedUser.Id)
			{
				return BadRequest();
			}

			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			user.FirstName = updatedUser.FirstName;
			user.LastName = updatedUser.LastName;
			user.Email = updatedUser.Email;
			user.Password = updatedUser.Password; // In practice, make sure to hash the password
			user.PhoneNumber = updatedUser.PhoneNumber;

			_context.Users.Update(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/User/5
		// Delete a user by their ID
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			var user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			_context.Users.Remove(user);
			await _context.SaveChangesAsync();

			return NoContent();
		}
	}
}
