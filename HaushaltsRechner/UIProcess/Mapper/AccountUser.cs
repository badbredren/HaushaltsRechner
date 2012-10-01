using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaushaltsRechner.UIProcess.Mapper
{
    public class AccountUser
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public bool InAccount { get; set; }
    }
}