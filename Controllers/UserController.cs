using LibraryServicesAPI.DTO;
using LibraryServicesAPI.Models;
using LibraryServicesAPI.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace LibraryServicesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount([FromBody] User user)
        {
            var isuseraccountcreated = _user.CreateUserAccount(user);
            if (isuseraccountcreated)
            {
                return Ok(new { msg = "Account Created Successfully" });
            }
            else
            {
                return Conflict(new { msg = $"Account Already Exists for {user.UserName}" });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLogin userlogin)
        {
            string username = userlogin.Username;
            string HashedPass = _user.CreateHashedPassword(userlogin.Password);

            if (_user.IsuserExists(username, HashedPass))
            {
                return Ok(new { message = "Logged in Successfully" });
            }
            return NotFound(new { message = "User Not Found" });
        }

        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword([FromBody] ResetPassword resetPassword)
        {
            try
            {
                string username = resetPassword.Username;
                string userpassword = _user.CreateHashedPassword(resetPassword.OldPassword);
                if (_user.IsuserExists(username, userpassword))
                {
                    var isvalidpass = _user.IsBothPasswordsAreValid(resetPassword.NewPassword, resetPassword.ReEnterNewPassword);
                    if (isvalidpass)
                    {
                        var isResetSucessfully = _user.ResetPassword(resetPassword);
                        if (isResetSucessfully)
                        {
                            return Ok(new { message = "Password Reset Successfully" });
                        }
                        return Conflict(new { message = "Error Occurs while resetting the New Password" });
                    }
                    return Conflict(new { message = "Password Mismatched. Re-Enter the Same Password again" });
                }
                return NotFound(new { message = "Invalid Username or Password" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = $"Error Occuted in the ResetPassword API : {ex.Message} " });
            }
        }

    }
}
