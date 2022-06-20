using FileActor.Internal;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileActor.Abstract
{
	/// <summary>
	/// abstraction for getting fileActor configuration info
	/// </summary>
	public interface IConfigurationManager
	{
		/// <summary>
		/// get all fileActor info of a class
		/// </summary>
		/// <param name="obj">obj of type which has fileActor Configuration</param>
		/// <returns>all fileActor information with given object</returns>
		IEnumerable<FileStreamInfo> GetAllInfo<T>(T obj);
		/// <summary>
		/// get a specific fileActor info from a property of a class
		/// </summary>
		/// <typeparam name="T">class with fileActor Configuration</typeparam>
		/// <typeparam name="TProperty">property of T which has fileActor Configuration</typeparam>
		/// <param name="exp">selected property</param>
		/// <returns>selected property information</returns>
		FileStreamInfo GetInfo<T, TProperty>(T obj,Expression<Func<T, TProperty>> exp);
		/// <summary>
		/// get a specific fileActor info from a property of a class
		/// </summary>
		/// <param name="obj">obj of type which has fileActor Configuration</param>
		/// <param name="propertyName">selected property</param>
		/// <returns></returns>
		FileStreamInfo GetInfo<T>(T obj, string propertyName);
	}
}
