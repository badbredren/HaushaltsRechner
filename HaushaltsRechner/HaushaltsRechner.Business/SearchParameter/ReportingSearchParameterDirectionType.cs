using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaushaltsRechner.Business.SearchParameter
{
    /// <summary>
    /// The type of money direction for <see cref="ReportingSearchParameter"/>
    /// </summary>
    public enum ReportingSearchParameterDirectionType
    {
        /// <summary>
        /// Overview
        /// </summary>
        Overview = 0,

        /// <summary>
        /// Outgoing (negativ)
        /// </summary>
        Outgoing = 1,

        /// <summary>
        /// Incomming (positive
        /// </summary>
        Incomming = 2
    }
}
