//using Class or File Data/DataContext.cs
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //to call any API function here, User must be logged in (authorized)
    [Authorize]
    //constructor for this controller, inherit from baseApiController
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        //HTTP Function to return all Users
        [HttpGet]
        //returning type of ActionResult<IEnumerable<AppUser>>. IEnumerable is an easy way to get a List. More Advanced on would be List
        
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //Ok to return a Task<ActionResult>
            return Ok(await _userRepository.GetMembersAsync());
        }

        //ask for authorizations
        [Authorize]
        //HTTP Function to get specific User, takes Parameter id. Call: localhost:5000/api/users/1
        [HttpGet("{username}")]
        //Takes not the attribute int id, no IEnumerable now, it's not a list
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //calls _userRepository (UserRepository.cs and its interface)
            return await _userRepository.GetMemberAsync(username);
        }
        

        //Function to update User
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdates){
            //Get Username of User (with Token)
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Get User Object
            var user = await _userRepository.GetUserByUsernameAsync(username);
            //Map memberUpdates (what has been changed) to user object
            _mapper.Map(memberUpdates, user);
            //call update Function
            _userRepository.Update(user);
            //If success return nothing
            if(await _userRepository.SaveAllAsync()) return NoContent();
            //if failed return Failed to update user
            return BadRequest("Failed to update User");
        }
    }
}