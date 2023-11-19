using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PA_ARS_RevisorDeQualidadeDeCodigoFonte_WebAPI.Controllers
{
    public class Projeto
    {
        public string ProjectPath;
        public bool ProjectSelected;
    }
    public class RepositorioComProjetos
    {
        public string RepositoryName;
        public IEnumerable<Projeto> Projects;
    }

    public class RepositoriesController : ApiController
    {
        // GET api/repositories
        public IEnumerable<RepositorioComProjetos> Get()
        {
            var client = new GitHubClient(new ProductHeaderValue("PA-ARS-RevisorDeQualidadeDeCodigoFonte-WebAPI"));
            var credentials = new Credentials("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");
            client.Credentials = credentials;

            var repositories = client.Repository.GetAllForCurrent();
            var result = new List<RepositorioComProjetos>();
            foreach (var repo in repositories.Result)
            {
                var repositorio = new RepositorioComProjetos();
                repositorio.RepositoryName = repo.Owner.Login + "/" + repo.Name;

                var search = new SearchCodeRequest();
                search.Repos.Add(repositorio.RepositoryName);
                (search.Extensions as List<string>).Add("dpr");
                var DPRs = client.Search.SearchCode(search);

                repositorio.Projects = new List<Projeto>();
                foreach (var proj in DPRs.Result.Items)
                {
                    (repositorio.Projects as List<Projeto>).Add(
                        new Projeto()
                        {
                            ProjectPath = proj.Path,
                            ProjectSelected = proj.Path.Contains("/Proje")
                        });
                }

                if ((repositorio.Projects as List<Projeto>).Count > 0)
                    result.Add(repositorio);
            }

            return result.ToArray();
        }
    }
}
