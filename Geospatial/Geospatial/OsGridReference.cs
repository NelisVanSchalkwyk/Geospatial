using System;

namespace Geospatial
{
    [Serializable]
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
