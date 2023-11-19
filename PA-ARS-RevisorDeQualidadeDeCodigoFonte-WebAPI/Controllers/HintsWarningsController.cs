using Octokit;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace PA_ARS_RevisorDeQualidadeDeCodigoFonte_WebAPI.Controllers
{
    public class DebitoTecnico
    {
        public string Description;
        public string Suggestion;
    }

    public class ProjetoCompilado
    {
        public string Project;
        public IEnumerable<DebitoTecnico> TecnicDebits;
    }

    public class HintsWarningsController : ApiController
    {
        // GET api/hintswarnings
        public IEnumerable<ProjetoCompilado> Get()
        {
            var client = new GitHubClient(new ProductHeaderValue("PA-ARS-RevisorDeQualidadeDeCodigoFonte-WebAPI"));
            var credentials = new Credentials("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");
            client.Credentials = credentials;

            var repositories = client.Repository.GetAllForCurrent();
            var result = new List<ProjetoCompilado>();
            foreach (var repo in repositories.Result)
            {
                var search = new SearchCodeRequest();
                search.Repos.Add(repo.Owner.Login + "/" + repo.Name);
                (search.Extensions as List<string>).Add("compile");
                var compiles = client.Search.SearchCode(search);

                foreach (var compile in compiles.Result.Items)
                {
                    var proj = new ProjetoCompilado();
                    proj.Project = repo.Owner.Login + "/" + repo.Name + " / " + compile.Path;
                    proj.TecnicDebits = new List<DebitoTecnico>();

                    var rawContent = client.Repository.Content.GetRawContent(repo.Owner.Login, repo.Name, compile.Path);
                    var arquivo = System.Text.Encoding.UTF8.GetString(rawContent.Result);

                    foreach (var linha in arquivo.Split('\n'))
                        (proj.TecnicDebits as List<DebitoTecnico>).Add(
                            new DebitoTecnico()
                            {
                                Description = linha,
                                Suggestion = 
                                    linha.Contains("declared but never used") ? 
                                        "Elimine esta variável da seção var." :
                                    linha.Contains("never used") ? 
                                        "Apague a atribuição que é feita a esta variável neste local." :
                                    linha.Contains("not have been initialized") ? 
                                        "Mova ou insira alguma atribuição para esta variável em algum ponto anterior do fluxo de execução que inclui esta atribuição dela."
                                : ""
                            }) ;

                    result.Add(proj);
                }
            }

            return result.ToArray();
        }
        
    }
}