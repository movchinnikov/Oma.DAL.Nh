namespace Oma.DAL.Nh.Interceptors
{
    using System;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.ServiceModel.Security;
    using Castle.DynamicProxy;
    using NHibernate;
    using Attributes;
    using Exceptions;
    using UnitOfWork;
    using IInterceptor = Castle.DynamicProxy.IInterceptor;

    public class NhUnitOfWorkInterceptor : IInterceptor
    {
        private readonly ISessionFactory _factory;

        public NhUnitOfWorkInterceptor(ISessionFactory factory)
        {
            this._factory = factory;
        }

        public void Intercept(IInvocation invocation)
        {
            var unitOfWorkAttr = invocation.MethodInvocationTarget.GetCustomAttribute<UnitOfWorkAttribute>();

            if (NhUnitOfWork.Current != null || unitOfWorkAttr == null)
            {
                invocation.Proceed();
                return;
            }

            try
            {
                NhUnitOfWork.Current = new NhUnitOfWork(this._factory);
                NhUnitOfWork.Current.BeginTransaction(unitOfWorkAttr.IsolationLevel);

                try
                {
                    invocation.Proceed();
                    NhUnitOfWork.Current.Session.Flush();
                    NhUnitOfWork.Current.Commit();
                }
                catch (ADOException e)
                {
                    try
                    {
                        NhUnitOfWork.Current.Rollback();
                    }
                    catch
                    {
                        // ignored
                    }
                    var baseEx = e.GetBaseException();
                    if (baseEx is SqlException)
                    {
                        var sqlException = baseEx as SqlException;
                        if (sqlException.Number == 50015)
                            throw new SecurityAccessDeniedException(baseEx.Message);
                        if (sqlException.Number > 50000)
                            throw new WebB2BDataAccessException(baseEx.Message);
                    }
                    throw new DataAccessException(baseEx.Message, e);
                }
                catch (Exception e)
                {
                    try { NhUnitOfWork.Current.Rollback(); }
                    catch
                    {
                        // ignored
                    }
                    throw new DataAccessException(e.Message, e);
                }
            }
            finally
            {
                NhUnitOfWork.Current = null;
            }
        }
    }
}