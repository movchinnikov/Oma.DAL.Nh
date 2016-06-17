namespace Oma.DAL.Nh.Utils
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;
    using FluentNHibernate;
    using FluentNHibernate.Automapping;
    using Entities;

    public sealed class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.IsSubclassOf(typeof(Entity));
        }

        public override bool IsId(Member member)
        {
            return member.MemberInfo.GetCustomAttribute<KeyAttribute>() != null;
        }
    }
}