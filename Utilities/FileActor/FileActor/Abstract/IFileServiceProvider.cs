using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileActor.Abstract
{
	public interface IFileServiceProvider<T> where T : class
	{
		void SaveAll(T obj);
		void Save<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		void DeleteAll(T obj);
		void Delete<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		Task SaveAllAsync(T obj);
		Task SaveAsync<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		Task DeleteAllAsync(T obj);
		Task DeleteAsync<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
	}
	public interface IFileServiceProvider
	{
		void SaveAll<T>(T obj);
		void Save<T,TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		void DeleteAll<T>(T obj);
		void Delete<T,TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		Task SaveAllAsync<T>(T obj);
		Task SaveAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		Task DeleteAllAsync<T>(T obj);
		Task DeleteAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj);
	}
}
