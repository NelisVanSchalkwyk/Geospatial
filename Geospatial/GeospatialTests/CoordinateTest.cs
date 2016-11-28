using Geospatial;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Measurement;

namespace GeospatialTests
{
    [TestClass]
    public class CoordinateTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            var latitude = -29;
            var longitude = 25.43;
            var actual = new Coordinate(latitude, longitude);
            var expected = new Coordinate();
            expected.Latitude = latitude;
            expected.Longitude = longitude;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "An invalid latitude was allowed.")]
        public void LatitudeLowerBoundsTest()
        {
            var latitude = -91;
            var longitude = 25.43;
            var actual = new Coordinate(latitude, longitude);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "An invalid latitude was allowed.")]
        public void LatitudeUpperBoundsTest()
        {
            var latitude = 91;
            var longitude = 25.43;
            var actual = new Coordinate(latitude, longitude);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "An invalid longitude was allowed.")]
        public void LongitudeLowerBoundsTest()
        {
            var latitude = -29;
            var longitude = -181;
            var actual = new Coordinate(latitude, longitude);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "An invalid longitude was allowed.")]
        public void LongitudeUpperBoundsTest()
        {
            var latitude = -29;
            var longitude = 181;
            var actual = new Coordinate(latitude, longitude);
        }

        [TestMethod]
        public void GetDistanceToTest()
        {
            var start = new Coordinate(-25.824906, 28.259757);
            var end = new Coordinate(-25.864751, 28.257087);
            var actual = Math.Round(start.GetDistanceTo(end), 3);

            // Expected result based upon http://www.movable-type.co.uk/scripts/latlong.html
            var expected = new Distance(4439).Kilometers;
            Assert.AreEqual(expected, actual);
        }
    }
}
