namespace Oma.DAL.Nh.Attributes
{
    using System;
    using System.Data;

    /// <summary>
    /// Этот аттрибут используется для указания атомарности помеченного метода.
    /// Метод, помеченный данным аттрибутом, будет перехвачен, а транзакция наченется до выполнения метода.
    /// По окончанию метода транзакция будет принята, если не было исключений, в ином случае произойдет откат.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
        public UnitOfWorkAttribute()
        {
            IsolationLevel = IsolationLevel.ReadCommitted;
        }

        public IsolationLevel IsolationLevel { get; set; }
    }
}