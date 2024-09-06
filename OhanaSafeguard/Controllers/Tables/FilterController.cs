using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ohana.Domain;
using Ohana.Infraestrutura;
using Ohana.MessageLibrary;

namespace OhanaSafeguard.Controllers.Tables
{
    [ApiController]
    [Route("[controller]")]
    public class FilterController : Controller
    {
        private OhanaDbContext _db;

        public FilterController(OhanaDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var filters = await _db.Filters.ToListAsync();
                if (filters == null)
                {
                    return NotFound();
                }
                else { return Ok(filters); }
            }
            catch
            {
                throw new Exception(ErrorMessages.ServerError);
            }
        }
        
        [HttpGet("UserId")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            try
            {
                var filters = await _db.Filters.FindAsync(userId);
                if (filters == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(filters);
                }
            }
            catch
            {
                throw new Exception(ErrorMessages.ServerError);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Filter filter)
        {
            var filters = await _db.Filters.AnyAsync(f => f.UserId == filter.UserId && f.Name == filter.Name);
            if (filters)
            {
                return BadRequest(ErrorMessages.JustExist);
            }
            _db.Filters.Add(filter);
            _db.SaveChanges();
            return Ok(filter);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Filter filter)
        {
            try
            {
                if (filter.Id > 0)
                {
                    _db.Filters.Remove(filter);
                    _db.SaveChanges();
                    return Ok(SuccessMessage.DeleteSuccess);
                }
                else
                {
                    var filterToDelete = await _db.Filters.FirstOrDefaultAsync(f => f.UserId == filter.UserId && f.Name == filter.Name);
                    if (filterToDelete != null)
                    {
                        _db.Filters.Remove(filterToDelete);
                        _db.SaveChanges();
                        return Ok(SuccessMessage.DeleteSuccess);
                    }
                }
                return BadRequest(ErrorMessages.CantDelete);
            }
            catch
            {
                return BadRequest(ErrorMessages.ServerError);
            }
        }
    }
}
