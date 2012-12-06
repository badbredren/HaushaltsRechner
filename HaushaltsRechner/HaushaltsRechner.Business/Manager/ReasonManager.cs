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
using System.Text;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Manager
{
    /// <summary>
    /// Provides CRUD methods to interact with <see cref="HaushaltsRechner.Data.Model.REASON"/>
    /// </summary>
    public static class ReasonManager
    {
        /// <summary>
        /// Gets the <see cref="HaushaltsRechner.Data.Model.REASON"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><see cref="HaushaltsRechner.Data.Model.REASON"/> or <c>null</c></returns>
        public static REASON GetReason(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.REASON.FirstOrDefault(r => r.ID == id);
            }
        }

        /// <summary>
        /// Gets the <see cref="REASON"/> with starting <c>string</c>.
        /// </summary>
        /// <param name="pre">the first characters of the <see cref="HaushaltsRechner.Data.Model.REASON"/>.</param>
        /// <returns>List with <see cref="HaushaltsRechner.Data.Model.REASON"/></returns>
        public static IEnumerable<REASON> GetReasons(string pre)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.REASON.Where(r => r.TEXT.StartsWith(pre)).ToList();
            }
        }

        /// <summary>
        /// Creates the <see cref="HaushaltsRechner.Data.Model.REASON"/>.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        /// The new <see cref="HaushaltsRechner.Data.Model.REASON"/>, 
        /// or if Text already exists,the old <see cref="HaushaltsRechner.Data.Model.REASON"/>
        /// </returns>
        public static REASON CreateReason(string text)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var rOld = en.REASON.Where(r => r.TEXT == text);

                if (!rOld.Any())
                {
                    var r = new REASON
                    {
                        ID = Guid.NewGuid(),
                        TEXT = text
                    };

                    en.REASON.AddObject(r);
                    en.SaveChanges();

                    return r;
                }

                return rOld.First();
            }
        }
    }
}
