using Common;
using Geospatial.Extensions;
using Geospatial.Formats;
using Measurement;
using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Geospatial
{
    [DataContract]
    public sealed class Coordinate : Equatable<Coordinate>
    {
        #region Fields

        double m_Latitude;  // latitude in degrees
        double m_Longitude; // longitude in degrees

        const double m_EarthRadius = 6371.01; // The earth's radius in kilometers.

        #endregion

        #region Properties

        /// <summary>
        /// The latitude (Y) of the geographical point.
        /// </summary>
        [DataMember]
        public double Latitude
        {
            get
            {
                return m_Latitude;
            }
            set
            {
                if (!(value >= -90 && value <= 90))
                {
                    throw new ArgumentOutOfRangeException("Latitude", "Latitude must be between -90 and 90 degrees.");
                }

                m_Latitude = value;
            }
        }

        /// <summary>
        /// The longitude (X) of the geographical point.
        /// </summary>
        [DataMember]
        public double Longitude
        {
            get
            {
                return m_Longitude;
            }
            set
            {
                if (!(value >= -180 && value <= 180))
                {
                    throw new ArgumentOutOfRangeException("Longitude", "Longitude must be between -180 and 180 degrees.");
                }

                m_Longitude = value;
            }
        }

        /// <summary>
        /// The X (longitude) value of the geographical point.
        /// </summary>
        /// <value>The X.</value>
        public double X
        {
            get
            {
                return m_Longitude;
            }
            set
            {
                Longitude = value;
            }
        }

        /// <summary>
        /// The Y (latitude) value of the geographical point.
        /// </summary>
        /// <value>The Y.</value>
        public double Y
        {
            get
            {
                return m_Latitude;
            }
            set
            {
                Latitude = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        public Coordinate()
            : this(0D, 0D) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="latitude">The latitude, in degrees.</param>
        /// <param name="longitude">The longitude, in degrees.</param>
        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the distance in kilometers between the latitude and longitude coordinates that are specified 
        /// by this Coordinate and another specified Coordinate.  The earth is approximated as a 
        /// sphere in the formula used to perform the calculation. 
        /// See http://en.wikipedia.org/wiki/Great-circle_distance for more info.
        /// </summary>
        /// <param name="coordinate">The Coordinate for the location to calculate the distance to.</param>
        /// <returns>The distance between the two coordinates, in kilometers.</returns>        
        public double GetDistanceTo(Coordinate other)
        {
            var lat1Rad = Latitude.ToRadians();
            var lat2Rad = other.Latitude.ToRadians();
            var deltaLngRad = (other.Longitude - Longitude).ToRadians();

            return Math.Acos(Math.Sin(lat1Rad) * Math.Sin(lat2Rad) +
                    Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                    Math.Cos(deltaLngRad)) * m_EarthRadius;
        }

        /// <summary>
        /// Returns the (initial) bearing from this coordinate to another coordinate in degrees.
        /// </summary>
        /// <param name="other">The Coordinate for the destination to calculate the bearing to.</param>
        /// <returns>Initial bearing in degrees from north.</returns>
        public double GetBearingTo(Coordinate other)
        {
            var lat1Rad = Latitude.ToRadians();
            var lat2Rad = other.Latitude.ToRadians();
            var deltaLngRad = (other.Longitude - Longitude).ToRadians();

            var y = Math.Sin(deltaLngRad) * Math.Cos(lat2Rad);
            var x = Math.Cos(lat1Rad) * Math.Sin(lat2Rad) - Math.Sin(lat1Rad) * Math.Cos(lat2Rad) * Math.Cos(deltaLngRad);
            var bearing = Math.Atan2(y, x);

            // Normalise the result to a compass bearing (in the range 0° ... 360°, with −ve values transformed into the range 180° ... 360°).
            return (bearing.ToDegrees() + 360) % 360;
        }

        /// <summary>
        /// Returns the coordinate at a specified distance and bearing from this coordinate.
        /// </summary>
        /// <param name="distance">The distance from this coordinate.</param>
        /// <param name="bearing">Initial bearing in degrees.</param>
        /// <returns></returns>
        public Coordinate GetCoordinateAt(Distance distance, double bearing)
        {
            var bearingRad = bearing.ToRadians();
            var anglurarDistanceRad = distance.Kilometers / m_EarthRadius;
            var lat1Rad = Latitude.ToRadians();
            var lng1Rad = Longitude.ToRadians();

            var lat2Rad = Math.Asin(Math.Sin(lat1Rad) * Math.Cos(bearingRad)) + Math.Cos(lat1Rad) * Math.Sin(anglurarDistanceRad) * Math.Cos(bearingRad);
            var y = Math.Sin(bearingRad) * Math.Sin(anglurarDistanceRad) * Math.Cos(lat1Rad);
            var x = Math.Cos(anglurarDistanceRad) - Math.Sin(lat1Rad) * Math.Sin(lat2Rad);
            var lng2Rad = lng1Rad + Math.Atan2(y, x);

            lng2Rad = (lng2Rad + 3 * Math.PI) % (2 * Math.PI) - Math.PI;

            var lat2 = lat2Rad.ToDegrees();
            var lng2 = lng2Rad.ToDegrees();

            return new Coordinate(lat2, lng2);
        }

        /// <summary>
        /// Returns the coordinate in Degrees Minutes Seconds (DMS) format.
        /// </summary>
        /// <returns>The DMS formatted string.</returns>
        public string ToDegreesMinutesSeconds()
        {
            var latDMS = new DegreesMinutesSeconds(Latitude);
            var lngDMS = new DegreesMinutesSeconds(Longitude, true);

            return $"{latDMS}, {lngDMS}";
        }

        /// <summary>
        /// Returns the coordinate in Degrees Decimal Minutes (DDM) format.
        /// </summary>
        /// <returns>The DDM formatted string.</returns>
        public string ToDegreesDecimalMinutes()
        {
            var latDDM = new DegreesDecimalMinutes(Latitude, false);
            var lngDDM = new DegreesDecimalMinutes(Longitude, true);

            return $"{latDDM}, {lngDDM}";
        }

        public string ToOSGridReference()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Formats the coordinate into the specified format.
        /// </summary>
        /// <param name="coordinateFormat">The format.</param>
        /// <returns></returns>
        public string Format(CoordinateFormat coordinateFormat)
        {
            switch (coordinateFormat)
            {
                case CoordinateFormat.DegreesMinutesSeconds:
                    return ToDegreesMinutesSeconds();
                case CoordinateFormat.DegreesDecimalMinutes:
                    return ToDegreesDecimalMinutes();
                case CoordinateFormat.DecimalDegrees:
                default:
                    return ToString();
            }
        }

        /// <summary>
        /// Returns the latitude in radians.
        /// </summary>
        /// <returns></returns>
        public double GetLatitudeInRadians() => Latitude.ToRadians();

        /// <summary>
        /// Returns the longitude in radians.
        /// </summary>
        /// <returns></returns>
        public double GetLongitudeInRadians() => Longitude.ToRadians();

        /// <summary>
        /// Provides the latitude and longitude as a readable string.
        /// </summary>
        /// <returns>Latitude,Longitude</returns>
        public override string ToString() => ToString(CultureInfo.CurrentCulture, 6);

        /// <summary>
        /// Provides the latitude and longitude as a readable string with a specific number of decimal places.
        /// </summary>
        /// <param name="decimalPlaces">The number of decimal places.</param>
        /// <returns>A string in the format: Latitude,Longitude</returns>
        public string ToString(IFormatProvider provider, uint decimalPlaces) => $"{Latitude.ToString("N" + decimalPlaces, provider ?? CultureInfo.CurrentCulture)},{Longitude.ToString("N" + decimalPlaces, provider ?? CultureInfo.CurrentCulture)}";

        /// <summary>
        /// Provides the coordinate as a readable string in the format longitude,latitude.
        /// </summary>
        /// <returns>Longitude,Latitude</returns>
        public string ToLngLatString(IFormatProvider provider) => string.Format(provider ?? CultureInfo.CurrentCulture, "{0},{1}", Longitude, Latitude);

        /// <summary>
        /// Provides the coordinate as a readable string in the format longitude,latitude.
        /// </summary>
        /// <returns>Longitude,Latitude</returns>
        public string ToLngLatString() => $"{Longitude:N6},{Latitude:N6}";

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return base.Equals(obj);
        }

        /// <summary>
        /// Gets the hash code. Used to determine object equality.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => CreateHash(Latitude, Longitude);

        #endregion

        public enum CoordinateFormat
        {
            [EnumMember]
            DecimalDegrees,

            [EnumMember]
            DegreesMinutesSeconds,

            [EnumMember]
            DegreesDecimalMinutes
        }

    }
}
