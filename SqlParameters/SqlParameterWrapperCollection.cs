namespace Oma.DAL.Nh.SqlParameters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using NHibernate;
    using NHibernate.Type;

    public sealed class SqlParameterWrapperCollection : ISqlParameterWrapperCollection
    {
        private readonly IList<SqlParameterWrapper> _list;

        public SqlParameterWrapperCollection()
        {
            this._list = new List<SqlParameterWrapper>();
        }

        public static ISqlParameterWrapperCollection Empty
        {
            get { return new SqlParameterWrapperCollection(); }
        }

        public ISqlParameterWrapperCollection Add(SqlParameterWrapper parameter)
        {
            this._list.Add(parameter);
            return this;
        }

        public ISqlParameterWrapperCollection Add(string name, object value, IType type)
        {
            return this.Add(new SqlParameterWrapper(name, value, type));
        }

        public ISqlParameterWrapperCollection AddString(string name, string value)
        {
            return this.Add(name, string.IsNullOrEmpty(value) ? null : value, NHibernateUtil.String);
        }

        public ISqlParameterWrapperCollection AddDecimal(string name, decimal value)
        {
            return this.Add(name, value, NHibernateUtil.Decimal);
        }

        public ISqlParameterWrapperCollection AddDecimal(string name, decimal? value)
        {
            return this.Add(name, value, NHibernateUtil.Decimal);
        }

        public ISqlParameterWrapperCollection AddInt16(string name, short value)
        {
            return this.Add(name, value, NHibernateUtil.Int16);
        }

        public ISqlParameterWrapperCollection AddInt16(string name, short? value)
        {
            return this.Add(name, value, NHibernateUtil.Int16);
        }

        public ISqlParameterWrapperCollection AddInt32(string name, int value)
        {
            return this.Add(name, value, NHibernateUtil.Int32);
        }

        public ISqlParameterWrapperCollection AddInt32(string name, int? value)
        {
            return this.Add(name, value, NHibernateUtil.Int32);
        }

        public ISqlParameterWrapperCollection AddByte(string name, byte value)
        {
            return this.Add(name, value, NHibernateUtil.Byte);
        }

        public ISqlParameterWrapperCollection AddByte(string name, byte? value)
        {
            return this.Add(name, value, NHibernateUtil.Byte);
        }

        public ISqlParameterWrapperCollection AddDateTime(string name, DateTime? value)
        {
            return this.Add(name, value, NHibernateUtil.DateTime);
        }

        public ISqlParameterWrapperCollection AddDate(string name, DateTime? value)
        {
            return this.Add(name, value, NHibernateUtil.Date);
        }

        public ISqlParameterWrapperCollection AddBool(string name, bool value)
        {
            return this.Add(name, value, NHibernateUtil.Boolean);
        }

        public ISqlParameterWrapperCollection AddBool(string name, bool? value)
        {
            return this.Add(name, value, NHibernateUtil.Boolean);
        }

        public ISqlParameterWrapperCollection AddBinaryBlob(string name, byte[] value)
        {
            return this.Add(name, value, NHibernateUtil.BinaryBlob);
        }

        public ISqlParameterWrapperCollection AddStringClob(string name, string value)
        {
            return this.Add(name, value, NHibernateUtil.StringClob);
        }

        public IEnumerator<SqlParameterWrapper> GetEnumerator()
        {
            return this._list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SqlParameterWrapper this[int index]
        {
            set { this._list[index] = value; }
        }
    }
}