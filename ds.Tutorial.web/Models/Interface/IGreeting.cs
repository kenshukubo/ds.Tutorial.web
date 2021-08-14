using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ds.Tutorial.web.Models.Interface
{
    public interface IGreeting
    {
        string Greet();
    }

    public class GoodMorning : IGreeting
    {
        public string Greet()
        {
            return "Good Morning!!";
        }
    }
    public class GoodEvening : IGreeting
    {
        public string Greet()
        {
            return "Good Evening!!";
        }
    }
}
