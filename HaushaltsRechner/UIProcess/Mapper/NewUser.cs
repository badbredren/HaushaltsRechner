using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaushaltsRechner.UIProcess.Mapper
{
    public class NewUser 
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool SysAdmin { get; set; }
    }
}