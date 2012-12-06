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

namespace HaushaltsRechner.Business.Reporting
{
    /// <summary>
    /// List of <see cref="CategorySummary"/>
    /// </summary>
    public class CategorySummaryCollection : List<CategorySummary>
    {
        #region Fields

        private decimal _PosSum;
        private decimal _NegSum;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategorySummaryCollection" /> class.
        /// </summary>
        public CategorySummaryCollection()
        {
            _PosSum = 0;
            _NegSum = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategorySummaryCollection" /> class.
        /// </summary>
        /// <param name="list">The list of <see cref="CategorySummary"/>.</param>
        public CategorySummaryCollection(IEnumerable<CategorySummary> list)
            : this()
        {
            base.AddRange(list);
            CalcPercentages();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the positive sum of all <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>s.
        /// </summary>
        /// <value>
        /// The positive sum.
        /// </value>
        public decimal PosSum
        {
            get { return _PosSum; }
        }

        /// <summary>
        /// Gets the negative sum of all <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>s.
        /// </summary>
        /// <value>
        /// The negative sum.
        /// </value>
        public decimal NegSum
        {
            get { return _NegSum; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the <see cref="CategorySummary"/> to the List.
        /// </summary>
        /// <param name="catSum">The <see cref="CategorySummary"/> to add.</param>
        public new void Add(CategorySummary catSum)
        {
            base.Add(catSum);
            CalcPercentages();
        }

        /// <summary>
        /// Removes the <see cref="CategorySummary"/> from the list.
        /// </summary>
        /// <param name="catSum">The <see cref="CategorySummary"/> to remove.</param>
        public new void Remove(CategorySummary catSum)
        {
            base.Remove(catSum);
            CalcPercentages();
        }

        /// <summary>
        /// Clears the List.
        /// </summary>
        public new void Clear()
        {
            base.Clear();
            CalcPercentages();
        }

        /// <summary>
        /// Calculates the sums of the <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>s
        /// and adds the specific percentages to the <see cref="CategorySummary" />
        /// </summary>
        private void CalcPercentages()
        {
            foreach (var c in this) 
            {
                if (c.Amount >= 0)
                {
                    _PosSum += c.Amount;
                }
                else
                {
                    _NegSum += c.Amount;
                }
            }

            foreach (var c in this)
            {
                if (c.Amount >= 0)
                {
                    c.Percentage = (double)_PosSum / (double)c.Amount;
                }
                else
                {
                    c.Percentage = (double)_NegSum / (double)c.Amount;
                }
            }
        }

        #endregion
    }
}