using Microsoft.AspNetCore.Mvc;

namespace PetHotel.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Http(int statusCode)
        {
            if (statusCode == 404) return View("NotFound");
            
            return View("Error");
        }
    }
}
