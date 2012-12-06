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
using HaushaltsRechner.Business.Mapper;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Reporting
{
    /// <summary>
    /// Maps all <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>s for a <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>
    /// </summary>
    public class CategorySummary
    {
        #region Fields

        private Guid _CategoryID;
        private string _CategoryName;
        private decimal _Amount;
        private List<GridMovement> _MovementsAmounts;
        private double _Percentage;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CategorySummary" /> class.
        /// </summary>
        /// <param name="cat">The <see cref="HaushaltsRechner.Data.Model.CATEGORY"/></param>
        public CategorySummary(CATEGORY cat)
            : this(cat.ID, cat.NAME, null)
        { }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="CategorySummary" /> class.
        /// </summary>
        /// <param name="cat">The <see cref="HaushaltsRechner.Data.Model.CATEGORY"/></param>
        /// <param name="movementAmounts">List of all <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>s.</param>
        public CategorySummary(CATEGORY cat, List<GridMovement> movementAmounts)
            : this(cat.ID, cat.NAME, movementAmounts)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategorySummary" /> class.
        /// </summary>
        /// <param name="categoryID">The <see cref="HaushaltsRechner.Data.Model.CATEGORY"/> ID.</param>
        /// <param name="categoryName">Name of the <see cref="HaushaltsRechner.Data.Model.CATEGORY"/>.</param>
        /// <param name="movementAmounts">List of all <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>s.</param>
        public CategorySummary(Guid categoryID, string categoryName, List<GridMovement> movementAmounts)            
        {
            _CategoryID = categoryID;
            _CategoryName = categoryName;
            _MovementsAmounts = movementAmounts;

            CalcAmounts();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the percentage.
        /// </summary>
        /// <value>
        /// The percentage in comparisson to all <see cref="HaushaltsRechner.Data.Model.CATEGORY" />.
        /// </value>
        public double Percentage
        {
            get { return _Percentage; }
            set { _Percentage = value; }
        }
        
        /// <summary>
        /// Gets the movements amounts.
        /// </summary>
        /// <value>
        /// The movements amounts.
        /// </value>
        public List<GridMovement> MovementsAmounts
        {
            get { return _MovementsAmounts; }
        }

        /// <summary>
        /// Gets the amount of all <seealso cref="MovementsAmounts"/>.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount
        {
            get { return _Amount; }
        }

        /// <summary>
        /// Gets the name of the category.
        /// </summary>
        /// <value>
        /// The name of the category.
        /// </value>
        public string CategoryName
        {
            get { return _CategoryName; }
        }

        /// <summary>
        /// Gets the category ID.
        /// </summary>
        /// <value>
        /// The category ID.
        /// </value>
        public Guid CategoryID
        {
            get { return _CategoryID; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a <see cref="HaushaltsRechner.Business.Mapper.GridMovement"/>
        /// </summary>
        /// <param name="movement"><see cref="HaushaltsRechner.Business.Mapper.GridMovement"/> to add</param>
        public void AddMovement(GridMovement movement)
        {
            _MovementsAmounts.Add(movement);
            CalcAmounts();
        }

        /// <summary>
        /// Adds a Range of <see cref="HaushaltsRechner.Business.Mapper.GridMovement"/>s
        /// </summary>
        /// <param name="movements"><see cref="HaushaltsRechner.Business.Mapper.GridMovement"/>s to add</param>
        public void AddRangeMovements(IEnumerable<GridMovement> movements)
        {
            _MovementsAmounts.AddRange(movements);
            CalcAmounts();
        }

        /// <summary>
        /// Removes a <see cref="HaushaltsRechner.Business.Mapper.GridMovement"/>.
        /// </summary>
        /// <param name="movement">The <see cref="HaushaltsRechner.Business.Mapper.GridMovement"/> to remove.</param>
        public void RemoveMovement(GridMovement movement)
        {
            _MovementsAmounts.Remove(movement);
            CalcAmounts();
        }

        /// <summary>
        /// Removes a <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/> by the <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/>s ID.
        /// </summary>
        /// <param name="movementID">The <see cref="HaushaltsRechner.Data.Model.MOVEMENT"/> ID.</param>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        /// No GridMovement found with the given 'movementID'
        /// </exception>
        public void RemoveMovement(Guid movementID)
        {
            foreach (var m in _MovementsAmounts)
            {
                if (m.ID == movementID)
                {
                    RemoveMovement(m);
                    return;
                }
            }

            throw new KeyNotFoundException("No GridMovement found with the given 'movementID'");
        }

        /// <summary>
        /// Clears the movements.
        /// </summary>
        public void ClearMovements()
        {
            _MovementsAmounts.Clear();
            CalcAmounts();
        }

        private void CalcAmounts()
        {
            _Amount = 0;
            foreach (var m in MovementsAmounts)
            {
                _Amount += m.Amount;
            }
        }

        #endregion
    }
}