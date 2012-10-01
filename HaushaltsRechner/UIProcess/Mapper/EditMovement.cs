using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaushaltsRechner.UIProcess.Mapper
{
    public class EditMovement
    {
        public Guid ID { get; set; }
        public Guid ACCOUNT_ID { get; set; }
        public Guid CATEGORY_ID { get; set; } 
        public Guid REASON_ID { get; set; }
        public string MESSAGE { get; set; }
        public decimal AMOUNT { get; set; }
        public string DATE_ADDED { get; set; }
    }
}