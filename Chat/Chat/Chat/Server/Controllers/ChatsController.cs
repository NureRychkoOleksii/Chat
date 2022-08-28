using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var chatsAsync = await _context.Chats.Include(u => u.Users).ToListAsync();
            var chats = new List<ChatDTO>();
            foreach (var ch in chatsAsync)
            {
                chats.Add(new ChatDTO
                {
                    IsPrivate = ch.IsPrivate, Id = ch.Id, ChatName = ch.ChatName,
                    UsersId = ch.Users.Select(u => u.Id).ToList()
                });
            }
            
            return Ok(chats);
        }

        [HttpGet("get/{name}")]
        public async Task<IActionResult> GetChatByName(string name)
        {
            var chatsAsync = await _context.Chats.ToListAsync();
            var chats = new List<ChatDTO>();
            chatsAsync.ForEach(ch =>
            {
                chats.Add(new ChatDTO()
                    {Id = ch.Id, ChatName = ch.ChatName, UsersId = ch.Users.Select(u => u.Id).ToList()} );
            });
            return Ok(chats.FirstOrDefault(ch => ch.ChatName == name));
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody]ChatToApiDTO chatToApi)
        {
            var users = await _context.Users.ToListAsync();
            var firstUser = users.FirstOrDefault(u => u.Name == chatToApi.FirstUserName);
            var secondUser = users.FirstOrDefault(u => u.Name == chatToApi.SecondUserName);
            users = new List<User>()
            {
                firstUser,
                secondUser
            };
            var chatToCreate = new Shared.Models.Chat() {ChatName = chatToApi.ChatName, Users = users};

            await _context.Chats.AddAsync(chatToCreate);
            await _context.SaveChangesAsync();

            return Ok(new ChatDTO(){ChatName = chatToCreate.ChatName, Id=chatToCreate.Id,
                IsPrivate = chatToCreate.IsPrivate, UsersId = chatToCreate.Users.Select(u => u.Id).ToList()});
        }

        [HttpPut("addUser")]
        public async Task<IActionResult> AddUserToChat([FromBody]ChatToUpdateDTO chatDto)
        {
            var chat = await _context.Chats.Include(ch => ch.Users)
                .FirstOrDefaultAsync(ch => ch.ChatName == chatDto.ChatName);

            var user = await _context.Users.Include(u => u.Chats)
                .FirstOrDefaultAsync(u => u.Id == chatDto.userToUpdate);
            
            
            chat.Users.Add(user);
            _context.Chats.Update(chat);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        
    }
}
