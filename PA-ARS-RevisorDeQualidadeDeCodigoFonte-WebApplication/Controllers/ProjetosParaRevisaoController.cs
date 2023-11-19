using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PA_ARS_RevisorDeQualidadeDeCodigoFonte_WebApplication.Controllers
{
    [DataContract]
    public class Projeto
    {
        [DataMember]
        public string ProjectPath { get; set; }
        [DataMember]
        public bool ProjectSelected { get; set; }
    }
    [DataContract]
    public class RepositorioComProjetos
    {
        [DataMember]
        public string RepositoryName { get; set; }
        [DataMember]
        public IEnumerable<Projeto> Projects { get; set; }
    }

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
        public IEnumerable<RepositorioComProjetos> Get()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://localhost:44315/api/repositories");
            var resposta = response.Result.Content.ReadAsStringAsync().Result;
            var result = JsonSerializer.Deserialize<IEnumerable<RepositorioComProjetos>>(resposta);

            return result;
        }
    }
}
