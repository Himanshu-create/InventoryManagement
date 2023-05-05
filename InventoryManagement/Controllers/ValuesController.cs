using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/callme")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public ActionResult Get()
        {

            return Ok(1);
        }
    }
}
