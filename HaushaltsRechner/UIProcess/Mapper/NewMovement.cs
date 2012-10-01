using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaushaltsRechner.UIProcess.Mapper
{
    public class NewMovement
    {
        public Guid ID { get; set; }
        public string Amount { get; set; }
        public string Message { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid CategoryID { get; set; }
        public Guid AccountID { get; set; }
        public String Reason { get; set; }

        public NewMovement()
        {
            ID = Guid.NewGuid();
        }
    }
}