using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppContext = Chat.Server.Data.AppContext;

namespace Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppContext _context;

        public UserController(AppContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var usersAsync = await _context.Users.ToListAsync();

            return Ok(usersAsync);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);

            return Ok(user);
        }
    }
}