using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using design_form.Domain.Entities;
using design_form.Domain.Interfaces;
using design_form.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Core;

namespace design_form.Presentation.Controllers
{
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        public FormController(IFormService formService)
        {
            _formService = formService ?? throw new ArgumentNullException(nameof(formService));
        }

        private Logger logger = new LoggerConfiguration()
            .WriteTo.Sentry("https://352b21018b4a424cad3e10f0377fae71@o852348.ingest.sentry.io/5818792")
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .CreateLogger();

        [Route("addform")]
        [HttpPost]
        public async Task<IActionResult> AddForm([FromBody] FormModel model)
        {
            try
            {
                await _formService.AddForm(model.ToEntity());
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Проверьте правильность вводимых данных. Если ошибка не исчезает - обратитесь в службу поддержки");
            }
        }
        [Route("updatestat")]
        [HttpPost]
        public async Task<IActionResult> UpdateStat([FromBody] FormModel model)
        {
            try
            {
                await _formService.UpdateStat(model.ToEntity());
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Невозможно обновить статус");
            }
        }

        [Route("getform")]
        [HttpPost]
        public async Task<IActionResult> GetForm([FromBody] FormModel model)
        {
            try
            {
                logger.Information("Запрос на получение заявок");
                return Ok((await _formService.GetForm(model.ToEntity()))
                    .Select(form => new FormModel(form)));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка или записи не найдены. Обратитесь в службу поддержки");
            }
        }

    }
}
