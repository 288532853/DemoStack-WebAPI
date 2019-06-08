using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.BAL;
using BO;
using WebAPI.Models;
using Utility;


namespace WebAPI.Controllers
{
    [Log]
    public class UserController : ApiController
    {
        // GET api/User
        //   [HttpGet]
        //[BasicAuthentication]
        [Route("~/api/User/Login")]
        public IHttpActionResult Get(string email, string password)
        {
            if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                //if(UserRepository.Login(email: email.Trim(), password: password.Trim(), userType: Enums.UserType.Administrator) == Enums.UserLoginStatus.Success)
                var user = UserRepository.FindUser(email: email.Trim(), password: password.Trim());
                if (user != null)
                {
                    return Ok(user.Tokan);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        // POST api/User
        [HttpPost]
        [Route("~/api/User/AddUpdateUser")]
        //[ActionName("AddUpdateUser")]
        public IHttpActionResult Post(User user)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (user != null)
            {
                try
                {
                    user.Tokan = Utilities.GenerateToken(user.Email,user.Password, Dns.GetHostEntry(Dns.GetHostName()).HostName, DateTime.Now, Utilities.GenerateRandomNumber());
                    UserRepository.AddUpdateUser(user);
                }
                catch (Exception ex)
                {
                    return InternalServerError(new Exception(ex.Message.ToString()));
                }
                return Ok(user.Id);

            }
            return NotFound();

        }

    }//end class
}//end namespace
