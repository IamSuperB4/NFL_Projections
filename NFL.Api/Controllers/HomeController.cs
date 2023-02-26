using Microsoft.AspNetCore.Mvc;
using NFL.Business;
using NFL.Web.Controllers;
using Serilog;
using Serilog.Core;
using NFLWebsite;
using ILogger = Serilog.ILogger;

namespace NFL.Website.Controllers
{
    /// <summary>
    /// HttpGet requests for pages other than Admin Page
    /// </summary>
    [Route("api/[controller]")]
    public class HomeController : BaseController
    {
        private readonly IGamesService gamesService;

        public HomeController(IGamesService _gamesService)
        {
            gamesService = _gamesService;
        }

        /// <summary>
        /// Creates the link to the Google Maps coordinates
        /// </summary>
        /// <param name="vin">VIN</param>
        /// <returns>ActionResult with Google Maps link</returns>
        [HttpGet("test")]
        public async Task<ActionResult> Test()
        {
            ActionResult result;

            try
            {
                result = Ok("Worked");
            }
            catch (Exception ex)
            {
                result = BadRequest(ex);
            }

            return result;
        }
    }
}
