using CinemaCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace WebApplicationLab6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly CinemaContext _context;

        public UserController(CinemaContext context)
        {
            _context = context;
        }

        [HttpPost("[action]/")]
        public async Task<ActionResult<int>> Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    string hash;
                    using (MD5 md5 = MD5.Create())
                    {
                        hash = String.Join("", md5.ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
                    }
                    var newUser = new User
                    {
                        Login = user.Login,
                        Password = hash
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();
                    return Ok(user);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("[action]/")]
        public async Task<ActionResult<string>> SignIn(User user)
        {

            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    string hash;
                    using (MD5 md5 = MD5.Create())
                    {
                        hash = String.Join("", md5.ComputeHash(Encoding.UTF8.GetBytes(user.Password)));
                    }

                    var entity = _context.Users.Where(user => (user.Login == user.Login && user.Password == hash)).FirstOrDefault();
                    if(entity != null)
                    {
                        var jsonHeader = JsonSerializer.Serialize(new Header { alg = "HS256", typ = "JWT" });
                        string codeHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonHeader));

                        var expDate = ConvertDateTimeToInt32(DateTime.Now.AddDays(30));
                        var jsonPayload = JsonSerializer.Serialize(new Payload { id = entity.Id, exp = expDate });
                        string codePayload = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonPayload));

                        var jsonSignature = JsonSerializer.Serialize(new Signature { key = "I love C Sharp" });
                        var codeSecret = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonSignature));

                        var token = String.Join(".", codeHeader, codePayload, codeSecret);
                        return Ok(token);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private int ConvertDateTimeToInt32(DateTime dt)
        {
            return (int)(dt - new DateTime(2017, 1, 1)).TotalSeconds;
        }
    }
}
