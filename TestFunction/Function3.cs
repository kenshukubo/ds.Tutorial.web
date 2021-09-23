using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TestFunction.Repositories;
using TestFunction.Model;

namespace TestFunction
{
    public class Function3
    {
        //private static readonly List<TaskModel> Items = new List<TaskModel>();

        public Function3(TutorialDbContext tutorialDbContext)
        {
            _tutorialDbContext = tutorialDbContext;
        }

        private readonly TutorialDbContext _tutorialDbContext;

        [FunctionName("CreateTask")]
        public async Task<IActionResult> CreateTask(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "task")] CreateTaskModel req,
            ILogger log)
        {
            log.LogInformation("Creating a new task list item");
            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //var input = JsonConvert.DeserializeObject<CreateTaskModel>(requestBody);

            var task = new TaskModel()
            {
                CreatedOn = DateTime.Now,
                Description = req.Description
            };
            _tutorialDbContext.TaskList.Add(task);
            await _tutorialDbContext.SaveChangesAsync();
            return new OkObjectResult(task);

            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionStrings:tutorial_db", EnvironmentVariableTarget.Process)))
            //    {
            //        connection.Open();
            //        if (String.IsNullOrEmpty("abcde"))
            //        {
            //            var query = $"INSERT INTO [TaskList] (Description,CreatedOn,IsDone) VALUES('{"abcde"}', '{input.CreatedOn}' , '{false}')";
            //            SqlCommand command = new SqlCommand(query, connection);
            //            command.ExecuteNonQuery();
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    log.LogError(e.ToString());
            //    return new BadRequestResult();
            //}
            //return new OkResult();
        }
    }
}
