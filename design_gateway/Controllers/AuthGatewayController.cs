using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sentry;
using Serilog;
using Serilog.Core;

namespace Gateway.Controllers
{
    [ApiController]
    public class AuthGatewayController : ControllerBase
    {
        private IConfiguration _configuration;

        public AuthGatewayController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        private HttpClient client = new HttpClient();
        private Logger logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://352b21018b4a424cad3e10f0377fae71@o852348.ingest.sentry.io/5818792")
                .Enrich.FromLogContext()
                .CreateLogger();

        private async Task<string> MakeRequest(string url, object model)
        {
            var content = model.ToString();
            var body = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, body);
            return await response.Content.ReadAsStringAsync();
        }


        [HttpPost]
        [Route("api/v1/adduser")]

        public async Task<IActionResult> AddAuth([FromBody] object model)
        {
            logger.Information("Создание аккаунта");

            var url = _configuration.GetSection("design_web_app").Value;
            var response = await MakeRequest($"{url}adduser", model);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/v1/reset")]

        public async Task<IActionResult> ResetPassword([FromBody] object model)
        {
            logger.Information("Смена пароля");

            var url = _configuration.GetSection("design_web_app").Value;
            var response = await MakeRequest($"{url}reset", model);
            return Ok(response);

        }

        [HttpPost]
        [Route("api/v1/auth")]

        public async Task<IActionResult> Auth([FromBody] object model)
        {
            logger.Information("Авторизация");

            var url = _configuration.GetSection("design_web_app").Value;
            var response = await MakeRequest($"{url}auth", model);
            return Ok(response);

        }
    }
}
