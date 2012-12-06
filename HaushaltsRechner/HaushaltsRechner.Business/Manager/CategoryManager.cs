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
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Manager
{
    /// <summary>
    /// Provides CRUD methods to interact with <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>
    /// </summary>
    public static class CategoryManager
    {
        /// <summary>
        /// Gets all <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>.
        /// </summary>
        /// <returns>all <see cref="CATEGORY"/>s</returns>
        public static IQueryable<CATEGORY> GetAllCategories()
        {
            var en = new HaushaltsrechnerEntities();
            
            return en.CATEGORY;            
        }

        /// <summary>
        /// Gets the <see cref="HaushaltsRechner.Data.Model.CATEGORY"/> by ID.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns><see cref="HaushaltsRechner.Data.Model.CATEGORY"/> or null, if no <see cref="HaushaltsRechner.Data.Model.CATEGORY"/> with this ID</returns>
        public static CATEGORY GetCategoryByID(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.CATEGORY.FirstOrDefault(c => c.ID == id);
            }
        }
        
        /// <summary>
        /// Adds an <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c>, if addition successfull</returns>
        public static bool AddCategory(string name)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var cOld = en.CATEGORY.FirstOrDefault(cat => cat.NAME == name);

                if (cOld == null)
                {
                    var c = new CATEGORY
                    {
                        ID = Guid.NewGuid(),
                        NAME = name
                    };

                    en.CATEGORY.AddObject(c);
                    en.SaveChanges();

                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Edits an <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="newName">The new name.</param>
        /// <returns><c>true</c>, if edition successfull</returns>
        public static bool EditCategory(Guid id, string newName)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                var cat = en.CATEGORY.FirstOrDefault(a => a.ID == id);
                if (cat == null || newName == null || newName == string.Empty)
                {
                    return false;
                }

                cat.NAME = newName;
                en.SaveChanges();

                return true;
            }
        }
    }
}