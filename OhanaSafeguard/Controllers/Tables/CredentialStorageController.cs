using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ohana.Domain;
using Ohana.Infraestrutura;
using Ohana.MessageLibrary;
using System.Net;

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
        [HttpGet("Userrow")]
        public async Task<ReturnMessage> Get(string userrow)
        {
            try
            {
                var credentials = await _db.credentialStorages.Where(x => x.UserRow == userrow).ToListAsync();
                if (credentials == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound, false);
                }
                return new ReturnMessage(message: SuccessMessage.GetSuccess, success: true, response: credentials);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(message: ErrorMessages.ServerError + ex.Message, success: false);
            }
        }
        [HttpGet("UserRow/CredentialId")]
        public ReturnMessage GetById(string userrow, int credentialId)
        {
            try
            {
                var credential = _db.credentialStorages.Where(cs => cs.UserRow == userrow && cs.ID == credentialId).FirstOrDefault();
                if (credential == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound, false);
                }
                return new ReturnMessage(credential,SuccessMessage.GetSuccess,true);
            }
            catch (Exception ex) 
            {
                return new ReturnMessage(ErrorMessages.ServerError + ex.Message, success: false);
            }
        }
        [HttpPost]
        public ReturnMessage Post([FromBody] CredentialStorage credentialStorage)
        {
            try
            {
                var userExist = _db.Users.Any(u => u.UserRow == credentialStorage.UserRow);
                if (userExist == false)
                {
                    return new ReturnMessage(ErrorMessages.UserNotFound, false);
                }
                var csAny = _db.credentialStorages.Any(cs => cs.ID == credentialStorage.ID);
                if (csAny)
                {
                    return new ReturnMessage(ErrorMessages.CantDeleteFilter, false);
                }
                if (csAny || credentialStorage.ID ==-1)
                {
                    _db.Update(credentialStorage);
                    _db.SaveChanges();
                    return new ReturnMessage(SuccessMessage.UpdateSuccess, true);
                }
                credentialStorage.ID = 0;
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
                var userExist = _db.Users.Any(u => u.UserRow == credential.UserRow);
                if (userExist == false)
                {
                    return new ReturnMessage(ErrorMessages.CantDelete, false);
                }
                var csAny = _db.credentialStorages.Any(cs => cs.ID == credential.ID && cs.UserRow == credential.UserRow);
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
