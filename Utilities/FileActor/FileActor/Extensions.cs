using FileActor.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace FileActor
{
	public static class Extensions
	{
		/// <summary>
		/// Gets a MemberInfo from a member expression.
		/// </summary>
		public static MemberInfo GetMember<T, TProperty>(this Expression<Func<T, TProperty>> expression)
		{
			var memberExp = RemoveUnary(expression.Body) as MemberExpression;

			if (memberExp == null)
			{
				return null;
			}

			Expression currentExpr = memberExp.Expression;

			// Unwind the expression to get the root object that the expression acts upon.
			while (true)
			{
				currentExpr = RemoveUnary(currentExpr);

				if (currentExpr != null && currentExpr.NodeType == ExpressionType.MemberAccess)
				{
					currentExpr = ((MemberExpression)currentExpr).Expression;
				}
				else
				{
					break;
				}
			}

			if (currentExpr == null || currentExpr.NodeType != ExpressionType.Parameter)
			{
				return null; // We don't care if we're not acting upon the model instance.
			}

			return memberExp.Member;
		}

		private static Expression RemoveUnary(Expression toUnwrap)
		{
			if (toUnwrap is UnaryExpression)
			{
				return ((UnaryExpression)toUnwrap).Operand;
			}

			return toUnwrap;
		}

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
		public static FileStreamInfo SetTargetProperty(this FileStreamInfo info, string targetProperty)
		{
			info.TargetProperty = targetProperty;
			return info;
		}
		public static FileStreamInfo SetPath(this FileStreamInfo info, string path)
		{
			info.RelativePath = path;
			return info;
		}
	}
}
