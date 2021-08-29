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
    // DI�ݒ��t�^������Function�ɂ��̑�����t�^����I
    // �����Ŏw�肷��^��ύX���邱�ƂŁA������DIConfig���g�������邱�Ƃ��o����I
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

            // => service�̎����^�́uPlayerVoiceService�v�ł��I
            log.LogInformation($"service�̎����^�́u{service.GetType().Name}�v�ł��I");

            var playerVoice = service.GetPlayerVoice();

            // => PlayerVoice=�X�v���g�D�[���y��������������������
            log.LogInformation($"PlayerVoice={playerVoice}");

            return new OkObjectResult(playerVoice);
        }

        public class DIConfig
        {
            public DIConfig(string functionName)
            {
                DependencyInjection.Initialize(builder =>
                {
                    // �C���^�[�t�F�C�X�ƁA����ɑΉ���������N���X���w��I
                    // �����Ȑݒ���@������̂ŁA�{��Autofac���Q�l�ɂ��ĂˁI
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
            public string Get() => "�X�v���g�D�[���y��������������������";
        }

        public interface IPlayerVoiceService
        {
            string GetPlayerVoice();
        }

        public class PlayerVoiceService : IPlayerVoiceService
        {
            private readonly IPlayeVoiceRepository repository;

            // PlayerVoiceService��InMemoryRepository�̗�����o�^���Ă���̂ŁA
            // �R���X�g���N�^������InMemoryRepository�̃C���X�^���X���C���W�F�N�g���Ă�����I
            // Function�̈�������Ȃ��̂�InjectAttribute�͕s�v����I
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
