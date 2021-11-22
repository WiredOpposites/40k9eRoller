using _40k9eRoller.Shared;
using Microsoft.AspNetCore.Mvc;

namespace _40k9eRoller.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RollController : ControllerBase
    {
        public RollController()
        {

        }

        [HttpPost]
        public FullResults GetResults([FromBody] RollRequest request)
        {
            return new FullResults();
        }
    }
}
