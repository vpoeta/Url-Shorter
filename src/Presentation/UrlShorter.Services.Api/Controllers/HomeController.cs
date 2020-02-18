using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UrlShorter.Services.Api.Controllers
{
    public class HomeController : BaseController
    {
        [HttpGet]
        public IActionResult Get() => Ok("Url Shorter Service");
    }
}
