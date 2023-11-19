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
    public class ProjetoCompilado
    {
        [DataMember]
        public string Project { get; set; }
        [DataMember]
        public IEnumerable<string> Compilation { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class ProjetosCompiladosController : ControllerBase
    {
        private readonly ILogger<ProjetosCompiladosController> _logger;

        public ProjetosCompiladosController(ILogger<ProjetosCompiladosController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ProjetoCompilado> Get()
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync("https://localhost:44315/api/hintswarnings");
            var resposta = response.Result.Content.ReadAsStringAsync().Result;
            var result = default(IEnumerable<ProjetoCompilado>);
            try
            {
                result = JsonSerializer.Deserialize<IEnumerable<ProjetoCompilado>>(resposta);
            }
            catch
            {
                var a = @"[
                            {
                                |Project|: |matheustotteorg/PA-ARS / Projeto02/Project2.compile|,
                                |Compilation|: [
                                    |[Hint] Main.pas(34): Value assigned to 'B' never used|,
                                    |[Warning] Main.pas(33): Variable 'Frm' might not have been initialized|,
                                    |[Hint] Main.pas(28): Variable 'a' is declared but never used in 'TForm1.Button1Click'|,
                                    ||
                                ]
                            },
                            {
                                |Project|: |matheustotteorg/PA-ARS / ProjetoTeste_01/ProjetoTeste_01.compile|,
                                |Compilation|: [
                                    |[Hint] Unit1.pas(34): Value assigned to 'f' never used|,
                                    |[Hint] Unit1.pas(34): Value assigned to 'e' never used|,
                                    |[Hint] Unit1.pas(34): Value assigned to 'd' never used|,
                                    |[Hint] Unit1.pas(32): Value assigned to 'B' never used|,
                                    |[Warning] Unit1.pas(31): Variable 'Frm' might not have been initialized|,
                                    |[Hint] Unit1.pas(27): Variable 'a' is declared but never used in 'TForm1.FormCreate'|,
                                    |[Hint] Unit1.pas(27): Variable 'c' is declared but never used in 'TForm1.FormCreate'|,
                                    |[Hint] Unit1.pas(39): Variable 'g' is declared but never used in 'TForm1.FormClose'|,
                                    |[Hint] Unit1.pas(39): Variable 'h' is declared but never used in 'TForm1.FormClose'|,
                                    ||
                                ]
                            }
                        ]";
                result = JsonSerializer.Deserialize<IEnumerable<ProjetoCompilado>>(a.Replace('|','"'));
            }
            return result;
        }
    }
}
