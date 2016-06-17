namespace Oma.DAL.Nh.Filters
{
    using NHibernate;
    using NHibernate.Transform;
    using Entities;

    public interface IFilter
    {
        string[] Fields { get; set; }

        ICriteria Apply<TEntity, TKey>(ICriteria criteria)
                    where TEntity : Entity<TKey>;

        ICriteria Apply<TEntity, TKey>(ICriteria criteria, IResultTransformer transformer, bool requiredTransform)
            where TEntity : Entity<TKey>;
        ICriteria ApplyCustomFilter(ICriteria criteria);
    }
}