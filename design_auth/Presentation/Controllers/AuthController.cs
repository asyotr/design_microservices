using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using design_web_app.Domain.Entities;
using design_web_app.Domain.Interfaces;
using design_web_app.Presentation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace design_web_app.Presentation.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [Route("adduser")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AuthModel model)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.Sentry("https://352b21018b4a424cad3e10f0377fae71@o852348.ingest.sentry.io/5818792")
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .CreateLogger();

            try
            {
                await _authService.AddAuth(model.ToEntity());
                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Произошла ошибка при регистрации, обратитесь в службу поддержки!");
            }
        }

        [Route("auth")]
        [HttpPost]
        public async Task<IActionResult> GetAuth([FromBody] AuthModel model)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.Sentry("https://352b21018b4a424cad3e10f0377fae71@o852348.ingest.sentry.io/5818792")
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .CreateLogger();

            try
            {
                await _authService.GetAuth(model.ToEntity());
                return StatusCode(StatusCodes.Status200OK, "Добро пожаловать!");
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Неверный логин или пароль");
            }
        }

        [Route("reset")]
        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] AuthModel model)
        {
            var logger = new LoggerConfiguration()
            .WriteTo.Sentry("https://352b21018b4a424cad3e10f0377fae71@o852348.ingest.sentry.io/5818792")
            .WriteTo.Console()
            .Enrich.FromLogContext()
            .CreateLogger();

            try
            {
                return Ok(await _authService.ResetPassword(model.ToEntity()));
            }
            catch (Exception e)
            {
                logger.Error(e, "Произошла ошибка");
                return StatusCode(StatusCodes.Status500InternalServerError, "Невозможно изменить пароль, обратитесь в службу поддержки");
            }
        }

    }
}
