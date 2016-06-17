namespace Oma.DAL.Nh.Criteries
{
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.Impl;

    public class ConvertedDetachedCriteria : DetachedCriteria
    {
        public ConvertedDetachedCriteria(ICriteria criteria)
            : base((CriteriaImpl)criteria, criteria)
        {
            var impl = (CriteriaImpl)criteria;
            impl.Session = null;
        }
    }
}