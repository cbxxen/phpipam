using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //Type of Controller
    [ApiController]
    //way to controller; in this case: localhost:5000/api
    [Route("api/[controller]")]
    
    public class BaseApiController : ControllerBase
    {
        
    }
}