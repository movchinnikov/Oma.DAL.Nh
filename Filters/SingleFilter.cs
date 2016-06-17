namespace Oma.DAL.Nh.Filters
{
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Transform;
    using Entities;

    public class SingleFilter<TKey> : Filters.IFilter
    {
        public SingleFilter()
        {
            this.Fields = new string[0];
        }

        public TKey Id { get; set; }
        public string[] Fields { get; set; }


        public virtual ICriteria Apply<TEntity, TKey>(ICriteria criteria)
            where TEntity : Entity<TKey>
        {
            return this.Apply<TEntity, TKey>(criteria, Transformers.AliasToBean(typeof(TEntity)), false);
        }

        public virtual ICriteria Apply<TEntity, TKey>(ICriteria criteria, IResultTransformer transformer, bool requiredTransform)
            where TEntity : Entity<TKey>
        {
            var projectionList = Projections.ProjectionList();

            foreach (var field in this.Fields)
            {
                projectionList.Add(Projections.Property(field), field);
            }

            this.AddComputedProjections(projectionList);

            var key = default(TKey);

            if (!object.Equals(this.Id, key))
                criteria.Add(Restrictions.Eq(Projections.Property<TEntity>(x => x.Id), this.Id));

            if (projectionList.Length != 0)
                criteria.SetProjection(projectionList);

            if (requiredTransform || projectionList.Length != 0)
                criteria.SetResultTransformer(transformer);

            return this.ApplyCustomFilter(criteria);
        }

        public virtual ICriteria ApplyCustomFilter(ICriteria criteria)
        {
            return criteria;
        }

        protected virtual void AddComputedProjections(ProjectionList projectionList) {}
    }
}