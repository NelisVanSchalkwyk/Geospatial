using Measurement;
using System;

namespace Geospatial.Geometries
{
    [Serializable]
    public abstract class Shape
    {
        /// <summary>
        /// Determines the outer bounds or outer rectangle for the shape.
        /// </summary>
        /// <returns>A <see cref="Geospatial.Bounds"/> object which describes 
        /// the rectangle which encapsulates the list of coordinate points.</returns>
        public abstract Bounds GetOuterBounds();

        /// <summary>
        /// Determines the inner bounds or inner rectangle for the shape.
        /// </summary>
        /// <returns>A <see cref="Geospatial.Bounds"/> object which describes 
        /// the rectangle which will be encapsulated by the list of coordinate points.</returns>
        public abstract Bounds GetInnerBounds();

        /// <summary>
        /// Determines whether a specified coordinates is within the defined shape.
        /// </summary>
        /// <param name="coordinate">The coordinate to evaluate.</param>
        /// <returns>True if the coordinate is within the shape, else false.</returns>
        public abstract bool Contains(Coordinate coordinate);

        /// <summary>
        /// Calculates the lenth of the shape's circumference.
        /// </summary>
        /// <returns>A <see cref="Measurement.Distance"/> object.</returns>
        public abstract Distance Length();

        /// <summary>
        /// Calculates the area of a shape.
        /// </summary>
        /// <returns>A <see cref="Measurement.Area"/> object.</returns>
        public abstract Area Area();

    }
}
