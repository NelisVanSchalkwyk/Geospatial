using Measurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geospatial.Geometries
{
    [Serializable]
    public sealed class Line : Shape
    {
        #region Properties

        public CoordinateList Vertices { get; set; }

        public Distance Tolerance { get; set; }

        #endregion

        #region Constructors

        public Line()
            : this(new CoordinateList(), new Distance())
        {
        }

        public Line(CoordinateList points, Distance tolerance)
        {
            Vertices = points;
            Tolerance = tolerance;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Determines the outer bounds or outer rectangle for the shape.
        /// </summary>
        /// <returns>A Bounds object.</returns>
        public override Bounds GetOuterBounds()
        {
            if (Tolerance != null && Tolerance.Meters > 0)
            {
                // The line shape can be approximated as a circle shape at each point.
                var circles = Vertices.Select(v => new Circle(v, Tolerance));
                var bounds = new Bounds();

                foreach (var circle in circles)
                {
                    bounds.Extend(circle.GetOuterBounds());
                }

                return bounds;
            }
            else
            {
                return Vertices.GetOuterBounds();
            }
        }

        /// <summary>
        /// Determines the inner bounds or inner rectangle for the shape.
        /// </summary>
        /// <returns>A Bounds object.</returns>
        public override Bounds GetInnerBounds()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether a specified coordinates is within the defined shape.
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate.</param>
        /// <returns>True if the coordinate is within the shape, else false.</returns>
        public override bool Contains(Coordinate coordinate)
        {
            // Determine if the point is inside the bounding rectangle.
            var insideBoundingRectangle = this.GetOuterBounds().Contains(coordinate);
            if (!insideBoundingRectangle)
            {
                return false;
            }

            if (Tolerance != null && Tolerance.Meters > 0)
            {
                // The line shape can be approximated as a circle shape at each point.
                // Check if the circles contain the coordinate.
                var circles = Vertices.Select(v => new Circle(v, Tolerance));
                var contains = circles.FirstOrDefault(c => c.Contains(coordinate));
                return contains != null;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Calculates the lenth of the line.
        /// </summary>
        /// <returns>A <see cref="Distance"/> object.</returns>
        public override Distance Length()
        {
            return Vertices.GetLength();
        }

        /// <summary>
        /// Calculates the area of a line.  Area can only be calculated if a tolerance is specified.
        /// </summary>
        /// <returns>A <see cref="Area"/> object.</returns>
        public override Area Area()
        {
            if (Tolerance.Meters <= 0)
            {
                return new Area();
            }

            throw new NotImplementedException();
        }

        #endregion
    }
}
