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
