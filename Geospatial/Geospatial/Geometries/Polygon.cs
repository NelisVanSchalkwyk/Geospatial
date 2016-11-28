using Measurement;
using System;

namespace Geospatial.Geometries
{
    [Serializable]
    public sealed class Polygon : Shape
    {
        #region Properties

        public CoordinateList Points { get; set; }

        #endregion

        #region Constructors

        public Polygon()
            : this(new CoordinateList())
        {
        }

        public Polygon(CoordinateList points)
        {
            Points = points;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines the outer bounds or outer rectangle for the shape.
        /// </summary>
        /// <returns>A Bounds object.</returns>
        public override Bounds GetOuterBounds()
        {
            return Points.GetOuterBounds();
        }

        /// <summary>
        /// Determines the inner bounds or inner rectangle for the shape.
        /// </summary>
        /// <returns>A Bounds object.</returns>
        public override Bounds GetInnerBounds()
        {
            return Points.GetInnerBounds();
        }

        /// <summary>
        /// Determines whether a specified coordinates is within the defined shape.
        /// </summary>
        /// <remarks>
        /// This method uses the ray casting algorithm. See http://en.wikipedia.org/wiki/Point_in_polygon for more information.
        /// </remarks>
        /// <param name="coordinate">The coordinate to evaluate.</param>
        /// <returns>True if the coordinate is within the shape, else false.</returns>
        public override bool Contains(Coordinate coordinate)
        {
            // First determine if the point is within the inner rectangle.
            var insideInnerRectangle = this.GetInnerBounds().Contains(coordinate);
            if (insideInnerRectangle)
            {
                return true;
            }

            // Now, determine if the point is inside the bounding rectangle.
            var insideBoundingRectangle = this.GetOuterBounds().Contains(coordinate);
            if (!insideBoundingRectangle)
            {
                return false;
            }

            // Since we now know that the point is somewhere between the inner rectangle and bounding rectangle,
            // we can perform the more complex calculations.
            var j = Points.Count - 1;
            var isIn = false;

            for (int i = 0; i < Points.Count; i++)
            {
                if ((Points[i].Y < coordinate.Y && Points[j].Y >= coordinate.Y) || (Points[j].Y < coordinate.Y && Points[i].Y >= coordinate.Y))
                {
                    if (Points[i].X + (coordinate.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) * (Points[j].X - Points[i].X) < coordinate.X)
                    {
                        isIn = !isIn;
                    }
                    j = i;
                }
            }

            return isIn;
        }

        public override Distance Length()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public override Area Area()
        {
            return Points.GetArea();
        }

        #endregion
    }
}
