using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Server.Data;
using Chat.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppContext = Chat.Server.Data.AppContext;

namespace Chat.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly AppContext _context;

        public ChatsController(AppContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _context.Chats.ToListAsync();

            return Ok(chats);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody]ChatDTO chat)
        {
            var users = await _context.Users.ToListAsync();
            var firstUser = users.FirstOrDefault(u => u.Name == chat.FirstUserName);
            var secondUser = users.FirstOrDefault(u => u.Name == chat.SecondUserName);

            users = new List<User>()
            {
                firstUser,
                secondUser
            };
            
            await _context.Chats.AddAsync(new Shared.Models.Chat() {ChatName = chat.ChatName, Users = users});
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
