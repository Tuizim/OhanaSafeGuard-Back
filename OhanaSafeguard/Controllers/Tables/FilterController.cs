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
        public async Task<ReturnMessage> Get()
        {
            try
            {
                var filters = await _db.Filters.ToListAsync();
                if (filters == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound, false);
                }
                return new ReturnMessage(message: SuccessMessage.GetSuccess, success: true, response: filters);
            }
            catch
            {
                return new ReturnMessage(ErrorMessages.ServerError, false);
            }
        }

        [HttpGet("userrow")]
        public async Task<ReturnMessage> GetByUser(string userrow)
        {
            try
            {
                var filters = await _db.Filters.Where(f => f.UserRow == userrow).ToListAsync();
                if (filters == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound, false);
                }
                return new ReturnMessage(filters, SuccessMessage.GetSuccess, true);
            }
            catch
            {
                return new ReturnMessage(ErrorMessages.ServerError, false);
            }
        }
        [HttpGet("UserId/UserRow")]
        public ReturnMessage GetById(int id, string userrow)
        {
            try
            {
                var filter = _db.Filters.Where(f => f.Id == id &&  f.UserRow == userrow).FirstOrDefault();
                if (filter == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound,false);
                }
                return new ReturnMessage(filter, SuccessMessage.GetSuccess, true);
            }
            catch
            {
                return new ReturnMessage(ErrorMessages.ServerError,false);
            }
        }
        [HttpPost]
        public async Task<ReturnMessage> Post([FromBody] Filter filter)
        {
            try
            {
                var userExist = _db.Users.Any(u => u.UserRow == filter.UserRow);
                if (userExist == false)
                {
                    return new ReturnMessage(ErrorMessages.CantDelete, false);
                }

                var filters = await _db.Filters.AnyAsync(f => f.UserRow == filter.UserRow && f.Name == filter.Name);
                if (filters)
                {
                    return new ReturnMessage(ErrorMessages.JustExist, false);
                }
                var filterAny = _db.Filters.Any(f => f.UserRow == filter.UserRow && f.Id == filter.Id);
                if (filterAny) 
                {
                    _db.Filters.Update(filter);
                    _db.SaveChanges();
                    return new ReturnMessage(message: SuccessMessage.UpdateSuccess, success: true, response: filters);

                }
                _db.Filters.Add(filter);
                _db.SaveChanges();
                return new ReturnMessage(message: SuccessMessage.InsertSuccess, success: true, response: filters);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(ErrorMessages.ServerError + ex.Message, false);
            }
        }

        [HttpDelete]
        public async Task<ReturnMessage> Delete([FromBody] Filter filter)
        {
            try
            {
                var userExist = _db.Users.Any(u => u.UserRow == filter.UserRow);
                if (userExist == false)
                {
                    return new ReturnMessage(ErrorMessages.CantDelete, false);
                }

                var filterInUse = _db.credentialStorages.Any(cs => cs.Filter == filter.Id);
                if (filterInUse == true)
                {
                    return new ReturnMessage(ErrorMessages.CantDeleteFilter, false);
                }
                if (filter.Id > 0)
                {
                    _db.Filters.Remove(filter);
                    _db.SaveChanges();
                    return new ReturnMessage(SuccessMessage.DeleteSuccess, true);
                }
                else
                {
                    var filterToDelete = await _db.Filters.FirstOrDefaultAsync(f => f.UserRow == filter.UserRow && f.Name == filter.Name);
                    if (filterToDelete != null)
                    {
                        _db.Filters.Remove(filterToDelete);
                        _db.SaveChanges();
                        return new ReturnMessage(SuccessMessage.DeleteSuccess, true);
                    }
                }
                return new ReturnMessage(ErrorMessages.CantDelete, false);
            }
            catch
            {
                return new ReturnMessage(ErrorMessages.CantDelete, false);
            }
        }
    }
}
