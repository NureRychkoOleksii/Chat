using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Server.Data;
using Chat.Shared.Models;
using Chat.Shared.Models.UserAndChatDTOS;
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
        public async Task<IActionResult> AddMessage([FromBody]MessageTOApiDTO messageToApi)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(ch => ch.ChatName == messageToApi.Chat);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == messageToApi.User);
            await _context.Messages.AddAsync(new Message()
                {Content = messageToApi.Content, Chat = chat, User = user, DateCreated = DateTime.Now});
            await _context.SaveChangesAsync();

            return Ok();
        }
        
        [HttpPut("changeMessage")]
        public async Task<IActionResult> ChangeMessage([FromBody]MessageDTO message)
        {
            var messageToUpdate = await
                _context.Messages.FirstOrDefaultAsync(m =>
                    m.User.Id == message.User && message.Content == m.Content);

            messageToUpdate.Content = message.PreviousMessage;
            
            _context.Messages.Update(messageToUpdate);
            await _context.SaveChangesAsync();
            
            return Ok();
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetMessagesFromChat(int chatId)
        {
            var messages = await _context.Messages.Include(m =>m.Chat)
                .Include(m=>m.User).Where(m => m.Chat.Id == chatId).ToListAsync();

            var messagesToReturn = messages.Select(m => new MessageDTO()
            {
                Content = m.Content, Chat = m.Chat.Id, Id = m.Id,
                User = m.User.Id, DateCreated = m.DateCreated, PreviousMessage = string.Empty
            }).ToList();

            return Ok(messagesToReturn.OrderBy(m=> m.DateCreated).ToList());
        }

        [HttpDelete("{messageId}")]
        public async Task<IActionResult> DeleteMessage(int messageId)
        {
            _context.Messages.Remove(await _context.Messages.FirstOrDefaultAsync(m => m.Id == messageId));
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
