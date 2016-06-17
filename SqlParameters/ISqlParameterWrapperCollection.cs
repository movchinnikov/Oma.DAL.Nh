namespace Oma.DAL.Nh.SqlParameters
{
    using System;
    using System.Collections.Generic;
    using NHibernate.Type;

    public interface ISqlParameterWrapperCollection : IEnumerable<SqlParameterWrapper>
    {
        ISqlParameterWrapperCollection Add(SqlParameterWrapper parameter);

        ISqlParameterWrapperCollection Add(string name, object value, IType type);

        ISqlParameterWrapperCollection AddString(string name, string value);

        ISqlParameterWrapperCollection AddDecimal(string name, decimal value);

        ISqlParameterWrapperCollection AddDecimal(string name, decimal? value);

        ISqlParameterWrapperCollection AddInt16(string name, short value);

        ISqlParameterWrapperCollection AddInt16(string name, short? value);

        ISqlParameterWrapperCollection AddInt32(string name, int value);

        ISqlParameterWrapperCollection AddInt32(string name, int? value);

        ISqlParameterWrapperCollection AddByte(string name, byte value);

        ISqlParameterWrapperCollection AddByte(string name, byte? value);

        ISqlParameterWrapperCollection AddDateTime(string name, DateTime? value);

        ISqlParameterWrapperCollection AddDate(string name, DateTime? value);

        ISqlParameterWrapperCollection AddBool(string name, bool value);

        ISqlParameterWrapperCollection AddBool(string name, bool? value);

        ISqlParameterWrapperCollection AddBinaryBlob(string name, byte[] value);

        ISqlParameterWrapperCollection AddStringClob(string name, string value);

        SqlParameterWrapper this[int index] { set; }
    }
}