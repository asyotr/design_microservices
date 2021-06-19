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
    public class FormGatewayController : ControllerBase
    {
        private IConfiguration _configuration;

        public FormGatewayController(IConfiguration configuration)
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
        [Route("api/v1/addForm")]

        public async Task<IActionResult> AddForm([FromBody] object model)
        {
            logger.Information("Создание заявки");

            var url = _configuration.GetSection("design_form").Value;
            var response = await MakeRequest($"{url}addform", model);
            return Ok(response);
        }

        [HttpPost]
        [Route("api/v1/updatestat")]

        public async Task<IActionResult> UpdateStat([FromBody] object model)
        {
            logger.Information("Обновление статуса");

            var url = _configuration.GetSection("design_form").Value;
            var response = await MakeRequest($"{url}updatestat", model);
            return Ok(response);

        }

        [HttpPost]
        [Route("api/v1/getform")]

        public async Task<IActionResult> GetForm([FromBody] object model)
        {
            logger.Information("Вывод заявок пользователя");

            var url = _configuration.GetSection("design_form").Value;
            var response = await MakeRequest($"{url}getform", model);
            return Ok(response);

        }
    }
}
