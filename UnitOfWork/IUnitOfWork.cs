namespace Oma.DAL.Nh.UnitOfWork
{
    using System.Data;

    public interface IUnitOfWork
    {
        /// <summary>
        /// Открывает подключение к БД и транзакцию
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Открывает подключение к БД и транзакцию c указанным уровнеми изоляции
        /// <param name="lvl">Уровень изолированости транзакции</param>
        /// </summary>
        void BeginTransaction(IsolationLevel lvl);

        /// <summary>
        /// Коммитит транзакциюи закрывает подключение к БД
        /// </summary>
        void Commit();

        /// <summary>
        /// Откатывает транзакцию и закрывает подключение к БД
        /// </summary>
        void Rollback(); 
    }
}