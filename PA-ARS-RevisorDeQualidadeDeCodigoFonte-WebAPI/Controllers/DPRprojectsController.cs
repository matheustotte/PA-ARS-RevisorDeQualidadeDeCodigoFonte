using Octokit;
using System.Collections.Generic;
using System.Web.Http;

namespace PA_ARS_RevisorDeQualidadeDeCodigoFonte_WebAPI.Controllers
{
    public struct DPRproject
    {
        public string ProjectPath;
        public string ProjectGitUrl;
    }

    public class DPRprojectsController : ApiController
    {
        // GET api/DPRprojects
        public IEnumerable<DPRproject> Get(string repositoryName)
        {
            var client = new GitHubClient(new ProductHeaderValue("PA-ARS-RevisorDeQualidadeDeCodigoFonte-WebAPI"));
            var credentials = new Credentials("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");
            client.Credentials = credentials;

            var search = new SearchCodeRequest();
            search.Repos.Add(repositoryName);
            (search.Extensions as List<string>).Add("DPR");
            var projects = client.Search.SearchCode(search);

            var result = new List<DPRproject>();
            foreach (var proj in projects.Result.Items)
                result.Add(
                    new DPRproject() {
                        ProjectPath = proj.Path,
                        ProjectGitUrl = proj.GitUrl
                    });

            return result;
        }
    }
}