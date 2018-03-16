using Common;
using System.Linq;

namespace Geospatial
{
    /// <summary>
    /// Defines the rectangular bounds for a list of coordinates.
    /// </summary>
    public sealed class Bounds : Equatable<Bounds>
    {
        #region Properties

        /// <summary>
        /// Gets the top latitude/Y value for this bounds.
        /// </summary>
        /// <value>The top.</value>
        public double Top { get; private set; }

        /// <summary>
        /// Gets the left longitude/X value for this bounds.
        /// </summary>
        /// <value>The left.</value>
        public double Left { get; private set; }

        /// <summary>
        /// Gets the bottom latitude/Y value for this bounds.
        /// </summary>
        /// <value>The bottom.</value>
        public double Bottom { get; private set; }

        /// <summary>
        /// Gets the right longitude/X value for this bounds.
        /// </summary>
        /// <value>The right.</value>
        public double Right { get; private set; }

        #endregion

        #region Constructors

        public Bounds()
            : this(new CoordinateList()) { }

        public Bounds(Coordinate southWest, Coordinate northEast)
            : this(new CoordinateList { southWest, northEast }) { }

        public Bounds(double bottom, double left, double top, double right)
        {
            Bottom = bottom;
            Left = left;
            Top = top;
            Right = right;
        }

        public Bounds(CoordinateList coordinates)
        {
            if (coordinates != null && coordinates.Any())
            {
                Top = coordinates.Max(p => p.Y);
                Left = coordinates.Min(p => p.X);
                Bottom = coordinates.Min(p => p.Y);
                Right = coordinates.Max(p => p.X);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns true if the given lat/lng is in this bounds.
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public bool Contains(Coordinate coordinate)
        {
            if (Left < Right)
            {
                return coordinate.Longitude < Right && coordinate.Longitude > Left && coordinate.Latitude < Top && coordinate.Latitude > Bottom;
            }

            // It crosses the date line.
            return (coordinate.Longitude < Right || coordinate.Longitude > Left) && coordinate.Latitude < Top && coordinate.Latitude > Bottom;
        }

        /// <summary>
        /// Extends this bounds to contain the given point.
        /// </summary>
        /// <param name="coordinate"></param>
        public void Extend(Coordinate coordinate)
        {
            if (!Contains(coordinate))
            {
                var coordinates = new CoordinateList
                {
                    GetNorthEast(),
                    GetSouthWest(),
                    coordinate
                };

                Top = coordinates.Max(p => p.Y);
                Left = coordinates.Min(p => p.X);
                Bottom = coordinates.Min(p => p.Y);
                Right = coordinates.Max(p => p.X);
            }
        }

        /// <summary>
        /// Extends this bounds to contain the given bounds.
        /// </summary>
        /// <param name="other">The bound to extend this instance with.</param>
        public void Extend(Bounds other)
        {
            var sw = other.GetSouthWest();
            var ne = other.GetNorthEast();

            Extend(sw);
            Extend(ne);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode() => CreateHash(Bottom, Left, Top, Right);

        /// <summary>
        /// Gets the north east corner coordinate of this bounds.
        /// </summary>
        /// <returns></returns>
        public Coordinate GetNorthEast() => new Coordinate(Top, Right);

        /// <summary>
        /// Gets the south west corner coordinate of this bounds.
        /// </summary>
        /// <returns></returns>
        public Coordinate GetSouthWest() => new Coordinate(Bottom, Left);

        /// <summary>
        /// Expresses the bounds as a csv string in the format: left,top,right,bottom.
        /// </summary>
        /// <returns></returns>
        public string ToCsvString() => $"{Left},{Top},{Right},{Bottom}";

        #endregion
    }
}
