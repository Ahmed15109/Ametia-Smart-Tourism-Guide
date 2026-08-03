using Grad.Repo;
using Grad.Repo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Grad.Controllers
{
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepoBase<User> _usre;

        public UserController(IRepoBase<User> usre)
        {
            _usre = usre;
        }
        private static string HashPassword(string Password)
        {
            return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)));
        }
        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromForm] User? user)
        {
            if (user == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            User? existingUser = await _usre.GetAsyncByParameter(user.Email);
            if (existingUser != null)
            {
                return Ok("Email Exists, You Have An Account");
            }

            if (user.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await user.ImageFile.CopyToAsync(memoryStream);
                    user.ImageBytes = memoryStream.ToArray(); // ✅ Store image as byte[]
                }
            }

            user.Password = HashPassword(user.Password);
            await _usre.CreateAsync(user);
            return Ok("User Added");
        }


        [HttpPost("Login/{login}/{Password}")]
        public async Task<IActionResult> Login(string? login, string? Password)
        {
            try
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(Password))
                {
                    return Ok("INVALID Pass");
                }

                User? user1 = await _usre.GetAsyncByParameter(login);
                if (user1 == null)
                    return Ok("INVALID Email");

                var res = HashPassword(Password);
                if (!user1.Password.Equals(res))
                    return Ok("Invalid Email Or Password");

                HttpContext.Session.SetString("Email", login.ToString());
                return Ok("Done");
            }
            catch
            {
                return Ok("Invalid Password Or Email");
            }
        }
        [HttpGet("LoadUserById/{id}")]
        public async Task<IActionResult> LoadUserById(int id)
        {
            var place = await _usre.GetByIdAsync(id);
            if (place == null)
                return NotFound(); // مهم جداً

            return Ok(place);
        }


        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] User? user)
        {
            if (user == null || !ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Not Valid Update");
                return Ok("Not Valid Update");
            }

            User? olduser = await _usre.GetAsyncByParameter(user.Email);
            if (olduser == null)
            {
                return NotFound("User not found.");
            }
            if (user.ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await user.ImageFile.CopyToAsync(memoryStream);
                    user.ImageBytes = memoryStream.ToArray();
                }
                olduser.ImageBytes = user.ImageBytes;
            }

            user.Password = HashPassword(user.Password);
            string? Email = user.Email;
            olduser.BDate = user.BDate;
            olduser.SacondName = user.SacondName;
            olduser.FirstName = user.FirstName;
            olduser.Email = user.Email;
            olduser.City = user.City;
            olduser.Password = user.Password;

            await _usre.UpdateAsync(olduser);
            return Ok("Up Done");
        }
        [HttpPost("ForgotPassword/{Email}")]
        public async Task<IActionResult> ForgotPassword(string? Email)
        {
            if (!ModelState.IsValid || Email == null)
            {
                ModelState.AddModelError("Error", "InValid Email");
                return Ok("InValid Email ");
            }
            User? user = await _usre.GetAsyncByParameter(Email);
            if (user == null)
            {
                ModelState.AddModelError("Error", "You Do not Have Acount ");
                return Ok("InValid Email ");
            }
            HttpContext.Session.SetString("Email", Email);
            return Ok("Founed Acount");

        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _usre.GetAsyncAll());
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            return Ok(await _usre.DeleteAsync(id));
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> Profile()
        {
            string? email =  HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized("⚠️ Session expired or user not logged in.");
            }

            User? user = await _usre.GetAsyncByParameter(email);

            if (user == null)
            {
                return NotFound("❌ User not found.");
            }

            return Ok(user);
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok("Logged out");
        }


    }

}
