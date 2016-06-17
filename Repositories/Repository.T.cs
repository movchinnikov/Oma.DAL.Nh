namespace Oma.DAL.Nh.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Attributes;
    using Entities;
    using Extensions;
    using Filters;
    using SqlParameters;
    using IFilter = Filters.IFilter;

    public class Repository<T, TKey> : Repository, IRepository<T, TKey> 
        where T : Entity<TKey>
    {
        public virtual string SqlInsert { get { return ""; } }
        public virtual string SqlUpdate { get { return ""; } }
        public virtual string SqlDelete { get { return ""; } }

        #region Get

        [UnitOfWork(IsolationLevel = IsolationLevel.ReadCommitted)]
        public virtual T Get(TKey key)
        {
            return this.Get(new SingleFilter<TKey> {Id = key});
        }

        [UnitOfWork(IsolationLevel = IsolationLevel.ReadCommitted)]
        public virtual T Get<TSingleFilter>(TSingleFilter filter)
            where TSingleFilter : IFilter
        {
            return this.Get(filter, null);
        }

        [UnitOfWork(IsolationLevel = IsolationLevel.ReadCommitted)]
        public virtual T Get<TSingleFilter>(TSingleFilter filter, IResultTransformer transformer)
            where TSingleFilter : IFilter
        {
            try
            {
                var result = this.MainActionGet(filter, transformer);
                this.AfterSuccessGet(result);
                return result;
            }
            catch (Exception e)
            {
                this.AfterFailGet(e);
                throw;
            }
        }

        protected virtual ICriteria SetAliasesGet(ICriteria criteria)
        {
            return criteria;
        }

        protected virtual T MainActionGet<TSingleFilter>(TSingleFilter filter)
            where TSingleFilter : IFilter
        {
            return MainActionGet(filter, null);
        }

        protected virtual T MainActionGet<TSingleFilter>(TSingleFilter filter, IResultTransformer transformer)
             where TSingleFilter : IFilter
        {
            var criteria = this.Session.CreateCriteria<T>()
                .ApplyFilter<T, TKey>(filter, transformer);
            return this.SetAliasesGet(criteria).UniqueResult<T>();
        }

        protected virtual void AfterSuccessGet(T result) { }

        protected virtual void AfterFailGet(Exception e) { }

        protected virtual void AfterGet() { }
        
        #endregion Get

        #region GetList

        [UnitOfWork(IsolationLevel = IsolationLevel.ReadCommitted)]
        public virtual IEnumerable<T> GetList<TListFilter>(TListFilter filter)
            where TListFilter : ListFilter
        {
            return this.GetList(filter, null);
        }

        [UnitOfWork(IsolationLevel = IsolationLevel.ReadCommitted)]
        public virtual IEnumerable<T> GetList<TListFilter>(TListFilter filter, IResultTransformer transformer)
           where TListFilter : ListFilter
        {
            try
            {
                var result = this.MainActionGetList(filter, transformer);
                this.AfterSuccessGetList(result);

                return result;
            }
            catch (Exception e)
            {
                this.AfterFailGetList(e);
                throw;
            }
        }

        [UnitOfWork(IsolationLevel = IsolationLevel.ReadCommitted)]
        public virtual IEnumerable<T> GetListPage<TListFilter>(TListFilter filter, ref ListPage page)
            where TListFilter : ListFilter
        {
            return this.GetListPage(filter, null, ref page);
        }

        [UnitOfWork(IsolationLevel = IsolationLevel.ReadCommitted)]
        public virtual IEnumerable<T> GetListPage<TListFilter>(TListFilter filter, IResultTransformer transformer, ref ListPage page)
           where TListFilter : ListFilter
        {
            try
            {
                var result = this.MainActionGetListPage(filter, transformer, ref page);
                this.AfterSuccessGetList(result);

                return result;
            }
            catch (Exception e)
            {
                this.AfterFailGetList(e);
                throw;
            }
        }

        protected virtual IEnumerable<T> MainActionGetList<TListFilter>(TListFilter filter, IResultTransformer transformer)
            where TListFilter : ListFilter
        {
            var criteria = this.Session.CreateCriteria<T>();
            return this.SetAliasesGetList(criteria)
                    .ApplyFilter<T, TKey>(filter, transformer, transformer != null)
                    .Future<T>().ToList();
        }

        protected virtual IEnumerable<T> MainActionGetListPage<TListFilter>(TListFilter filter, IResultTransformer transformer, ref ListPage page)
           where TListFilter : ListFilter
        {
            var criteria = this.Session.CreateCriteria<T>()
                .ApplyCustomFilter(filter);
           
            var totalCount =
                this.SetAliasesGetList(criteria)
                .SetProjection(MainActionTotalCount())
                .SetResultTransformer(transformer ?? new DistinctRootEntityResultTransformer())
                .FutureValue<int>().Value;
            page = new ListPage(filter.Count, filter.Offset, totalCount);
            return this.MainActionGetList(filter, transformer);
        }

        protected virtual IProjection MainActionTotalCount()
        {
            return Projections.CountDistinct("Id");
        }

        protected virtual void AfterGetList() { }
        protected virtual void AfterFailGetList(Exception e) { }
        protected virtual void AfterSuccessGetList(IEnumerable<T> result) { }

        protected virtual ICriteria SetAliasesGetList(ICriteria criteria)
        {
            return this.SetAliasesGet(criteria);
        }

        #endregion GetList

        #region Create

        [UnitOfWork]
        public virtual TKey Create(IEnumerable<SqlParameterWrapper> args)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.SqlInsert))
                    throw new ArgumentException(ExceptionMessages.SpIsEmpty);
                return this.Session.CreateSQLQuery(this.SqlInsert)
                    .SetParameters(args)
                    .UniqueResult<TKey>();
            }
            catch (Exception e)
            {
                this.AfterFailCreate(e);
                throw;
            }
        }

        [UnitOfWork]
        protected virtual T Create(IEnumerable<SqlParameterWrapper> args, IResultTransformer transformer)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.SqlInsert))
                    throw new ArgumentException(ExceptionMessages.SpIsEmpty);
                var query = this.Session.CreateSQLQuery(this.SqlInsert)
                    .SetParameters(args);
                if (transformer != null)
                    query.SetResultTransformer(transformer);
                return query.UniqueResult<T>();
            }
            catch (Exception e)
            {
                this.AfterFailCreate(e);
                throw;
            }
        }

        /// <summary>
        /// Обработка после удачного создания
        /// </summary>
        protected virtual void AfterSuccessCreate() { }

        /// <summary>
        /// Обработка после неудачного создания
        /// </summary>
        protected virtual void AfterFailCreate(Exception e) { }

        /// <summary>
        /// Обработка после любого исхода создания
        /// </summary>
        protected virtual void AfterCreate() { }

        #endregion

        #region Update

        [UnitOfWork]
        public virtual void Update(IEnumerable<SqlParameterWrapper> args)
        {
            this.Update(this.SqlUpdate, args);
        }

        [UnitOfWork]
        public virtual T Update(IEnumerable<SqlParameterWrapper> args, IResultTransformer transformer)
        {
            return this.Update(this.SqlUpdate, args, transformer);
        }

        [UnitOfWork]
        public virtual TKey UpdateAndReturnKey(IEnumerable<SqlParameterWrapper> args)
        {
            return this.UpdateAndReturnKey(this.SqlUpdate, args);
        }

        [UnitOfWork]
        public virtual TKey UpdateAndReturnKey(string sql, IEnumerable<SqlParameterWrapper> args)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sql))
                    throw new ArgumentException(ExceptionMessages.SpIsEmpty);
                return this.Session.CreateSQLQuery(sql)
                    .SetParameters(args)
                    .UniqueResult<TKey>();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [UnitOfWork]
        public virtual void Update(string sql, IEnumerable<SqlParameterWrapper> args)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sql))
                    throw new ArgumentException(ExceptionMessages.SpIsEmpty);
                this.Session.CreateSQLQuery(sql)
                    .SetParameters(args)
                    .ExecuteUpdate();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [UnitOfWork]
        public virtual T Update(string sql, IEnumerable<SqlParameterWrapper> args, IResultTransformer transformer)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sql))
                    throw new ArgumentException(ExceptionMessages.SpIsEmpty);
                var query = this.Session.CreateSQLQuery(sql)
                    .SetParameters(args);

                if (transformer != null)
                    query.SetResultTransformer(transformer);

                return query.UniqueResult<T>();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion Update

        #region Delete

        [UnitOfWork]
        public virtual void Delete(TKey key)
        {
            this.Delete(SqlParameterWrapperCollection.Empty.Add("oid", key, NHibernateUtil.GuessType(typeof(TKey))));
        }

        [UnitOfWork]
        public virtual void Delete(IEnumerable<SqlParameterWrapper> args)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.SqlDelete))
                    throw new ArgumentException(ExceptionMessages.SpIsEmpty);
                this.Session.CreateSQLQuery(this.SqlDelete)
                    .SetParameters(args)
                    .ExecuteUpdate();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [UnitOfWork]
        public TKey DeleteAndReturnKey(TKey key)
        {
            return this.DeleteAndReturnKey(SqlParameterWrapperCollection.Empty.Add("oid", key, NHibernateUtil.GuessType(typeof(TKey))));
        }

        [UnitOfWork]
        public TKey DeleteAndReturnKey(IEnumerable<SqlParameterWrapper> args)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.SqlDelete))
                    throw new ArgumentException(ExceptionMessages.SpIsEmpty);
                return this.Session.CreateSQLQuery(this.SqlDelete)
                    .SetParameters(args)
                    .UniqueResult<TKey>();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion
    }
}