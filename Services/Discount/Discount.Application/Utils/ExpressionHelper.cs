using System.Linq.Expressions;

namespace Discount.Application.Utils
{
	public static class ExpressionHelper
	{
		public static Expression<T> And<T>(Expression<T> expr1, Expression<T> expr2)
		{
			var body = Expression.AndAlso(expr1.Body, expr2.Body);
			return Expression.Lambda<T>(body, expr1.Parameters[0]);
		}
		public static Expression<T> Or<T>(Expression<T> expr1, Expression<T> expr2)
		{
			var body = Expression.Or(expr1.Body, expr2.Body);
			return Expression.Lambda<T>(body, expr1.Parameters[0]);
		}
      public static Expression<Func<TTo, bool>> Convert<TFrom, TTo>(Expression<Func<TFrom, bool>> expr)
      {
         Dictionary<Expression, Expression> substitutues = new Dictionary<Expression, Expression>();
         var oldParam = expr.Parameters[0];
         var newParam = Expression.Parameter(typeof(TTo), oldParam.Name);
         substitutues.Add(oldParam, newParam);
         Expression body = ConvertNode(expr.Body, substitutues);
         return Expression.Lambda<Func<TTo, bool>>(body, newParam);
      }
      private static Expression ConvertNode(Expression node, IDictionary<Expression, Expression> subst)
      {
         if (node == null) return null;
         if (subst.ContainsKey(node)) return subst[node];

         switch (node.NodeType)
         {
            case ExpressionType.Constant:
               return node;
            case ExpressionType.MemberAccess:
               {
                  var me = (MemberExpression)node;
                  var newNode = ConvertNode(me.Expression, subst);
                  return Expression.MakeMemberAccess(newNode, newNode.Type.GetMember(me.Member.Name).Single());
               }
            case ExpressionType.Equal: /* will probably work for a range of common binary-expressions */
               {
                  var be = (BinaryExpression)node;
                  return Expression.MakeBinary(be.NodeType, ConvertNode(be.Left, subst), ConvertNode(be.Right, subst), be.IsLiftedToNull, be.Method);
               }
            default:
               throw new NotSupportedException(node.NodeType.ToString());
         }
      }
   }
}
