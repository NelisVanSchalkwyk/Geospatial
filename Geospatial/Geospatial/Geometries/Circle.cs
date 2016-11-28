using Geospatial.Extensions;
using Measurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geospatial.Geometries
{
    [Serializable]
    public sealed class Circle : Shape
    {
        #region Properties

        public Coordinate Center { get; set; }
        public Distance Radius { get; set; }

        #endregion

        #region Constructors

        public Circle()
            : this(new Coordinate(), new Distance())
        {
        }

        public Circle(Coordinate center, Distance radius)
        {
            Center = center;
            Radius = radius;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Computes the bounding coordinates of all points on the surface
        /// of a sphere that have a great circle distance to the point represented
        /// by a Coordinate instance that is less or equal to the distance
        /// argument.
        /// For more information about the formulae used in this method visit
        /// http://JanMatuschek.de/LatitudeLongitudeBoundingCoordinates
        /// </summary>
        /// <param name="distance">The distance from the point represented by this 
        /// Circle instance's center. Must me measured in the same unit as the radius property.
        /// </param>
        /// <returns>A Bounds object.</returns>
        public override Bounds GetOuterBounds()
        {
            var radLat = Center.GetLatitudeInRadians();
            var radLng = Center.GetLongitudeInRadians();
            var distance = Radius.Meters;
            var MIN_LAT = -90d.ToRadians();  // -PI/2
            var MAX_LAT = 90d.ToRadians();   //  PI/2
            var MIN_LNG = -180d.ToRadians(); // -PI
            var MAX_LNG = 180d.ToRadians();  //  PI

            if (distance < 0d)
            {
                throw new Exception("Distance cannot be less than 0.");
            }

            // Angular distance in radians on a great circle
            double earthRadius = 6371.01;
            double radDist = distance / earthRadius;

            double minLat = radLat - radDist;
            double maxLat = radLat + radDist;

            double minLng, maxLng;
            if (minLat > MIN_LAT && maxLat < MAX_LAT)
            {
                double deltaLon = Math.Asin(Math.Sin(radDist) / Math.Cos(radLat));
                minLng = radLng - deltaLon;
                if (minLng < MIN_LNG)
                {
                    minLng += 2d * Math.PI;
                }

                maxLng = radLng + deltaLon;
                if (maxLng > MAX_LNG)
                {
                    maxLng -= 2d * Math.PI;
                }
            }
            else
            {
                // a pole is within the distance
                minLat = Math.Max(minLat, MIN_LAT);
                maxLat = Math.Min(maxLat, MAX_LAT);
                minLng = MIN_LNG;
                maxLng = MAX_LNG;
            }

            var top = maxLat.ToDegrees();
            var left = minLng.ToDegrees();
            var bottom = minLat.ToDegrees();
            var right = maxLng.ToDegrees();

            return new Bounds(bottom, left, top, right);
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
            // Determine the distance between the center of this Circle and the coordinate.
            var distance = Center.GetDistanceTo(coordinate);

            return distance <= Radius.Meters;
        }

        /// <summary>
        /// Calculates the lenth of the circle's circumference.
        /// </summary>
        /// <returns>A <see cref="Distance"/> object.</returns>
        public override Distance Length()
        {
            var h = Constants.EARTH_MEAN_RADIUS_METERS * (1 - Math.Cos(Radius.Meters / Constants.EARTH_MEAN_RADIUS_METERS));
            var circumference = 2 * Math.PI * Math.Sqrt(2 * Constants.EARTH_MEAN_RADIUS_METERS - h);
            return new Distance(circumference);
        }

        /// <summary>
        /// Calculates the area of a circle.
        /// </summary>
        /// <returns>A <see cref="Area"/> object.</returns>
        public override Area Area()
        {
            if (Radius.Meters <= 0)
            {
                return new Area(0d);
            }

            if (Radius.Meters > Math.PI * Constants.EARTH_MEAN_RADIUS_METERS)
            {
                return new Area(0d);
            }

            var h = Constants.EARTH_MEAN_RADIUS_METERS * (1 - Math.Cos(Radius.Meters / Constants.EARTH_MEAN_RADIUS_METERS));
            var area = 2 * Math.PI * Radius.Meters * h;
            return new Area(area);
        }

        #endregion
    }
}
