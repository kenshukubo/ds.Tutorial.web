using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using TestFunction.Repositories;

namespace TestFunction
{
    public class Function2
    {
        public Function2(TutorialDbContext tutorialDbContext)
        {
            _tutorialDbContext = tutorialDbContext;
        }

        private readonly TutorialDbContext _tutorialDbContext;

        [FunctionName("Function2")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($".NET Core Version: {Environment.Version}");

            var list = await _tutorialDbContext.User.ToListAsync();

            return new OkObjectResult(list);
        }
    }
}
