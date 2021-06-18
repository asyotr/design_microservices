using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sentry;
using Serilog;

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

        [HttpPost]
        [Route("api/v1/addForm")]

        public async Task<IActionResult> AddForm([FromBody] object model)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://352b21018b4a424cad3e10f0377fae71@o852348.ingest.sentry.io/5818792")
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {
                logger.Error("Создание заявки");
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("sentry-header", "123");
                    var url = _configuration.GetSection("design_form").Value;
                    var resultMessage = await client.PostAsJsonAsync($"{url}addform", model);
                    resultMessage.EnsureSuccessStatusCode();
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        [Route("api/v1/updatestat")]

        public async Task<IActionResult> UpdateStat([FromBody] object model)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://352b21018b4a424cad3e10f0377fae71@o852348.ingest.sentry.io/5818792")
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {

                logger.Error("Обновление статуса");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("sentry-header", "123");
                    var url = _configuration.GetSection("design_form").Value;
                    var resultMessage = await client.PostAsJsonAsync($"{url}updatestat", model);
                    resultMessage.EnsureSuccessStatusCode();
                    var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(result);
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPost]
        [Route("api/v1/getform")]

        public async Task<IActionResult> GetForm([FromBody] object model)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Sentry("https://352b21018b4a424cad3e10f0377fae71@o852348.ingest.sentry.io/5818792")
                .Enrich.FromLogContext()
                .CreateLogger();

            try
            {

                logger.Error("Вывод заявок пользователя");

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("sentry-header", "123");
                    var url = _configuration.GetSection("design_form").Value;
                    var content = JsonConvert.SerializeObject(model);//Serealiza la lista
                   
                    HttpClient hc = new HttpClient();
                    hc.DefaultRequestHeaders.Add("content-type", "application/json");
                    var response = await hc.PostAsync($"{url}getform", new StringContent(content));

                    // var resultMessage = await client.PostAsJsonAsync($"{url}getform", model);
                    // resultMessage.EnsureSuccessStatusCode();
                    // var result = await resultMessage.Content.ReadAsStringAsync();
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                logger.Fatal(e, "Произошла фатальная ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
