using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PA_ARS_RevisorDeQualidadeDeCodigoFonte_WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjetosParaRevisaoController : ControllerBase
    {
        private readonly ILogger<ProjetosParaRevisaoController> _logger;

        public ProjetosParaRevisaoController(ILogger<ProjetosParaRevisaoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://localhost:44315/api/repositories");
            var resposta = response.Result.Content.ReadAsStringAsync().Result;
            var result = JsonSerializer.Deserialize<IEnumerable<string>>(resposta);
            return result;
        }
    }
}
