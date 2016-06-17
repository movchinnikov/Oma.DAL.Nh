namespace Oma.DAL.Nh.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using NHibernate;

    public static class EnumerableExtensions
    {
        public static bool HasElements<T>(this IEnumerable<T> enumerable)
        {
            try
            {
                return enumerable.Any();
            }
            catch (LazyInitializationException e)
            {
                return false;
            }
        }
    }
}