using Geospatial.Conversions;
using System;

namespace Geospatial.Formats
{
    public sealed class OsGridReference
    {
        #region Fields


        #endregion

        #region Properties

        public long Easting { get; set; }

        public long Northing { get; set; }

        public string GridReference { get; set; }

        #endregion

        #region Constructors

        public OsGridReference()
            : this(0L, 0L) { }

        public OsGridReference(long easting, long northing)
        {
            Easting = easting;
            Northing = northing;
        }

        public OsGridReference(Coordinate latLng)
        {
            // Convert to OSGB36.
            var transform = HelmertTransform.GetTransform(HelmertTransformType.WGS84toOSGB36);


            var latRad = latLng.GetLatitudeInRadians();
            var lngRad = latLng.GetLongitudeInRadians();

            throw new NotImplementedException();

        }

        #endregion

        #region Public Methods

        public OsGridReference FromLatLng(Coordinate point)
        {
            var ukBounds = new Bounds(49, -7, 61, 2);
            if (!ukBounds.Contains(point))
            {
                throw new ArgumentOutOfRangeException("The provided point does not fall within the defined UK bounds.");
            }



            throw new NotImplementedException();
        }

        public Coordinate ToLatLng()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
