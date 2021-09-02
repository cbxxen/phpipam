//using Class or File Data/DataContext.cs
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helper;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IPhotoService _photoService;
        public UsersController(IUserRepository userRepository, IMapper mapper, 
            IPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //HTTP Function to return all Users
        [HttpGet]
        //returning type of ActionResult<IEnumerable<AppUser>>. IEnumerable is an easy way to get a List. More Advanced on would be List
        
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            //Ok to return a Task<ActionResult>
            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        //HTTP Function to get specific User, takes Parameter id. Call: localhost:5000/api/users/1
        [HttpGet("{username}", Name = "GetUser")]
        //Takes not the attribute int id, no IEnumerable now, it's not a list
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            //calls _userRepository (UserRepository.cs and its interface)
            return await _userRepository.GetMemberAsync(username);
        }
        
        //API to Add new Photo
        [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto([FromForm]IFormFile file){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            //Call AddPhotoAsync Function in PhotoService.cs
            var result = await _photoService.AddPhotoAsync(file);
            //if return is not null, there has been an error
            if(result.Error != null) return BadRequest(result.Error.Message);

            //Set Photo variable
            Photo photo = new Photo {
                url = result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };
            //set as main if it is the first Picture
            if(user.Photos == null){
                photo.isMain = true;
            };

            //Add photos to User
            user.Photos.Add(photo);

            //save changes to DB
            if(await _userRepository.SaveAllAsync()) {
               // return ;
                return CreatedAtRoute("GetUser", new {Username = user.UserName}, _mapper.Map<PhotoDto>(photo));
            };

            //if cant save, send Bad Request
            return BadRequest("Problem adding Photo");
        }

        [HttpPut("set-main-photo/{photoId}")]
        public async Task<ActionResult> SetMainPhoto(int photoId){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            //get Photo that matches ID of Photo
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if(photo.isMain) return BadRequest("This is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.isMain);
            //set current Main photo.ismain to false
            if(currentMain != null) currentMain.isMain =false;
            //set new photo as main
            photo.isMain = true;

            //update Database
            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest("failed to change main Photo");
        }
    
        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId){
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());

            //save photoToDelete in photo var
            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            //if no photo with id found, return Not Found
            if (photo == null) return NotFound();

            if (photo.isMain) return BadRequest("You cannot delete your main photo");

            if(photo.PublicId != null){
                var result = await _photoService.DeletePhotoAsync(photo.PublicId);
                //if Error return Bad Request
                if(result.Error != null) return BadRequest(result.Error.Message);
            }
            //remove photo from user
            user.Photos.Remove(photo);

            //try to update user db object
            if(await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to delete photo");
        }

        //Function to update User
        [HttpPut]
        public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdates){
        {
            var user = await _userRepository.GetUserByUsernameAsync(User.GetUsername());
            //Map memberUpdates (what has been changed) to user object
            _mapper.Map(memberUpdates, user);
            //call update Function
            _userRepository.Update(user);
            //If success return nothing
             if (await _userRepository.SaveAllAsync()) return NoContent();

            //if failed return Failed to update user
             return BadRequest("Failed to update user");
        }
    }
}
}