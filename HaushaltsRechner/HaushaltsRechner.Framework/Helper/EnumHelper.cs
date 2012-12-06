using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaushaltsRechner.Framework.Helper
{
    /// <summary>
    /// Provides helper methods for Enums
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Gets the values from T as Enum.
        /// </summary>
        /// <typeparam name="T">Enum-Type</typeparam>
        /// <returns>List of enum values</returns>
        /// <exception cref="System.InvalidCastException">T is not an Enum.</exception>
        public static IEnumerable<T> GetValues<T>() 
        {
            if (typeof(T).IsEnum)
            {
                return Enum.GetValues(typeof(T)).Cast<T>();
            }
            else
            {
                throw new InvalidCastException("T is not an Enum.");
            }
        }
    }
}
