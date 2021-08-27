/*
Class used for everything that has to do with tokens. 
Used by AccountController.cs
*/

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        //Encryption Key used
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config){
            //creation of new Key
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }
 /******************************************** Create Token **************************************************/    
        public string CreateToken(AppUser user)
        {
            //create claim Variable with the username in it
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName )
            };

            //create credentials
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);   

            //create Authentication Token
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                //When does it expires
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            //needs to be created to create token
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return Token
            return tokenHandler.WriteToken(token);
        }
    }
}