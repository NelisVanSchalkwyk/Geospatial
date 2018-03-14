using Common;
using System;

namespace Geospatial
{
    [Serializable]
    public sealed class Position : Equatable<Position>
    {
        public Coordinate Coordinate { get; set; }

        public string Address { get; set; }

        #region Public methods

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode() => CreateHash(Coordinate);

        #endregion
    }
}
