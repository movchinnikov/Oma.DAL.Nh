namespace Oma.DAL.Nh.UnitOfWork
{
    using System;
    using System.Data;
    using NHibernate;

    public class NhUnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _factory;
        private ITransaction _transaction;

        public NhUnitOfWork(ISessionFactory factory)
        {
            this._factory = factory;
        }

        public static NhUnitOfWork Current
        {
            get { return _current; }
            set { _current = value; }
        }
        [ThreadStatic]
        private static NhUnitOfWork _current;

        /// <summary>
        /// Получает текущую сессию
        /// </summary>
        public ISession Session { get; private set; }

        public void BeginTransaction()
        {
            this.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel lvl)
        {
            this.Session = this._factory.OpenSession();
            this._transaction = this.Session.BeginTransaction(lvl);
        }

        public void Commit()
        {
            try
            {
                if (this._transaction.IsActive) this._transaction.Commit();
            }
            finally
            {
                Session.Close();
            }
        }

        public void Rollback()
        {
            try
            {
                if (this._transaction.IsActive) this._transaction.Rollback();
            }
            finally
            {
                Session.Close();
            }
        }
    }
}