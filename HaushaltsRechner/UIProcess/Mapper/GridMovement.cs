using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaushaltsRechner.UIProcess.Mapper
{
    public class GridMovement
    {
        public Guid ID { get; set; }
        public decimal Amount { get; set; }
        public string ReasonText { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
        public string AccountName { get; set; }
        public string DateAdded { get; set; }
        public string DateEdit { get; set; }
        public string Message { get; set; }
        public Guid CategoryID { get; set; }
        public Guid ReasonID { get; set; }
        public Guid AccountID { get; set; }      
    }
}