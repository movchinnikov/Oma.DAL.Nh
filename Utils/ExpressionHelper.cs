namespace Oma.DAL.Nh.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using NHibernate.SqlCommand;
    using Attributes;

    public class ExpressionHelper
    {
        public static string GetAliasAssociationPath<T>(Expression<Func<T, object>> expression)
        {
            var stack = InternalGetAliasAssociationPath(expression);
            return stack.Count > 0 ?
                stack.Aggregate((string left, string right) => left + "." + right).TrimStart('.') :
                string.Empty;
        }

        private static Stack<string> InternalGetAliasAssociationPath<T>(Expression<Func<T, object>> expression)
        {
            var stack = new Stack<string>();
            var expression2 = expression.Body;
            while (expression2 != null && stack.Count < 2)
            {
                if (expression2.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpression = (MemberExpression)expression2;
                    expression2 = memberExpression.Expression;
                    var member = memberExpression.Member;
                    if (stack.Count == 0)
                    {
                        stack.Push(member.Name);
                    }
                    else
                    {
                        var attr = member.GetCustomAttributes(typeof(AliasAttribute), false).FirstOrDefault() as AliasAttribute;
                        stack.Push(attr == null ? member.Name : attr.Alias);
                    }
                }
                else
                {
                    if (expression2.NodeType != ExpressionType.Parameter) break;
                    expression2 = null;
                }
            }

            return stack;
        }

        public static AliasAttribute GetAlias<T>(Expression<Func<T, object>> expression)
        {
            var expression2 = expression.Body;
            if (expression2.NodeType == ExpressionType.MemberAccess)
            {
                var memberExpression = (MemberExpression)expression2;
                var member = memberExpression.Member;
                var attr = member.GetCustomAttributes(typeof(AliasAttribute), false).FirstOrDefault() as AliasAttribute;
                return attr ?? new AliasAttribute(member.Name) { JoinType = JoinType.InnerJoin };
            }
            throw new Exception(ExceptionMessages.OnlyMemberAccess);
        }
    }
}