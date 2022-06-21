using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FileActor.Abstract
{
	/// <summary>
	/// Generic version of IFileServiceProvider for specific type
	/// </summary>
	/// <typeparam name="T">type with fileactor configuration</typeparam>
	public interface IFileServiceActor<T> where T : class
	{
		/// <summary>
		/// Saves all file values of an object which has fileActor configuration for its type from all configured back stores
		/// </summary>
		/// <param name="obj">given object</param>
		void SaveAll(T obj);
		/// <summary>
		/// Saves specific file value of an object which has fileActor configuration for its type and given property from all configured back stores
		/// </summary>
		/// <typeparam name="TProperty">property of T</typeparam>
		/// <param name="exp">property expression</param>
		/// <param name="obj">given object</param>
		void Save<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		/// <summary>
		/// Deletes all files of an object which has fileActor configuration for its type from all configured back stores
		/// </summary>
		/// <param name="obj">given object</param>
		void DeleteAll(T obj);
		/// <summary>
		/// Deletes specific file value of an object which has fileActor configuration for its type and given property from all configured back stores
		/// </summary>
		/// <typeparam name="TProperty">property of T</typeparam>
		/// <param name="exp">property expression</param>
		/// <param name="obj">given object</param>
		void Delete<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		/// <summary>
		/// Saves all file values of an object which has fileActor configuration for its type from all configured back stores asynchronously
		/// </summary>
		/// <param name="obj">given object</param>
		Task SaveAllAsync(T obj);
		/// <summary>
		/// Saves specific file value of an object which has fileActor configuration for its type and given property from all configured back stores asynchronously
		/// </summary>
		/// <typeparam name="TProperty">property of T</typeparam>
		/// <param name="exp">property expression</param>
		/// <param name="obj">given object</param>
		Task SaveAsync<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		/// <summary>
		/// Deletes all files of an object which has fileActor configuration for its type from all configured back stores asynchronously
		/// </summary>
		/// <param name="obj">given object</param>
		Task DeleteAllAsync(T obj);
		/// <summary>
		/// Deletes specific file value of an object which has fileActor configuration for its type and given property from all configured back stores asynchronously
		/// </summary>
		/// <typeparam name="TProperty">property of T</typeparam>
		/// <param name="exp">property expression</param>
		/// <param name="obj">given object</param>
		Task DeleteAsync<TProperty>(Expression<Func<T, TProperty>> exp, T obj);
	}
	/// <summary>
	/// abstraction of fileServiceProvider for file actions on types with fileActor configurations
	/// </summary>
	public interface IFileServiceActor
	{
		/// <summary>
		/// Saves all file values of an object which has fileActor configuration for its type from all configured back stores
		/// </summary>
		/// <typeparam name="T">type with fileactor configuration</typeparam>
		/// <param name="obj">given object</param>
		void SaveAll<T>(T obj);
		/// <summary>
		/// Saves specific file value of an object which has fileActor configuration for its type and given property from all configured back stores
		/// </summary>
		/// <typeparam name="T">type with fileactor configuration</typeparam>
		/// <typeparam name="TProperty">property of T</typeparam>
		/// <param name="exp">property expression</param>
		/// <param name="obj">given object</param>
		void Save<T,TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		/// <summary>
		/// Deletes all files of an object which has fileActor configuration for its type from all configured back stores
		/// </summary>
		/// <typeparam name="T">type with fileactor configuration</typeparam>
		/// <param name="obj">given object</param>
		void DeleteAll<T>(T obj);
		/// <summary>
		/// Deletes specific file value of an object which has fileActor configuration for its type and given property from all configured back stores
		/// </summary>
		/// <typeparam name="T">type with fileactor configuration</typeparam>
		/// <typeparam name="TProperty">property of T</typeparam>
		/// <param name="exp">property expression</param>
		/// <param name="obj">given object</param>
		void Delete<T,TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		/// <summary>
		/// Saves all file values of an object which has fileActor configuration for its type from all configured back stores asynchronously
		/// </summary>
		/// <typeparam name="T">type with fileactor configuration</typeparam>
		/// <param name="obj">given object</param>
		Task SaveAllAsync<T>(T obj);
		/// <summary>
		/// Saves specific file value of an object which has fileActor configuration for its type and given property from all configured back stores asynchronously
		/// </summary>
		/// <typeparam name="T">type with fileactor configuration</typeparam>
		/// <typeparam name="TProperty">property of T</typeparam>
		/// <param name="exp">property expression</param>
		/// <param name="obj">given object</param>
		Task SaveAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj);
		/// <summary>
		/// Deletes all files of an object which has fileActor configuration for its type from all configured back stores asynchronously
		/// </summary>
		/// <typeparam name="T">type with fileactor configuration</typeparam>
		/// <param name="obj">given object</param>
		Task DeleteAllAsync<T>(T obj);
		/// <summary>
		/// Deletes specific file value of an object which has fileActor configuration for its type and given property from all configured back stores asynchronously
		/// </summary>
		/// <typeparam name="T">type with fileactor configuration</typeparam>
		/// <typeparam name="TProperty">property of T</typeparam>
		/// <param name="exp">property expression</param>
		/// <param name="obj">given object</param>
		Task DeleteAsync<T, TProperty>(Expression<Func<T, TProperty>> exp, T obj);
	}
}
