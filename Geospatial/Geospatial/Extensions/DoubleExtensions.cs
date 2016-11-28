using System;

namespace Geospatial.Extensions
{
    public static class DoubleExtensions
    {
        /// <summary>
        /// Convert numeric degrees to radians.
        /// </summary>
        public static double ToRadians(this double number)
        {
            return number * Math.PI / 180;
        }

        /// <summary>
        /// Convert radians to numeric (signed) degrees.
        /// </summary>
        public static double ToDegrees(this double number)
        {
            return number * 180 / Math.PI;
        }

        public static double NormalizeDegrees(this double degrees)
        {
            while (degrees >= 360)
            {
                degrees -= 360;
            }

            while (degrees < 0)
            {
                degrees += 360;
            }

            return degrees;
        }
    }
}
