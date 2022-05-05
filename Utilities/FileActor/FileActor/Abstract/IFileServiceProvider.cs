using System;
using System.Linq.Expressions;

namespace FileActor.Abstract
{
	public interface IFileServiceProvider<T> where T : class
	{
		void SaveAll(T obj);
		void Save<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		void DeleteAll(T obj);
		void Delete<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
	}
	public interface IFileServiceProvider
	{
		void SaveAll<T>(T obj);
		void Save<T,TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		void DeleteAll<T>(T obj);
		void Delete<T,TProperty>(Expression<Func<T, TProperty>> exp, T obj);
	}
}
