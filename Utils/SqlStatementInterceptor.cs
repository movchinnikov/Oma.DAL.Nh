namespace Oma.DAL.Nh.Utils
{
    using System.Diagnostics;
    using NHibernate;
    using NHibernate.SqlCommand;

    public class SqlStatementInterceptor : EmptyInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Trace.WriteLine(sql.ToString());
            return sql;
        }
    }
}