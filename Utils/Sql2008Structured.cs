namespace Oma.DAL.Nh.Utils
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Xml;
    using NHibernate;
    using NHibernate.Engine;
    using NHibernate.SqlTypes;
    using NHibernate.Type;

    public class Sql2008Structured : IType
    {
        private static readonly SqlType[] x = new[] { new SqlType(DbType.Object) };
        public SqlType[] SqlTypes(IMapping mapping)
        {
            return x;
        }

        public bool IsXMLElement { get; private set; }

        public bool IsCollectionType
        {
            get { return true; }
        }

        public bool IsComponentType { get; private set; }
        public bool IsEntityType { get; private set; }
        public bool IsAnyType { get; private set; }

        public int GetColumnSpan(IMapping mapping)
        {
            return 1;
        }

        public bool IsDirty(object old, object current, ISessionImplementor session)
        {
            throw new NotImplementedException();
        }

        public bool IsDirty(object old, object current, bool[] checkable, ISessionImplementor session)
        {
            throw new NotImplementedException();
        }

        public bool IsModified(object oldHydratedState, object currentState, bool[] checkable, ISessionImplementor session)
        {
            throw new NotImplementedException();
        }

        public object NullSafeGet(IDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        public object NullSafeGet(IDataReader rs, string name, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        public void NullSafeSet(IDbCommand st, object value, int index, bool[] settable, ISessionImplementor session)
        {
            throw new NotImplementedException();
        }

        public void NullSafeSet(IDbCommand dbCommand, object value, int index, ISessionImplementor session)
        {
            var sqlCommand = dbCommand as SqlCommand;
            if (sqlCommand != null)
            {
                sqlCommand.Parameters[index].SqlDbType = SqlDbType.Structured;
                sqlCommand.Parameters[index].TypeName = "ObjectIdTable";
                sqlCommand.Parameters[index].Value = value;
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public string ToLoggableString(object value, ISessionFactoryImplementor factory)
        {
            throw new NotImplementedException();
        }

        public object DeepCopy(object val, EntityMode entityMode, ISessionFactoryImplementor factory)
        {
            throw new NotImplementedException();
        }

        public object Hydrate(IDataReader rs, string[] names, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        public object ResolveIdentifier(object value, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        public object SemiResolve(object value, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        public object Replace(object original, object target, ISessionImplementor session, object owner, IDictionary copiedAlready)
        {
            throw new NotImplementedException();
        }

        public object Replace(object original, object target, ISessionImplementor session, object owner, IDictionary copyCache,
            ForeignKeyDirection foreignKeyDirection)
        {
            throw new NotImplementedException();
        }

        public bool IsSame(object x, object y, EntityMode entityMode)
        {
            throw new NotImplementedException();
        }

        public bool IsEqual(object x, object y, EntityMode entityMode)
        {
            throw new NotImplementedException();
        }

        public bool IsEqual(object x, object y, EntityMode entityMode, ISessionFactoryImplementor factory)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(object x, EntityMode entityMode)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(object x, EntityMode entityMode, ISessionFactoryImplementor factory)
        {
            throw new NotImplementedException();
        }

        public int Compare(object x, object y, EntityMode? entityMode)
        {
            throw new NotImplementedException();
        }

        public IType GetSemiResolvedType(ISessionFactoryImplementor factory)
        {
            throw new NotImplementedException();
        }

        public void SetToXMLNode(XmlNode node, object value, ISessionFactoryImplementor factory)
        {
            throw new NotImplementedException();
        }

        public object FromXMLNode(XmlNode xml, IMapping factory)
        {
            throw new NotImplementedException();
        }

        public bool[] ToColumnNullness(object value, IMapping mapping)
        {
            throw new NotImplementedException();
        }

        public string Name { get; private set; }
        public Type ReturnedClass { get; private set; }
        public bool IsMutable { get; private set; }
        public bool IsAssociationType { get; private set; }

        public object Disassemble(object value, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        public object Assemble(object cached, ISessionImplementor session, object owner)
        {
            throw new NotImplementedException();
        }

        public void BeforeAssemble(object cached, ISessionImplementor session)
        {
            throw new NotImplementedException();
        }
    }
}
