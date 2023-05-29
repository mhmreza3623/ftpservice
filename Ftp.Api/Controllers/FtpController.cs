using Ftp.Domain.Models;
using Ftp.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Ftp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FtpController : ControllerBase
    {
        private readonly ILogger<FtpController> _logger;
        private readonly ISftpService _sftpService;
        private readonly SftpServiceConfig _sftpServiceConfig;

        public FtpController(ILogger<FtpController> logger, ISftpService sftpService, IOptions<SftpServiceConfig> options)
        {
            _logger = logger;
            _sftpService = sftpService;
            _sftpServiceConfig = options.Value;
        }

        [HttpPost("send")]
        public async Task<ActionResult> SendAsync(IFormFile file)
        {
            try
            {
                if (Request.Form.Files.Any())
                {

                    //var file = Request.Form.Files[0];
                    var folderName = Path.Combine("Resources", "RecievedFiles");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    if (!Directory.Exists(pathToSave))
                    {
                        Directory.CreateDirectory(pathToSave);
                    }

                    using (var stream = new FileStream(fullPath, FileMode.Append))
                    {
                        file.CopyTo(stream);
                        _sftpService.UploadFile(fullPath, $@"{_sftpServiceConfig.RemoteDirectory}/{fileName}");
                    }
                }
                return Ok();

            }
            catch (System.Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message);

                return StatusCode(500);
            }
        }
    }
}
