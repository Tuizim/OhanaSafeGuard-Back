using Microsoft.AspNetCore.Mvc;

namespace OhanaSafeguard.Controllers.Views
{
    [ApiController]
    [Route("[controller]")]
    public class UserFiltersViewController : Controller
    {
        [HttpGet]
        public async Task< IActionResult >Get()
        {
        }
    }
}
