using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Shared.Models;
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
            var usersAsync = await _context.Users.Include(u => u.Chats).ToListAsync();

            var users = new List<Shared.Models.UserAndChatDTOS.UserDTO>();
            usersAsync.ForEach(u =>
            {
                users.Add(new Shared.Models.UserAndChatDTOS.UserDTO()
                    {Id = u.Id,Name = u.Name, ChatsId = u.Chats.Select(i => i.Id).ToList()} );
            });
            return Ok(users);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var usersAsync = await _context.Users.ToListAsync();
            var users = new List<Shared.Models.UserAndChatDTOS.UserDTO>();
            usersAsync.ForEach(u =>
            {
                users.Add(new Shared.Models.UserAndChatDTOS.UserDTO()
                    {Id = u.Id, Name = u.Name, ChatsId = u.Chats.Select(i => i.Id).ToList()} );
            });

            return Ok(users.FirstOrDefault(u => u.Name == name));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserDTO user)
        {
            await _context.Users.AddAsync(new User() {Name = user.Name, Pass = user.Pass});
            await _context.SaveChangesAsync();
            return Ok();
        }
        
    }
}