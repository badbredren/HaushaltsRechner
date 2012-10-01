using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaushaltsRechner.UIProcess.Mapper
{
    public class CategorySummary : IEnumerable<CategorySummary>
    {
        private Guid _CategoryID;
        private string _CategoryName;
        private decimal _Amount;
        private List<decimal> _MovementsAmounts;

        public CategorySummary(Guid categoryID, string categoryName, List<decimal> movementAmounts)
        {
            _CategoryID = categoryID;
            _CategoryName = categoryName;
            _MovementsAmounts = movementAmounts;

            CalcAmounts();
        }

        public List<decimal> MovementsAmounts
        {
            get { return _MovementsAmounts; }
        }

        public decimal Amount
        {
            get { return _Amount; }
        }

        public string CategoryName
        {
            get { return _CategoryName; }
        }

        public Guid CategoryID
        {
            get { return _CategoryID; }
        }

        private void CalcAmounts()
        {
            _Amount = 0;
            foreach (var m in MovementsAmounts)
            {
                _Amount += m;
            }
        }
    }
}