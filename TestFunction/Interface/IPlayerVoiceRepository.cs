using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFunction.Interface
{
    public interface IPlayerVoiceRepository
    {
        string Get();
    }

    public class InMemoryPlayerVoiceRepository : IPlayerVoiceRepository
    {
        public string Get() => "ABCDEFGH";
    }
}
