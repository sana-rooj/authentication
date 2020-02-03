using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using authentication.Models;
using System.Net.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace authentication.Models
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserCredentailsController : ControllerBase
    {
        private readonly authenticationContext _context;
        private readonly IConfiguration configuration;
        private static readonly HttpClient Client = new HttpClient();


        public UserCredentailsController(authenticationContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("getToken")] 
        public async Task<IActionResult> getNewToken( [FromBody] dynamic token)
        {
           
            //getting facebook auth credentials 
            FacebookAuthSettings authCred = new FacebookAuthSettings();
            authCred.AppId = configuration.GetSection("FacebookAuthSettings").GetValue<string>("AppId");
            authCred.AppSecret = configuration.GetSection("FacebookAuthSettings").GetValue<string>("AppSecret");
            //getting app access token from fb 
            var appAccessTokenResponse = await Client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={authCred.AppId}&client_secret={authCred.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<fbTokenResource>(appAccessTokenResponse);
            //validating user access token using identity token and app access token
            var userAccessTokenValidationResponse = await Client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={token}&access_token={appAccessToken.access_token}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<fbAccessTokenValidation>(userAccessTokenValidationResponse);
            if (!userAccessTokenValidation.data.IsValid)
            {
                return BadRequest("Invalid login");
            }
            //getting user data from fb
            var userInfoResponse = await Client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={token}");
            var userInfo = JsonConvert.DeserializeObject<UserCredentails>(userInfoResponse);

            string appToken = GenerateJSONWebToken(userInfo);

          
            return Ok(appToken);
        }

        private string GenerateJSONWebToken(UserCredentails user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
  

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
               claims: new Claim[]
              {
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("picture", user.Picture.Data.Url)
              }
,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
