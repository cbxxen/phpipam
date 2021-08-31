//DTO is used to return users From usersController
//reason: Returning entity will not return the pictures of a user

using System;
using System.Collections.Generic;

namespace API.DTOs
{
    public class MemberDto
    {
        //identity Integer, this is std .net sharp stuff
        public int Id { get; set; }
        //Username String
        public string Username {get; set;}

        //PhotoUrl for Picture that is set as default
        public string PhotoUrl {get; set;}

        //For Password Use
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastActive { get; set; }
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDto> Photos {get; set;}
    }
}