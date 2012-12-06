using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaushaltsRechner.Framework.Mapper
{
    /// <summary>
    /// Base class for classes with GUID as ID an string as NAME
    /// </summary>
    public class GUIDStringMapperBase : GUIDMapperBase
    {
        /// <summary>
        /// Gets or sets the NAME.
        /// </summary>
        /// <value>
        /// The NAME.
        /// </value>
        public string NAME { get; set; }
    }
}
