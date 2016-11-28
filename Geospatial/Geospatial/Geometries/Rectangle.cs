using Geospatial.Extensions;
using Measurement;
using System;

namespace Geospatial.Geometries
{
    [Serializable]
    public sealed class Rectangle : Shape
    {
        #region Properties

        public double Bottom { get; set; }

        public double Left { get; set; }

        public double Top { get; set; }

        public double Right { get; set; }

        #endregion

        #region Constructors

        public Rectangle()
            : this(0D, 0D, 0D, 0D)
        {
        }

        public Rectangle(double bottom, double left, double top, double right)
        {
            Bottom = bottom;
            Left = left;
            Top = top;
            Right = right;
        }

        public Rectangle(Bounds bounds)
        {
            Bottom = bounds.Bottom;
            Left = bounds.Left;
            Top = bounds.Top;
            Right = bounds.Right;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines the outer bounds or outer rectangle for the shape.
        /// </summary>
        /// <returns>A Bounds object.</returns>
        public override Bounds GetOuterBounds()
        {
            return new Bounds(Bottom, Left, Top, Right);
        }

        /// <summary>
        /// Determines the inner bounds or inner rectangle for the shape.
        /// The inner and outer rectangle for a rectangle shape must be equal.
        /// </summary>
        /// <returns>A Bounds object.</returns>
        public override Bounds GetInnerBounds()
        {
            return GetOuterBounds();
        }

        /// <summary>
        /// Determines whether a specified coordinates is within the defined shape.
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate.</param>
        /// <returns>True if the coordinate is within the shape, else false.</returns>
        public override bool Contains(Coordinate coordinate)
        {
            return GetOuterBounds().Contains(coordinate);
        }

        /// <summary>
        /// Calculates the lenth of the rectangle's circumference.
        /// </summary>
        /// <returns>A <see cref="Distance"/> object.</returns>
        public override Distance Length()
        {
            var lat = 180 / Top - Bottom;
            var lon = 360 / Right - Left;
            if (Left > Right)
            {
                lon = 1 - lon;
            }

            var h1 = Constants.EARTH_MEAN_RADIUS_METERS * (1 - Math.Cos(Top.ToRadians()));
            var h2 = Constants.EARTH_MEAN_RADIUS_METERS * (1 - Math.Cos(Bottom.ToRadians()));

            var r1 = Math.Sqrt(h1 * (2 * Constants.EARTH_MEAN_RADIUS_METERS - h1));
            var r2 = Math.Sqrt(h2 * (2 * Constants.EARTH_MEAN_RADIUS_METERS - h2));

            var c1 = 2 * Math.PI * r1 * lon;
            var c2 = 2 * Math.PI * r2 * lon;
            var c3 = 2 * Math.PI * Constants.EARTH_MEAN_RADIUS_METERS * lat * 2;

            return new Distance(c1 + c2 + c3);
        }

        /// <summary>
        /// Calculates the area of a rectangle.
        /// </summary>
        /// <returns>A <see cref="Area"/> object.</returns>
        public override Area Area()
        {
            var h1 = Constants.EARTH_MEAN_RADIUS_METERS * (1 - Math.Cos(Right.ToRadians()));
            var h2 = Constants.EARTH_MEAN_RADIUS_METERS * (1 - Math.Cos(Left.ToRadians()));
            var zoneArea = 2 * Math.PI * Constants.EARTH_MEAN_RADIUS_METERS * (h2 - h1);
            var lonPercentage = (Right - Left) / 360;
            return new Area(zoneArea * lonPercentage);
        }
        #endregion
    }
}
