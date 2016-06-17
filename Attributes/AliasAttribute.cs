namespace Oma.DAL.Nh.Attributes
{
    using System;
    using NHibernate.SqlCommand;

    [AttributeUsage(AttributeTargets.Property)]
    public class AliasAttribute : Attribute
    {
        public AliasAttribute(string alias)
        {
            this.Alias = alias;
        }

        public string Alias { get; set; }

        public JoinType JoinType { get; set; }
    }
}