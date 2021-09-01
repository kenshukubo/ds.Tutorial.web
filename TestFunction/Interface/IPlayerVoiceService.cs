using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunction.Interface
{
    public interface IPlayerVoiceService
    {
        string GetPlayerVoice();
    }

    public class PlayerVoiceService : IPlayerVoiceService
    {
        public readonly IPlayerVoiceRepository repository;

        public PlayerVoiceService(IPlayerVoiceRepository repository)
        {
            this.repository = repository;
        }

        public string GetPlayerVoice()
        {
            return repository.Get();
        }
    }
}
