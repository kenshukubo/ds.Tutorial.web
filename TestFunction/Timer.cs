using System;
using ds.Tutorial.Model;
using ds.Tutorial.Model.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace TestFunction
{
    public class Timer
    {
        public Timer(TutorialDbContext tutorialDbContext)
        {
            _tutorialDbContext = tutorialDbContext;
        }

        private readonly TutorialDbContext _tutorialDbContext;

        [FunctionName("Timer")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            PerformTasks().GetAwaiter().GetResult();
        }

        private async Task<IActionResult> PerformTasks()
        {
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //var input = JsonConvert.DeserializeObject<CreateTaskModel>(requestBody);

            var task = new TaskModel()
            {
                CreatedOn = DateTime.Now,
                Description = "timer test"
            };
            _tutorialDbContext.TaskList.Add(task);
            await _tutorialDbContext.SaveChangesAsync();
            return new OkObjectResult(task);
        }
    }
}
