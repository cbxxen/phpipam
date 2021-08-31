using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context){
            //check if there any users in DB. Return if there are users
            if(await context.Users.AnyAsync()) return;
            //read user Data from file
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            //store users in variable user data (deserialize Json)
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users){
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                //set PW and Salt for User
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("password"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}