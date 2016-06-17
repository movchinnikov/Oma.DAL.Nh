namespace Oma.DAL.Nh.Extensions
{
    using System.Collections.Generic;
    using NHibernate;
    using SqlParameters;

    public static class QueryExtensions
    {
        public static IQuery SetParameters(this IQuery query, IEnumerable<SqlParameterWrapper> args)
        {
            if (args == null) return query;
            foreach (var sqlParameterWrapper in args)
                query.SetParameter(sqlParameterWrapper.Name, sqlParameterWrapper.Value, sqlParameterWrapper.Type);
            return query;
        } 
    }
}