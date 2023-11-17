using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PA_ARS_RevisorDeQualidadeDeCodigoFonte_WebAPI.Controllers
{
    public class RepositoriesController : ApiController
    {
        // GET api/repositories
        public IEnumerable<string> Get()
        {
            var client = new GitHubClient(new ProductHeaderValue("PA-ARS-RevisorDeQualidadeDeCodigoFonte-WebAPI"));
            var credentials = new Credentials("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");
            client.Credentials = credentials;

            var repositories = client.Repository.GetAllForCurrent();
            var result = new List<string>();
            foreach (var repo in repositories.Result)
                result.Add(repo.Owner.Login + "/" + repo.Name);

            return result.ToArray();
        }
    }
}
