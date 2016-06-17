namespace Oma.DAL.Nh.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using Nh;

    public abstract class Entity
    {
        
    }

    public abstract class Entity<T> : Entity
    {
        [Key, Column("Oid")]
        public virtual T Id { get; set; }

        public virtual T GetIdentifier()
        {
            var property = this.GetType().GetProperties()
                .FirstOrDefault(x => x.GetCustomAttribute(typeof(KeyAttribute)) != null);
            if (property == null)
                throw new KeyNotFoundException(ExceptionMessages.KeyNotFoundException);
            if (property.PropertyType != typeof(T))
                throw new SettingsPropertyWrongTypeException(string.Format(ExceptionMessages.InvalidKeyTypeException,
                    property.PropertyType, typeof(T)));
            return (T)property.GetValue(this);
        }
    }

    /// <summary>
    /// Для составных идентификаторов
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CompositeEntity<T> : Entity
    {
        public virtual T GetIdentifier()
        {
            var property = this.GetType().GetProperties()
                .FirstOrDefault(x => x.GetCustomAttribute(typeof(KeyAttribute)) != null);
            if (property == null)
                throw new KeyNotFoundException(ExceptionMessages.KeyNotFoundException);
            if (property.PropertyType != typeof(T))
                throw new SettingsPropertyWrongTypeException(string.Format(ExceptionMessages.InvalidKeyTypeException,
                    property.PropertyType, typeof(T)));
            return (T)property.GetValue(this);
        }
    }
}