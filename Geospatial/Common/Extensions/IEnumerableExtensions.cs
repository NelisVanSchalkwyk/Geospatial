using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the collection.
        /// </summary>
        /// <typeparam name="T">The type of the members of enumerable.</typeparam>
        /// <param name="enumerable">A collection that contains the objects to perform the action on.</param>
        /// <param name="action">The Action&lt;T&gt delegate to perform on each element of the List&lt;T&gt.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if action is null.</exception>
        public static void Each<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        /// <summary>
        /// Concatenates the members of a collection, using a comma as separator between each member.
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="values">A collection that contains the objects to concatenate.</param>
        /// <returns>A string that consists of the members of values delimited by a comma. 
        /// If values has no members, the method returns <see cref="System.String.Empty"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if values is null.</exception>
        public static string ToCsv<T>(this IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            return values.Join(",");
        }

        /// <summary>
        /// Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <typeparam name="T">The type of the members of values.</typeparam>
        /// <param name="values">A collection that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator. separator is included in the 
        /// returned string only if values has more than one element.</param>
        /// <returns>A string that consists of the members of values delimited by the separator string. 
        /// If values has no members, the method returns <see cref="System.String.Empty"/>.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if values is null.</exception>
        /// <remarks>
        /// If separator is null, an empty string (<see cref="System.String.Empty"/>) is used instead. 
        /// If any member of values is null, an empty string is used instead.
        /// </remarks>
        public static string Join<T>(this IEnumerable<T> values, string separator)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            var builder = new StringBuilder();
            if (values.Any())
            {
                builder.Append(values.First());
                values.Skip(1).Aggregate(builder, (sb, s) => sb.Append(separator).Append(s));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Determines whether a collection is null or has no elements without having to enumerate the entire collection to get a count.  Uses LINQ.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="values">The items.</param>
        /// <returns>
        /// <c>true</c> if this list is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> values)
        {
            return values == null || !values.Any();
        }
    }
}
