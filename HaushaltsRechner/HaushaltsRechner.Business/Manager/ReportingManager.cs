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
using System.Threading.Tasks;
using HaushaltsRechner.Business.Mapper;
using HaushaltsRechner.Business.Reporting;
using HaushaltsRechner.Business.SearchParameter;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Manager
{
    /// <summary>
    /// Provides methods to create Reports
    /// </summary>
    public static class ReportingManager
    {
        /// <summary>
        /// Creates a <see cref="HaushaltsRechner.Business.Reporting.CategorySummaryCollection"/>
        /// for the given <see cref="HaushaltsRechner.Business.SearchParameter.ReportingSearchParameter"/>.
        /// </summary>
        /// <param name="para">The parameter.</param>
        /// <returns></returns>
        public static CategorySummaryCollection GetCategorySummaray(ReportingSearchParameter para)
        {
            var catSumList = new CategorySummaryCollection();

            using (var en = new HaushaltsrechnerEntities())
            {
                //Account
                var movements = en.MOVEMENT.AsQueryable();

                if (para.Accounts.Count > 0)
                {
                    movements = movements.Where(
                        m => para.Accounts.Contains(m.ACCOUNT_ID));
                }

                //Reason
                if (para.Reasons.Count > 0)
                {
                    movements = movements.Where(
                        m => para.Reasons.Contains(m.REASON_ID));
                }

                //DirectionType
                switch (para.DirectionType)
                {
                    case ReportingSearchParameterDirectionType.Incomming:
                        movements = movements.Where(
                            m => m.AMOUNT >= 0);
                        break;
                    case ReportingSearchParameterDirectionType.Outgoing:
                        movements = movements.Where(
                            m => m.AMOUNT < 0);
                        break;
                }

                //DateAdded
                if (para.DateFrom != DateTime.MinValue)
                {
                    movements = movements.Where(
                        m => m.DATE_ADDED >= para.DateFrom);
                }

                if (para.DateTo != DateTime.MinValue)
                {
                    var date = para.EditTo.AddDays(23.99);
                    movements = movements.Where(
                        m => m.DATE_ADDED <= date);
                }

                //DateEdited
                if (para.EditFrom != DateTime.MinValue)
                {
                    movements = movements.Where(
                        m => m.DATE_EDIT >= para.EditFrom);
                }

                if (para.EditTo != DateTime.MinValue)
                {
                    var date = para.EditTo.AddDays(23.99);
                    movements = movements.Where(
                        m => m.DATE_EDIT <= date);
                }

                //Amount
                if (para.AmountFrom != decimal.MinValue)
                {
                    movements = movements.Where(
                        m => m.AMOUNT >= para.AmountFrom);
                }

                if (para.AmountTo != decimal.MinValue)
                {
                    movements = movements.Where(
                        m => m.AMOUNT <= para.AmountTo);
                }

                var categories = movements.Select(m => m.CATEGORY).Distinct();

                foreach (var cat in categories)
                {
                    var movInCat = movements.Where(
                        m =>
                            m.CATEGORY_ID == cat.ID);

                    var movInCatsGridMovementList = new List<GridMovement>();

                    foreach (var m in movInCat)
                    {
                        movInCatsGridMovementList.Add(new GridMovement
                        {
                            ID = m.ID,
                            Amount = m.AMOUNT,
                            CategoryName = m.CATEGORY.NAME,
                            ReasonText = m.REASON.TEXT,
                            UserName = m.USER.NAME,
                            AccountName = m.ACCOUNT.NAME,
                            DateAdded = m.DATE_ADDED.ToShortDateString(),
                            DateEdit = m.DATE_EDIT.HasValue ? m.DATE_EDIT.Value.ToShortDateString() : string.Empty,
                            Message = m.MESSAGE,
                            CategoryID = m.CATEGORY_ID,
                            ReasonID = m.REASON_ID,
                            AccountID = m.ACCOUNT_ID
                        });
                    }

                    var catSum = new CategorySummary(cat.ID, cat.NAME, movInCatsGridMovementList);
                    catSumList.Add(catSum);
                }
            }
            return catSumList;
        }
    }
}
