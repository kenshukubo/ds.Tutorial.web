using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ds.Tutorial.Model
{
    public class CreatePost
    {
        public IFormFile MyImage { set; get; }
    }
}
