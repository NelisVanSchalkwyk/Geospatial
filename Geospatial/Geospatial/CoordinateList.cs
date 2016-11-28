using Common.Extensions;
using Geospatial.Extensions;
using Measurement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Geospatial
{
    /// <summary>
    /// Represents a list of coordinate points.
    /// </summary>
    [Serializable]
    public sealed class CoordinateList : List<Coordinate>
    {
        #region Properties

        public bool IsClosed
        {
            get
            {
                return Count > 2 && this[0].Equals(this[Count - 1]);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateList"/> class.
        /// </summary>
        public CoordinateList()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateList"/> class 
        /// from a comma seperated string in the format longitude,latitude.
        /// </summary>
        /// <param name="lngLatString">A comma seperated string in the format longitude,latitude.</param>
        public CoordinateList(string lngLatString)
        {
            var points = lngLatString.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

            if (points.Length < 2)
            {
                return;
            }

            // Points is an array in the format [lng,lat,lng,lat]
            // We need to create a list of coordinate objects from this array.
            for (int i = 0; i < points.Length; i += 2)
            {
                this.Add(new Coordinate(Convert.ToDouble(points[i + 1]), Convert.ToDouble(points[i])));
            }
        }

        #endregion

        #region public Methods

        /// <summary>
        /// Provides a comma seperated string of latitude,longitude,latitude,longitude,... values
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToCsv();
        }

        /// <summary>
        /// Provides a comma seperated string of longitude,latitude,longitude,latitude,... values
        /// </summary>
        /// <returns></returns>
        public string ToLngLatString()
        {
            return this.ToLngLatArray().ToCsv();
        }

        /// <summary>
        /// Provides an array of type double in the format [longitude,latitude,longitude,latitude,...].
        /// </summary>
        /// <returns></returns>
        public double[] ToLngLatArray()
        {
            var points = new List<double>();
            foreach (var coordinate in this)
            {
                points.Add(coordinate.Longitude);
                points.Add(coordinate.Latitude);
            }

            return points.ToArray();
        }

        /// <summary>
        /// Calculates the center coordinate for this list.
        /// </summary>
        /// <returns></returns>
        public Coordinate GetCenter()
        {
            if (!this.Any())
            {
                throw new Exception("The list is empty.");
            }

            int total = this.Count;

            double X = 0;
            double Y = 0;
            double Z = 0;

            foreach (var i in this)
            {
                double lat = i.Latitude * Math.PI / 180;
                double lon = i.Longitude * Math.PI / 180;

                double x = Math.Cos(lat) * Math.Cos(lon);
                double y = Math.Cos(lat) * Math.Sin(lon);
                double z = Math.Sin(lat);

                X += x;
                Y += y;
                Z += z;
            }

            X = X / total;
            Y = Y / total;
            Z = Z / total;

            double Lon = Math.Atan2(Y, X);
            double Hyp = Math.Sqrt(X * X + Y * Y);
            double Lat = Math.Atan2(Z, Hyp);

            return new Coordinate(Lat * 180 / Math.PI, Lon * 180 / Math.PI);
        }

        /// <summary>
        /// Get the outer bounds for the list of coordinates.
        /// </summary>
        /// <returns></returns>
        public Bounds GetOuterBounds()
        {
            return new Bounds(this);
        }

        /// <summary>
        /// Get the inner bounds for the list of coordinates.
        /// </summary>
        /// <returns></returns>
        public Bounds GetInnerBounds()
        {
            throw new NotImplementedException();
            //float top = 0, bottom = 0, left = 0, right = 0;

            //if (this.Any())
            //{
            //    // Calculate the inner rectangle of the polygon.
            //    if (!Utils.NearestSearch.PolygonUtils.GetInnerRectangleFromPolygon(this.ToLngLatArray(), out left, out bottom, out right, out top))
            //    {
            //        throw new Exception("Inner rectangle could not be calculated for the polygon.");
            //    }
            //}

            //var southWest = new Coordinate(bottom, left);
            //var northEast = new Coordinate(top, right);

            //return new Bounds(southWest, northEast);
        }

        public Distance GetLength()
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        public Area GetArea()
        {
            var area = 0D;
            if (Count > 3 && IsClosed)
            {
                for (var i = 0; i < Count - 1; i++)
                {
                    var p1 = this[i];
                    var p2 = this[i + 1];
                    area += (p2.Longitude - p1.Longitude).ToRadians() *
                            (2 + Math.Sin(p1.Latitude.ToRadians()) +
                             Math.Sin(p2.Latitude.ToRadians()));
                }
                area = area * Constants.EARTH_MEAN_RADIUS_METERS * Constants.EARTH_MEAN_RADIUS_METERS / 2.0;
            }
            return new Area(area);
        }

        #endregion
    }
}
