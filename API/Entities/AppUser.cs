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
    }
}