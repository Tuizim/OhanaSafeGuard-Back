using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ohana.Infraestrutura;
using Ohana.MessageLibrary;
using System.Diagnostics.CodeAnalysis;

namespace OhanaSafeguard.Controllers.Tables
{
    [ApiController]
    [Route("[controller]")]
    public class DefaultFilterController : Controller
    {
        private OhanaDbContext _db;

        public DefaultFilterController(OhanaDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var filters = await _db.DefaultFilters.ToListAsync();
                if (filters == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(filters);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ErrorMessages.ServerError + ex.Message);
            }
        }
        
    }
}
