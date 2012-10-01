using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HaushaltsRechner.Business.SearchParameter
{
    public class UebersichtSearchParameter : SearchParameterBase
    {
        public decimal ValueFrom { get; set; }
        public decimal ValueTo { get; set; }
        public Guid UserID { get; set; }
        public Guid AccountID { get; set; }
        public Guid ReasonID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime EditFrom { get; set; }
        public DateTime EditTo { get; set; }

        [OnDeserializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            ValueFrom = decimal.MinValue;
            ValueTo = decimal.MinValue;
            UserID = Guid.Empty;
            AccountID = Guid.Empty;
            ReasonID = Guid.Empty;
            DateFrom = DateTime.MinValue;
            DateTo = DateTime.MinValue;
            EditFrom = DateTime.MinValue;
            EditTo = DateTime.MinValue;
        }
    }
}
