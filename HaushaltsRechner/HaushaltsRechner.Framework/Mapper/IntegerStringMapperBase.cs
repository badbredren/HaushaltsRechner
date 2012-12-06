using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaushaltsRechner.Framework.Mapper
{
    /// <summary>
    /// Base class for classes with <c>int</c> as Number and <c>string</c> as Name
    /// </summary>
    public class IntegerStringMapperBase :IntegerMapperBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
