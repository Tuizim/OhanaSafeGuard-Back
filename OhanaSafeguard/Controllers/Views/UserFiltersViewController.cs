using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ohana.Infraestrutura;
using Ohana.MessageLibrary;

namespace OhanaSafeguard.Controllers.Views
{
    [ApiController]
    [Route("[controller]")]
    public class UserFiltersViewController : Controller
    {
        OhanaDbContext _db;

        public UserFiltersViewController(OhanaDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet("UserId")]
        public async Task< IActionResult >Get(int userId)
        {
            try
            {
                var filters = await _db.UserFilterViews.Where(f => f.UserId == userId).ToListAsync();
                if (filters == null)
                {
                    return NotFound();
                }
                return Ok(filters);
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorMessages.ServerError + ex.Message);
            }
        }
    }
}
