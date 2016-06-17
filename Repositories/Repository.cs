namespace Oma.DAL.Nh.Repositories
{
    using NHibernate;
    using UnitOfWork;

    public class Repository: IRepository
    {
        protected ISession Session { get { return NhUnitOfWork.Current.Session; } }
    }
}