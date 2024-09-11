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
        public async Task<ReturnMessage> GetUserRow( string login,string password)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
                if (user == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound, false);
                }
                return new ReturnMessage(success: true, message: SuccessMessage.GetSuccess, response: user.UserRow);
            }
            catch
            {
                return new ReturnMessage(ErrorMessages.ServerError, false);
            }
        }
    }
}
