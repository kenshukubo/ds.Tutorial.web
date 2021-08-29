using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AzureFunctions.Autofac.Configuration;
using Autofac;
using AzureFunctions.Autofac;

namespace TestFunction
{
    // DI設定を付与したいFunctionにこの属性を付与する！
    // 引数で指定する型を変更することで、複数のDIConfigを使い分けることが出来る！
    [DependencyInjectionConfig(typeof(DIConfig))]
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log,
            [Inject] IPlayerVoiceService service
            )
        {
            //log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;

            //string responseMessage = string.IsNullOrEmpty(name)
            //    ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
            //    : $"Hello, {name}. This HTTP triggered function executed successfully.";

            //return new OkObjectResult(responseMessage);

            // => serviceの実装型は「PlayerVoiceService」です！
            log.LogInformation($"serviceの実装型は「{service.GetType().Name}」です！");

            var playerVoice = service.GetPlayerVoice();

            // => PlayerVoice=スプラトゥーン楽しいいいいいいいいい
            log.LogInformation($"PlayerVoice={playerVoice}");

            return new OkObjectResult(playerVoice);
        }

        public class DIConfig
        {
            public DIConfig(string functionName)
            {
                DependencyInjection.Initialize(builder =>
                {
                    // インターフェイスと、それに対応する実装クラスを指定！
                    // いろんな設定方法があるので、本家Autofacを参考にしてね！
                    // https://github.com/autofac/Autofac#get-started
                    builder.RegisterType<InMemoryPlayerVoiceRepository>().As<IPlayeVoiceRepository>();
                    builder.RegisterType<PlayerVoiceService>().As<IPlayerVoiceService>();
                },
                functionName);
            }
        }

        public interface IPlayeVoiceRepository
        {
            string Get();
        }

        public class InMemoryPlayerVoiceRepository : IPlayeVoiceRepository
        {
            public string Get() => "スプラトゥーン楽しいいいいいいいいい";
        }

        public interface IPlayerVoiceService
        {
            string GetPlayerVoice();
        }

        public class PlayerVoiceService : IPlayerVoiceService
        {
            private readonly IPlayeVoiceRepository repository;

            // PlayerVoiceServiceとInMemoryRepositoryの両方を登録しているので、
            // コンストラクタ引数へInMemoryRepositoryのインスタンスをインジェクトしてくれるよ！
            // Functionの引数じゃないのでInjectAttributeは不要だよ！
            public PlayerVoiceService(IPlayeVoiceRepository repository)
            {
                this.repository = repository;
            }

            public string GetPlayerVoice()
            {
                return repository.Get();
            }
        }
    }
}
