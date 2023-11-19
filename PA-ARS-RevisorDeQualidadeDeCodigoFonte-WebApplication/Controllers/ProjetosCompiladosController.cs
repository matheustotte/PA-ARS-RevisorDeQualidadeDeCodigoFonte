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
    public class DebitoTecnico
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Suggestion { get; set; }
    }

    [DataContract]
    public class ProjetoCompilado
    {
        [DataMember]
        public string Project { get; set; }
        [DataMember]
        public IEnumerable<DebitoTecnico> TecnicDebits { get; set; }
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
            var result = default(IEnumerable<ProjetoCompilado>);
            var resposta = "";

            if (true)
            {
                resposta = @"
                            [
                                {
                                    |Project|: |matheustotteorg/PA-ARS / Projeto02/Project2.compile|,
                                    |TecnicDebits|: [
                                        {
                                            |Description|: |[Hint] Main.pas(34): Value assigned to 'B' never used|,
                                            |Suggestion|: |Apague a atribuição que é feita a esta variável neste local.|
                                        },
                                        {
                                            |Description|: |[Warning] Main.pas(33): Variable 'Frm' might not have been initialized|,
                                            |Suggestion|: |Mova ou insira alguma atribuição para esta variável em algum ponto anterior do fluxo de execução que inclui esta atribuição dela.|
                                        },
                                        {
                                            |Description|: |[Hint] Main.pas(28): Variable 'a' is declared but never used in 'TForm1.Button1Click'|,
                                            |Suggestion|: |Elimine esta variável da seção var.|
                                        },
                                        {
                                            |Description|: ||,
                                            |Suggestion|: ||
                                        }
                                    ]
                                },
                                {
                                    |Project|: |matheustotteorg/PA-ARS / ProjetoTeste_01/ProjetoTeste_01.compile|,
                                    |TecnicDebits|: [
                                        {
                                            |Description|: |[Hint] Unit1.pas(34): Value assigned to 'f' never used|,
                                            |Suggestion|: |Apague a atribuição que é feita a esta variável neste local.|
                                        },
                                        {
                                            |Description|: |[Hint] Unit1.pas(34): Value assigned to 'e' never used|,
                                            |Suggestion|: |Apague a atribuição que é feita a esta variável neste local.|
                                        },
                                        {
                                            |Description|: |[Hint] Unit1.pas(34): Value assigned to 'd' never used|,
                                            |Suggestion|: |Apague a atribuição que é feita a esta variável neste local.|
                                        },
                                        {
                                            |Description|: |[Hint] Unit1.pas(32): Value assigned to 'B' never used|,
                                            |Suggestion|: |Apague a atribuição que é feita a esta variável neste local.|
                                        },
                                        {
                                            |Description|: |[Warning] Unit1.pas(31): Variable 'Frm' might not have been initialized|,
                                            |Suggestion|: |Mova ou insira alguma atribuição para esta variável em algum ponto anterior do fluxo de execução que inclui esta atribuição dela.|
                                        },
                                        {
                                            |Description|: |[Hint] Unit1.pas(27): Variable 'a' is declared but never used in 'TForm1.FormCreate'|,
                                            |Suggestion|: |Elimine esta variável da seção var.|
                                        },
                                        {
                                            |Description|: |[Hint] Unit1.pas(27): Variable 'c' is declared but never used in 'TForm1.FormCreate'|,
                                            |Suggestion|: |Elimine esta variável da seção var.|
                                        },
                                        {
                                            |Description|: |[Hint] Unit1.pas(39): Variable 'g' is declared but never used in 'TForm1.FormClose'|,
                                            |Suggestion|: |Elimine esta variável da seção var.|
                                        },
                                        {
                                            |Description|: |[Hint] Unit1.pas(39): Variable 'h' is declared but never used in 'TForm1.FormClose'|,
                                            |Suggestion|: |Elimine esta variável da seção var.|
                                        },
                                        {
                                            |Description|: ||,
                                            |Suggestion|: ||
                                        }
                                    ]
                                }
                            ]
                        ";
            }
            else
            {
                var response = client.GetAsync("https://localhost:44315/api/hintswarnings");
                resposta = response.Result.Content.ReadAsStringAsync().Result;
            }

            result = JsonSerializer.Deserialize<IEnumerable<ProjetoCompilado>>(resposta.Replace('|', '"'));

            return result;
        }
    }
}
