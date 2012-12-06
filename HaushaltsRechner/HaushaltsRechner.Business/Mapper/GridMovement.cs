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
using System.Web;
using HaushaltsRechner.Framework.Mapper;

namespace HaushaltsRechner.Business.Mapper
{
    /// <summary>
    /// Mapper for <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/> data, prerared for sending to a client
    /// </summary>
    public class GridMovement : GUIDMapperBase
    {
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }
        /// <summary>
        /// Gets or sets the reason text.
        /// </summary>
        /// <value>
        /// The reason text.
        /// </value>
        public string ReasonText { get; set; }
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string CategoryName { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        /// <value>
        /// The name of the account.
        /// </value>
        public string AccountName { get; set; }
        /// <summary>
        /// Gets or sets the date added.
        /// </summary>
        /// <value>
        /// The date added.
        /// </value>
        public string DateAdded { get; set; }
        /// <summary>
        /// Gets or sets the date edit.
        /// </summary>
        /// <value>
        /// The date edit.
        /// </value>
        public string DateEdit { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        /// <value>
        /// The category ID.
        /// </value>
        public Guid CategoryID { get; set; }
        /// <summary>
        /// Gets or sets the reason ID.
        /// </summary>
        /// <value>
        /// The reason ID.
        /// </value>
        public Guid ReasonID { get; set; }
        /// <summary>
        /// Gets or sets the account ID.
        /// </summary>
        /// <value>
        /// The account ID.
        /// </value>
        public Guid AccountID { get; set; }      
    }
}