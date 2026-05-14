using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTrackerLib
{
    public static class ExpansionClass
    {
        public static IEnumerable<T> ExceptCollection<T>(this IEnumerable<T> collection, IEnumerable<T> exceptionCollection)
        {
            List<T> result = new List<T>();
            foreach (T item in collection)
            {
                if (!exceptionCollection.Contains(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
