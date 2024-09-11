using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ohana.Infraestrutura;
using Ohana.MessageLibrary;

namespace OhanaSafeguard.Controllers.Views
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialViewController : Controller
    {
        OhanaDbContext _db;

        public CredentialViewController(OhanaDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        [HttpGet ("UserRow")]
        public async Task<ReturnMessage> Get(string userRow)
        {
            try
            {
                var filter = await _db.CredentialViews.Where(cv => cv.UserRow == userRow).ToListAsync();
                if (filter == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound,false);
                }
                return new ReturnMessage(filter, SuccessMessage.GetSuccess, true);

            }
            catch (Exception ex)
            {
                return new ReturnMessage(ErrorMessages.ServerError + ex.Message, false);
            }
        }
    }
}
