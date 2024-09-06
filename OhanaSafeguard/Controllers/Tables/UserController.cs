using Elfie.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Ohana.Domain;
using Ohana.Infraestrutura;
using Ohana.MessageLibrary;
using System.Resources;

namespace OhanaSafeguard.Controllers.Tables
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : Controller
    {
        private OhanaDbContext _db;

        public UserController(OhanaDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var user = await _db.Users.ToListAsync();
                if (user == null)
                {
                    return NotFound();
                }
                else { return Ok(user); }
            }
            catch
            {
                return BadRequest(ErrorMessages.ServerError);
            }
        }
        
        [HttpGet ("UserId")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var user = await _db.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                else { return Ok(user); }
            }
            catch
            {
                return BadRequest(ErrorMessages.ServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                return Ok(user);
            }
            catch
            {
                return BadRequest(ErrorMessages.ServerError);
            }
        }
    }
}
