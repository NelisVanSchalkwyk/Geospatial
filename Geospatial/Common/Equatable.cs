using System;
using System.Linq;

namespace Common
{
    public abstract class Equatable<T> : IEquatable<T> where T : class, IEquatable<T>
    {
        #region Fields

        static readonly int OffsetBasis = unchecked((int)2166136261);
        static readonly int Prime = 16777619;

        #endregion

        #region Public Methods

        public abstract override int GetHashCode();

        public bool Equals(T other)
        {
            if ((object)other == null)
            {
                return false;
            }

            return GetHashCode() == other.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as T);
        }

        /// <summary>
        /// Creates an FNV-1a alternate algorithm hash.
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public static int CreateHash(params object[] objs)
        {
            return objs.Aggregate(OffsetBasis, (r, o) => (r ^ o.GetHashCode()) * Prime);
        }

        public static bool operator ==(Equatable<T> obj1, Equatable<T> obj2)
        {
            if ((object)obj1 == null || (object)obj2 == null)
            {
                return Object.Equals(obj1, obj2);
            }

            return obj1.Equals(obj2);
        }

        public static bool operator !=(Equatable<T> obj1, Equatable<T> obj2)
        {
            return !(obj1 == obj2);
        }

        #endregion    
    }
}
