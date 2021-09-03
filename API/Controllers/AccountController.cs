/*
The Class is used to Authenticate a User and give him a Token which he uses to authenticate in future
*/

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        //_context gets defined to DataContext (Function to make changes in DB)
        public AccountController(DataContext context, ITokenService tokenService,IMapper mapper)
        {
            _context = context;
            _tokenService = tokenService;
            _mapper = mapper;
        }


 /******************************************** REGISTERING USER **************************************************/


        //http Post definition for registering user
        //api/account/register
        [HttpPost("register")]
        //Using DTO 
        public async Task<ActionResult<UserDto>> Register(RegisterDTO registereDto){
           
           //check if Username is taken, return BadRequest if exists
           if(await UserExists(registereDto.Username)) return BadRequest("Username is taken");

            var user = _mapper.Map<AppUser>(registereDto);

            //"using" gurantees that the data is disposed/deleted after usage of class (HMACSHA512)
            //initializing the Hash Function object
            using var hmac = new HMACSHA512();

            
            user.UserName= registereDto.Username.ToLower();
            //Create Hash From PW (HMACSHA512 auto greates a random key)
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registereDto.Password));
            user.PasswordSalt = hmac.Key;

            //enables tracking in context
            _context.Users.Add(user);
            //calling db and save user async
            await _context.SaveChangesAsync();

            //Return DTO with username and Token fields
            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs
            };
        }

 /******************************************** Login Part **************************************************/        
        //api/account/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            var user = await _context.Users
                //also save photos of Users to user var
                .Include(p => p.Photos)
                .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
            
            //if no sequence
            if (user == null) return Unauthorized("Invalid username");

            //second instance of HMACSHA512 needs key 
            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            //Go through each byte of new created Hash
            for (int i = 0; i<ComputeHash.Length; i++){
                //check if byte is the same as the User PW from DB
                if (ComputeHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }

            //Return DTO with username and Token fields
            return new UserDto{
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                photoUrl = user.Photos.FirstOrDefault(x => x.isMain)?.url,
                KnownAs = user.KnownAs
            };
        }

 /******************************************** Helper Methods **************************************************/
        //private method to check if whether username is taken (false if not, true if exists)
        private async Task<bool> UserExists(string username){
            //check if user exists. Make to Lower Key to compare same to same
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}