using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class AppUser
    {
        //identity Integer, this is std .net sharp stuff
        public int Id { get; set; }
        //Username String
        public string UserName {get; set;}

        //For Password Use
        public byte[] PasswordHash { get; set;}
        public byte[] PasswordSalt {get; set;}
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos {get; set;}

        //get Age with the date of birth
        //Get will automatically called if a Value is requested with the same name e.g. GetAge, GetCountry
    //    public int GetAge(){
    //        return DateOfBirth.CalculateAge();
    //    }
    }
}