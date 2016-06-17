namespace Oma.DAL.Nh.Repositories
{
    using System.Collections.Generic;
    using NHibernate.Transform;
    using Entities;
    using Filters;
    using SqlParameters;

    /// <summary>
    /// Репозиторий
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    public interface IRepository<T, TKey> : IRepository
        where T: Entity<TKey>
    {
        /// <summary>
        /// Скрипт для создания записи
        /// </summary>
        string SqlInsert { get; }

        /// <summary>
        /// Скрипт для обновления записи
        /// </summary>
        string SqlUpdate { get; }

        /// <summary>
        /// Скрипт для удаления записи
        /// </summary>
        string SqlDelete{ get; }

        /// <summary>
        /// Получение записи по ключу
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns>запись с типом T</returns>
        T Get(TKey key);

        /// <summary>
        /// Получение записи по фильтру
        /// </summary>
        /// <param name="filter">фильтр</param>
        /// <returns>запись с типом T</returns>
        T Get<TSingleFilter>(TSingleFilter filter)
            where TSingleFilter : IFilter;

        /// <summary>
        /// Получение записи по фильтру с заданным преобразователем
        /// </summary>
        /// <param name="filter">фильтр</param>
        /// <param name="transformer">преобразователь данных</param>
        /// <returns>запись с типом T</returns>
        T Get<TSingleFilter>(TSingleFilter filter, IResultTransformer transformer)
            where TSingleFilter : IFilter;

        /// <summary>
        /// Получение коллекции записей по фильтру
        /// </summary>
        /// <param name="filter">фильтр</param>
        /// <returns>коллекция записей с указаным типом</returns>
        IEnumerable<T> GetList<TListFilter>(TListFilter filter)
            where TListFilter : ListFilter;

        /// <summary>
        /// Получение коллекции записей по фильтру с заданым преобразователем
        /// </summary>
        /// <param name="filter">фильтр</param>
        /// <param name="transformer">преобразователь данных</param>
        /// <returns>коллекция записей с указаным типом</returns>
        IEnumerable<T> GetList<TListFilter>(TListFilter filter, IResultTransformer transformer)
            where TListFilter : ListFilter;

        /// <summary>
        /// Получение коллекции записей по фильтру
        /// </summary>
        /// <param name="filter">фильтр</param>
        /// <param name="page"></param>
        /// <returns>коллекция записей с указаным типом</returns>
        IEnumerable<T> GetListPage<TListFilter>(TListFilter filter, ref ListPage page)
            where TListFilter : ListFilter;

        /// <summary>
        /// Получение коллекции записей по фильтру с заданым преобразователем
        /// </summary>
        /// <param name="filter">фильтр</param>
        /// <param name="transformer">преобразователь данных</param>
        /// <param name="page"></param>
        /// <returns>коллекция записей с указаным типом</returns>
        IEnumerable<T> GetListPage<TListFilter>(TListFilter filter, IResultTransformer transformer, ref ListPage page)
            where TListFilter : ListFilter;

        #region Create

        /// <summary>
        /// Создание записи
        /// </summary>
        /// <param name="args">параметры для сохранения</param>
        /// <returns>идентификатор сохраненной записи</returns>
        TKey Create(IEnumerable<SqlParameterWrapper> args);

        #endregion

        #region Update

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="args">параметры для обновления</param>
        void Update(IEnumerable<SqlParameterWrapper> args);

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="sql">запрос</param>
        /// <param name="args">параметры для обновления</param>
        void Update(string sql, IEnumerable<SqlParameterWrapper> args);

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="args">параметры для обновления</param>
        /// <param name="transformer">преобразователь данных</param>
        T Update(IEnumerable<SqlParameterWrapper> args, IResultTransformer transformer);

        /// <summary>
        /// Обновление записи
        /// </summary>
        /// <param name="sql">запрос</param>
        /// <param name="args">параметры для обновления</param>
        /// <param name="transformer">преобразователь данных</param>
        T Update(string sql, IEnumerable<SqlParameterWrapper> args, IResultTransformer transformer);

        TKey UpdateAndReturnKey(IEnumerable<SqlParameterWrapper> args);

        TKey UpdateAndReturnKey(string sql, IEnumerable<SqlParameterWrapper> args);

        #endregion

        #region Delete
        void Delete(TKey key);

        /// <summary>
        /// Удаление записей
        /// </summary>
        /// <param name="args">параметры для удаления</param>
        void Delete(IEnumerable<SqlParameterWrapper> args);

        /// <summary>
        /// Удаление записей
        /// </summary>
        /// <param name="key">ключ удаляемой записи</param>
        /// <returns></returns>
        TKey DeleteAndReturnKey(TKey key);

        /// <summary>
        /// Удаление записей
        /// </summary>
        /// <param name="args">параметры для удаления</param>
        /// <returns>ключ удаленной записи</returns>
        TKey DeleteAndReturnKey(IEnumerable<SqlParameterWrapper> args);

        #endregion
    }
}