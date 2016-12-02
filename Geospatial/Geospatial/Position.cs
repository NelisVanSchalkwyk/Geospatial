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

        public override int GetHashCode()
        {
            return CreateHash(Coordinate);
        }

        #endregion
    }
}
