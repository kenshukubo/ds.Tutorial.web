using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ds.Tutorial.Model;
using ds.Tutorial.Model.Repositories;

namespace TestFunction
{
    public class Function4
    {
        //private static readonly List<TaskModel> Items = new List<TaskModel>();

        public Function4(TutorialDbContext tutorialDbContext)
        {
            _tutorialDbContext = tutorialDbContext;
        }

        private readonly TutorialDbContext _tutorialDbContext;

        [FunctionName("UpdateTask")]
        public async Task<IActionResult> UpdateTask(
            [HttpTrigger(AuthorizationLevel.Function, "put", "get", Route = "task/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            var task = _tutorialDbContext.TaskList.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return new NotFoundResult();
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updated = JsonConvert.DeserializeObject<UpdateTaskModel>(requestBody);

            task.IsDone = updated.IsDone;
            if (!string.IsNullOrEmpty(updated.Description))
            {
                task.Description = updated.Description;
            }

            await _tutorialDbContext.SaveChangesAsync();
            return new OkObjectResult(task);
        }
    }
}
