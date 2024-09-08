using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ohana.Domain;
using Ohana.Infraestrutura;
using Ohana.MessageLibrary;

namespace OhanaSafeguard.Controllers.Tables
{
    [ApiController]
    [Route("[controller]")]
    public class CredentialStorageController : Controller
    {
        OhanaDbContext _db;
        public CredentialStorageController(OhanaDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        [HttpGet("UserId")]
        public async Task<ReturnMessage> Get(int userId)
        {
            try
            {
                var credentials = await _db.credentialStorages.Where(x => x.UserId == userId).ToListAsync();
                if (credentials == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound, false);
                }
                return new ReturnMessage(message: SuccessMessage.GetSuccess, success: false, response: credentials);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(message: ErrorMessages.ServerError + ex.Message, success: false);
            }
        }
        [HttpPost]
        public ReturnMessage Post([FromBody] CredentialStorage credentialStorage)
        {
            try
            {
                var csAny = _db.credentialStorages.Any(cs => cs.ID == credentialStorage.ID);
                if (csAny)
                {
                    _db.Update(credentialStorage);
                    _db.SaveChanges();
                    return new ReturnMessage(SuccessMessage.UpdateSuccess, true);
                }
                _db.credentialStorages.Add(credentialStorage);
                _db.SaveChanges();
                return new ReturnMessage(success: true , message: SuccessMessage.InsertSuccess);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(ErrorMessages.ServerError + ex.Message , false);
            }
        }
        [HttpDelete]
        public ReturnMessage Delete([FromBody] CredentialStorage credential)
        {
            try
            {
                var csAny = _db.credentialStorages.Any(cs => cs.ID == credential.ID && cs.UserId == credential.UserId);
                if (csAny)
                {
                    _db.credentialStorages.Remove(credential);
                    _db.SaveChanges();
                    return new ReturnMessage(SuccessMessage.DeleteSuccess, true);
                }
                return new ReturnMessage(ErrorMessages.CantDelete, false);
            }
            catch
            {
                return new ReturnMessage(ErrorMessages.ServerError, false);
            }
        }
    }
}
