using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ds.Tutorial.Model.Repositories;
using ds.Tutorial.Model;
using Microsoft.Data.SqlClient;

namespace TestFunction
{
    public class Function3
    {
        private static readonly List<TaskModel> Items = new List<TaskModel>();

        [FunctionName("CreateTask")]
        public static async Task<IActionResult> CreateTask(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "task")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Creating a new task list item");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var input = JsonConvert.DeserializeObject<CreateTaskModel>(requestBody);

            log.LogInformation("Define Task");
            var task = new TaskModel()
            {
                Description = "abcde"
            };
            log.LogInformation("Add Task");
            Items.Add(task);
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
