﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task< ReturnMessage >Get(int userId)
        {
            try
            {
                var filters = await _db.UserFilterViews.Where(f => f.UserId == userId).ToListAsync();
                if (filters == null)
                {
                    return new ReturnMessage(ErrorMessages.NotFound , false);
                }
                return new ReturnMessage(message: SuccessMessage.GetSuccess, response: filters, success: true);
            }
            catch (Exception ex)
            {
                return new ReturnMessage(ErrorMessages.ServerError + ex.Message, false);
            }
        }
    }
}
