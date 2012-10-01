using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HaushaltsRechner.Business.SearchParameter;
using HaushaltsRechner.Data.Model;

namespace HaushaltsRechner.Business.Manager
{
    public static class MovementManager
    {
        public static IQueryable<MOVEMENT> GetAllMovements()
        {
            var en = new HaushaltsrechnerEntities();

            return en.MOVEMENT;
        }

        public static IQueryable<MOVEMENT> GetMovementsByAccountId(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.MOVEMENT.Where(m => m.ACCOUNT.ID == id);
            }
        }

        public static IQueryable<MOVEMENT> GetMovementsByAccount(ACCOUNT ac)
        {
            return GetMovementsByAccountId(ac.ID);
        }

        public static MOVEMENT GetMovementById(Guid id)
        {
            using (var en = new HaushaltsrechnerEntities())
            {
                return en.MOVEMENT.FirstOrDefault(m => m.ID == id);
            }            
        }

        public static bool DeleteMovement(Guid id)
        {
            if (id == Guid.Empty)
            {
                return false;
            }

            using (var en = new HaushaltsrechnerEntities())
            {
                var m = en.MOVEMENT.FirstOrDefault(mo => mo.ID == id);
                
                if (m == null)
                {
                    return false;
                }

                en.MOVEMENT.DeleteObject(m);
                en.SaveChanges();
                return true;
            }
        }

        public static IQueryable<MOVEMENT> GetMovementsBySearchparameter(UebersichtSearchParameter para, Guid currUserID)
        {
            var en = new HaushaltsrechnerEntities();

            var query = from m in en.MOVEMENT
                            select m;

            var accIds = AccountManager.GetAccountsByUserId(currUserID).Select(a => a.ID);
            query = query.Where(m => accIds.Contains(m.ACCOUNT_ID));
                
            if (para.ValueFrom != decimal.MinValue)
            {
                query = query.Where(m => m.AMOUNT >= para.ValueFrom);
            }

            if (para.ValueTo != decimal.MinValue)
            {
                query = query.Where(m => m.AMOUNT <= para.ValueTo);
            }

            if (para.DateFrom != DateTime.MinValue)
            {
                query = query.Where(m => m.DATE_ADDED >= para.DateFrom);
            }

            if (para.DateTo != DateTime.MinValue)
            {
                query = query.Where(m => m.DATE_ADDED <= para.DateTo);
            }

            if (para.EditFrom != DateTime.MinValue)
            {
                query = query.Where(m => m.DATE_EDIT >= para.EditFrom);
            }

            if (para.EditTo != DateTime.MinValue)
            {
                query = query.Where(m => m.DATE_EDIT <= para.EditTo);
            }

            if (para.UserID != Guid.Empty)
            {
                query = query.Where(m => m.USER_ID == para.UserID);
            }

            if (para.AccountID != Guid.Empty)
            {
                query = query.Where(m => m.ACCOUNT_ID == para.AccountID);
            }

            if (para.ReasonID != Guid.Empty)
            {
                query = query.Where(m => m.REASON_ID == para.ReasonID);
            }
                
            query = query.OrderBy(m => m.DATE_ADDED);

            return query;            
        }
    }
}