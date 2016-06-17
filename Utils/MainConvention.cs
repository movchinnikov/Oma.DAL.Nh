namespace Oma.DAL.Nh.Utils
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Reflection;
    using FluentNHibernate.Conventions;
    using FluentNHibernate.Conventions.Instances;
    using NHibernate.Id;
    using Attributes;

    public class MainConvention :
        IIdConvention, IClassConvention, IReferenceConvention, IHasManyConvention, IPropertyConvention,
        IHasOneConvention, IVersionConvention
    {
        public virtual void Apply(IIdentityInstance instance)
        {
            var customAttribute =
                instance.Property.MemberInfo.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
            instance.Column(customAttribute != null ? customAttribute.Name : instance.Property.Name);
            instance.GeneratedBy.Custom<IdentityGenerator>();
        }

        public virtual void Apply(IClassInstance instance)
        {
            var customAttribute = instance.EntityType.GetCustomAttribute(typeof (TableAttribute)) as TableAttribute;
            instance.Table(customAttribute != null ? customAttribute.Name : instance.TableName);
        }

        public virtual void Apply(IManyToOneInstance instance)
        {
            var customAttribute =
                instance.Property.MemberInfo.GetCustomAttribute(typeof (ColumnAttribute)) as ColumnAttribute;
            instance.Column(customAttribute != null ? customAttribute.Name : instance.Property.Name);
            var notInsertAttribute =
                instance.Property.MemberInfo.GetCustomAttribute(typeof (NotInsertAttribute)) as NotInsertAttribute;
            if (notInsertAttribute != null) instance.Not.Insert();
            var notUpdateAttribute =
                instance.Property.MemberInfo.GetCustomAttribute(typeof (NotUpdateAttribute)) as NotUpdateAttribute;
            if (notUpdateAttribute != null) instance.Not.Update();
            instance.LazyLoad();
        }

        public virtual void Apply(IOneToManyCollectionInstance instance)
        {
            var fk = instance.Member.GetCustomAttribute(typeof (ForeignKeyAttribute)) as ForeignKeyAttribute;
            instance.Key.Column(fk != null ? fk.Name : string.Format("{0}{1}", instance.EntityType.Name, "Id"));
            instance.LazyLoad();
        }

        public virtual void Apply(IPropertyInstance instance)
        {
            var customAttribute = instance.Property.MemberInfo.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
            instance.Column(customAttribute != null ? customAttribute.Name : instance.Property.Name);
            var notInsertAttribute = instance.Property.MemberInfo.GetCustomAttribute(typeof(NotInsertAttribute)) as NotInsertAttribute;
            if (notInsertAttribute != null) instance.Not.Insert();
            var notUpdateAttribute = instance.Property.MemberInfo.GetCustomAttribute(typeof(NotUpdateAttribute)) as NotUpdateAttribute;
            if (notUpdateAttribute != null) instance.Not.Update();
            var nvarcharMaxAttribute = instance.Property.MemberInfo.GetCustomAttribute(typeof(NvarcharMaxAttribute)) as NvarcharMaxAttribute;
            if (nvarcharMaxAttribute != null)
            {
                instance.Length(4001);
                instance.CustomSqlType("nvarchar(max)");
            }
            var varbinaryMaxAttribute = instance.Property.MemberInfo.GetCustomAttribute(typeof(VarbinaryMaxAttribute)) as VarbinaryMaxAttribute;
            if (varbinaryMaxAttribute != null)
            {
                instance.Length(2147483647);
                instance.CustomSqlType("varbinary(max)");
            }
        }

        public virtual void Apply(IOneToOneInstance instance)
        {

        }

        public virtual void Apply(IVersionInstance instance)
        {
            var versionPropName = "Version";

            var property = instance.EntityType.GetProperties()
                .FirstOrDefault(x => x.Name == versionPropName);
            if (property == null) return;

            var columnAttr = property.GetCustomAttribute<ColumnAttribute>();
            if (columnAttr != null)
                versionPropName = columnAttr.Name;

            instance.Column(versionPropName);
            instance.CustomType(property.PropertyType);
            instance.UnsavedValue("0");
            instance.Not.Nullable();
        }
    }
}