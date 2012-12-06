/*
    This file is part of HaushaltsRechner.

    HaushaltsRechner is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    HaushaltsRechner is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with HaushaltsRechner.  If not, see <http://www.gnu.org/licenses/>.

    Diese Datei ist Teil von HaushaltsRechner.

    HaushaltsRechner ist Freie Software: Sie können es unter den Bedingungen
    der GNU General Public License, wie von der Free Software Foundation,
    Version 3 der Lizenz oder (nach Ihrer Option) jeder späteren
    veröffentlichten Version, weiterverbreiten und/oder modifizieren.

    HaushaltsRechner wird in der Hoffnung, dass es nützlich sein wird, aber
    OHNE JEDE GEWÄHELEISTUNG, bereitgestellt; sogar ohne die implizite
    Gewährleistung der MARKTFÄHIGKEIT oder EIGNUNG FÜR EINEN BESTIMMTEN ZWECK.
    Siehe die GNU General Public License für weitere Details.

    Sie sollten eine Kopie der GNU General Public License zusammen mit diesem
    Programm erhalten haben. Wenn nicht, siehe <http://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HaushaltsRechner.Business.SearchParameter
{
    /// <summary>
    /// Searchparamter for Reporting
    /// </summary>
    public class ReportingSearchParameter : SearchParameterBase
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ReportingSearchParameterType Type { get; set; }
        /// <summary>
        /// Gets or sets the type of the direction.
        /// </summary>
        /// <value>
        /// The type of the direction.
        /// </value>
        public ReportingSearchParameterDirectionType DirectionType { get; set; }
        /// <summary>
        /// Gets or sets the date from.
        /// </summary>
        /// <value>
        /// The date from.
        /// </value>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// Gets or sets the date to.
        /// </summary>
        /// <value>
        /// The date to.
        /// </value>
        public DateTime DateTo { get; set; }
        /// <summary>
        /// Gets or sets the edit from.
        /// </summary>
        /// <value>
        /// The edit from.
        /// </value>
        public DateTime EditFrom { get; set; }
        /// <summary>
        /// Gets or sets the edit to.
        /// </summary>
        /// <value>
        /// The edit to.
        /// </value>
        public DateTime EditTo { get; set; }
        /// <summary>
        /// Gets or sets the amount from.
        /// </summary>
        /// <value>
        /// The amount from.
        /// </value>
        public decimal AmountFrom { get; set; }
        /// <summary>
        /// Gets or sets the amount to.
        /// </summary>
        /// <value>
        /// The amount to.
        /// </value>
        public decimal AmountTo { get; set; }
        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        /// <value>
        /// The accounts.
        /// </value>
        public List<Guid> Accounts { get; set; }
        /// <summary>
        /// Gets or sets the reasons.
        /// </summary>
        /// <value>
        /// The reasons.
        /// </value>
        public List<Guid> Reasons { get; set; }

        [OnDeserializing]
        internal void OnDeserializingMethod(System.Runtime.Serialization.StreamingContext context)
        {
            Type = 0;
            DirectionType = 0;
            DateFrom = DateTime.MinValue;
            DateTo = DateTime.MinValue;
            EditFrom = DateTime.MinValue;
            EditTo = DateTime.MinValue;
            AmountFrom = decimal.MinValue;
            AmountTo = decimal.MinValue;
            Accounts = new List<Guid>();
            Reasons = new List<Guid>();
        }
    }
}
