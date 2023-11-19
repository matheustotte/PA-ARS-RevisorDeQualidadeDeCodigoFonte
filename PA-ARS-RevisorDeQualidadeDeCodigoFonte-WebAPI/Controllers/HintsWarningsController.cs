using Octokit;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace PA_ARS_RevisorDeQualidadeDeCodigoFonte_WebAPI.Controllers
{
    public class ProjetoCompilado
    {
        public string Project;
        public IEnumerable<string> Compilation;
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
                var DPRs = client.Search.SearchCode(search);

                foreach (var proj in DPRs.Result.Items)
                {
                    var rawContent = client.Repository.Content.GetRawContent(repo.Owner.Login, repo.Name, proj.Path);
                    var arquivo = System.Text.Encoding.UTF8.GetString(rawContent.Result);

                    result.Add(
                        new ProjetoCompilado()
                        {
                            Project = repo.Owner.Login + "/" + repo.Name + " / " + proj.Path,
                            Compilation = arquivo.Split('\n')
                        });
                }
            }

            return result.ToArray();
        }
        
    }
}