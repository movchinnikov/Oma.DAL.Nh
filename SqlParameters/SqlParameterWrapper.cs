namespace Oma.DAL.Nh.SqlParameters
{
    using NHibernate.Type;

    public sealed class SqlParameterWrapper
    {
        public string Name { get; set; } 
        public object Value { get; set; } 
        public IType Type { get; set; }

        public SqlParameterWrapper(string name, object value, IType type)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
        }
    }
}