using heracles.console;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace heracles.webapp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoneyController : ControllerBase
    {
        private readonly ILogger<MoneyController> _logger;

        public MoneyController(ILogger<MoneyController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{input}")]
        public ActionResult<string> GetFormattedMoney(string input)
        {
            string output;
            try
            {
                output = new MoneyFormatter().Format(input);
            }
            catch (ArgumentException e)
            {
                output = e.Message;
            }

            var logMessage = $"Received input '{input}', Heracles responded with {output}";
            _logger.LogInformation(logMessage);

            return output;
        }
    }
}
