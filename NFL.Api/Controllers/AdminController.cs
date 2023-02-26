using Microsoft.AspNetCore.Mvc;
using NFL.Business;
using NFL.Dto;
using Serilog;
using System.Threading.Tasks;
using System.Collections.Generic;
using ILogger = Serilog.ILogger;
using NFLWebsite;

namespace NFL.Web.Controllers
{
    /// <summary>
    /// API for the admin page to populate information on the page and update the database
    /// </summary>
    [Route("api/[controller]")]
    public class AdminController : BaseController
    {
        private readonly IAdminService adminService;

        public AdminController(IAdminService _adminService)
        {
            adminService = _adminService;
        }

        /// <summary>
        /// Upload CSV file to add games
        /// </summary>
        /// <returns>ActionResult with resulting information after uploading CSV</returns>
        [HttpPost("upload-games"), DisableRequestSizeLimit]
        public ActionResult ReceiveGamesFile()
        {
            ActionResult? result;

            try
            {
                var file = Request.Form.Files[0];
                var stream = file.OpenReadStream();

                UploadFileProcessor processor = new();

                List<GameCsvRecord> data = processor.LoadGames(stream);

                adminService.AddOrUpdateGamesAsync(data, 2022);

                result = Ok(data);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex);
            }

            return result;
        }

        /// <summary>
        /// Upload CSV file to add teams
        /// </summary>
        /// <returns>ActionResult with resulting information after uploading CSV</returns>
        [HttpPost("upload-teams"), DisableRequestSizeLimit]
        public ActionResult ReceiveTeamsFile()
        {
            ActionResult? result;

            try
            {
                var file = Request.Form.Files[0];
                var stream = file.OpenReadStream();

                UploadFileProcessor processor = new();

                List<TeamCsvRecord> data = processor.LoadTeams(stream);

                adminService.AddOrUpdateTeamsAsync(data, 2022);

                result = Ok(data);
            }
            catch (Exception ex)
            {
                result = BadRequest(ex);
            }

            return result;
        }
    }
}
