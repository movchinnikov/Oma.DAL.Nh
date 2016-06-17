namespace Oma.DAL.Nh.Filters
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Entities;
    using Utils;

    public class ListFilter : Filters.IFilter
    {
        public ListFilter()
        {
            this.Fields = new string[0];
            this.Count = 10;
        }

        public string Q { get; set; }
        public SortParam[] SortParams { get; set; }
        public string[] Fields { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }

        public virtual ICriteria Apply<TEntity, TKey>(ICriteria criteria)
            where TEntity : Entity<TKey>
        {
            return this.Apply<TEntity, TKey>(criteria, Transformers.AliasToBean(typeof(TEntity)), false);
        }

        public virtual ICriteria Apply<TEntity, TKey>(ICriteria criteria, IResultTransformer transformer, bool requiredTransform)
            where TEntity : Entity<TKey>
        {
            if (this.SortParams == null || this.SortParams.Length == 0)
                this.SortParams = new[] { SortParam.Create<TEntity>(x => x.Id, ListSortDirection.Ascending) };

            if (this.Count > 0)
                criteria.SetFirstResult(this.Offset).SetMaxResults(this.Count);

            var projectionList = Projections.ProjectionList();

            foreach (var field in this.Fields)
            {
                projectionList.Add(Projections.Property(field), field);
            }

            this.AddComputedProjections(projectionList);

            if (projectionList.Length != 0)
                criteria.SetProjection(projectionList);

            this.SetTransformer(criteria, requiredTransform, projectionList, transformer);

            foreach (var item in this.SortParams)
                SetOrder(criteria, item);

            return this.ApplyCustomFilter(criteria);
        }

        protected virtual void SetTransformer(ICriteria criteria,bool requiredTransform, 
            ProjectionList projectionList, IResultTransformer transformer)
        {
            if (requiredTransform || projectionList.Length != 0)
                criteria.SetResultTransformer(transformer);
        }

        public virtual ICriteria ApplyCustomFilter(ICriteria criteria)
        {
            return criteria;
        }

        protected void SetOrder(ICriteria criteria, SortParam param)
        {
            if (string.IsNullOrWhiteSpace(param.Field))
                return;

            criteria
                .AddOrder(param.Direction == ListSortDirection.Ascending
                    ? Order.Asc(param.Field)
                    : Order.Desc(param.Field));
        }

        protected void SetOrder(DetachedCriteria criteria, SortParam param)
        {
            if (string.IsNullOrWhiteSpace(param.Field))
                return;

            criteria
                .AddOrder(param.Direction == ListSortDirection.Ascending
                    ? Order.Asc(param.Field)
                    : Order.Desc(param.Field));
        }

        protected virtual void AddComputedProjections(ProjectionList projectionList) { }
    }

    public struct SortParam
    {
        public string Field { get; set; }
        public ListSortDirection Direction { get; set; }

        public static SortParam Create<TEntity>(Expression<Func<TEntity, object>> expression, ListSortDirection direction)
        {
            return new SortParam { Direction = direction, Field = ExpressionHelper.GetAliasAssociationPath(expression) };
        }
    }
}