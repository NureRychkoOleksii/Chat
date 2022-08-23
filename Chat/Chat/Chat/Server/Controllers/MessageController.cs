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
    public class MessageController : ControllerBase
    {
        private readonly AppContext _context;

        public MessageController(AppContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody]MessageDTO message)
        {
            await _context.Messages.AddAsync(new Message(){Content = message.Content, Chat = message.Chat, User = message.User, DateCreated = DateTime.Now});

            return Ok();
        }
    }
}
