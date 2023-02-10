using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_Example.Controllers
{
    [Route("api/[action]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        [Authorize]
        public JsonResult Protected()
        {
            return Json(new
            {
                Value = Random()
            });
        }

        [HttpGet]
        [AllowAnonymous]
        public JsonResult Public()
        {
            return Json(new
            {
                Value = Random()
            });
        }

        private int Random()
        {
            return new System.Random().Next(0, 1000);
        }
    }
}
