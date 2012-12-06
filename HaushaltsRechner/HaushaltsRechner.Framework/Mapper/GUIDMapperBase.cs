using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaushaltsRechner.Framework.Mapper
{
    /// <summary>
    /// Base class for classes with GUID as ID
    /// </summary>
    public class GUIDMapperBase
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>
        /// The ID.
        /// </value>
        public Guid ID { get; set; }
    }
}
