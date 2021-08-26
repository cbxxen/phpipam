//using Class or File Data/DataContext.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
//Using MVC (model, view controller -> ControllerBase)
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //Type of Controller
    [ApiController]
    //way to controller; in this case: localhost:5000/api
    [Route("api/[controller]")]
    //constructor for this controller
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //HTTP Function to return all Users
        [HttpGet]
        //returning type of ActionResult<IEnumerable<AppUser>>. IEnumerable is an easy way to get a List. More Advanced on would be List
        
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            //calls DataContext.Users.get and brings it to a List
            return await _context.Users.ToListAsync();
        }

        //HTTP Function to get specific User, takes Parameter id. Call: localhost:5000/api/users/1
        [HttpGet("{id}")]
        //Takes not the attribute int id, no IEnumerable now, it's not a list
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            //calls Datacontext in Users and uses Extensions function Find
            return await _context.Users.FindAsync(id);
        }
    }
}