namespace Oma.DAL.Nh.Extensions
{
    using System;
    using System.Linq.Expressions;
    using NHibernate;
    using NHibernate.SqlCommand;
    using NHibernate.Transform;
    using Entities;
    using Utils;
    using IFilter = Filters.IFilter;

    public static class CriteriaExtensions
    {
        public static ICriteria ApplyFilter<TEntity, TKey>(this ICriteria criteria, IFilter filter)
            where TEntity : Entity<TKey>
        {
            filter.Apply<TEntity, TKey>(criteria);
            return criteria;
        }

        public static ICriteria ApplyFilter<TEntity, TKey>(this ICriteria criteria, IFilter filter, IResultTransformer transformer, bool requiredTransformer = false)
            where TEntity : Entity<TKey>
        {
            filter.Apply<TEntity, TKey>(criteria, transformer, requiredTransformer);
            return criteria;
        }

        public static ICriteria ApplyCustomFilter(this ICriteria criteria, IFilter filter)
        {
            filter.ApplyCustomFilter(criteria);
            return criteria;
        }

        public static ICriteria CreateAlias<TEntity>(this ICriteria criteria,
            Expression<Func<TEntity, object>> expression)
            where TEntity : Entity
        {
            return CreateAlias(criteria, expression, "");
        }

        public static ICriteria CreateAlias<TEntity>(this ICriteria criteria,
            Expression<Func<TEntity, object>> expression, JoinType joinType)
            where TEntity : Entity
        {
            var associationpath = ExpressionHelper.GetAliasAssociationPath(expression);
            var aliasData = ExpressionHelper.GetAlias(expression);
            var alias = aliasData.Alias;
            return criteria
                .CreateAlias(associationpath, alias, joinType);
        }

        public static ICriteria CreateAlias<TEntity>(this ICriteria criteria,
            Expression<Func<TEntity, object>> expression, string newName)
            where TEntity : Entity
        {
            var associationpath = ExpressionHelper.GetAliasAssociationPath(expression);
            var aliasData = ExpressionHelper.GetAlias(expression);
            var alias = string.IsNullOrWhiteSpace(newName) ? aliasData.Alias : newName;
            return criteria
                .CreateAlias(associationpath, alias, aliasData.JoinType);
        }

        public static ICriteria CreateAlias<TEntity>(this ICriteria criteria,
            Expression<Func<TEntity, object>> expression, string newName, JoinType joinType)
            where TEntity : Entity
        {
            var associationpath = ExpressionHelper.GetAliasAssociationPath(expression);
            var aliasData = ExpressionHelper.GetAlias(expression);
            var alias = string.IsNullOrWhiteSpace(newName) ? aliasData.Alias : newName;
            return criteria
                .CreateAlias(associationpath, alias, joinType);
        }

        public static ICriteria CreateCriteria<TEntity>(this ICriteria criteria,
            Expression<Func<TEntity, object>> expression)
            where TEntity : Entity
        {
            return CreateCriteria(criteria, expression, "");
        }

        public static ICriteria CreateCriteria<TEntity>(this ICriteria criteria,
            Expression<Func<TEntity, object>> expression, string newName)
            where TEntity : Entity
        {
            var associationpath = ExpressionHelper.GetAliasAssociationPath(expression);
            var aliasData = ExpressionHelper.GetAlias(expression);
            var alias = string.IsNullOrWhiteSpace(newName) ? aliasData.Alias : newName;
            return criteria
                .CreateCriteria(associationpath, alias, aliasData.JoinType);
        }
    }
}