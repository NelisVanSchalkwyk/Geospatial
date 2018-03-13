using System;

namespace Measurement
{
    /// <summary>
    /// This class performs measurment unit conversions.
    /// </summary>
    public static class UnitConverter
    {
        const double MILES_PER_KILOMETER = 0.621371192237;
        const double FEET_PER_METER = 3.28083989501;
        const double FEET_PER_MILE = 5280;

        #region Public Methods

        #region Distance

        /// <summary>
        /// Converts meters to kilometers.
        /// </summary>
        /// <param name="meters">The meter distance to convert.</param>
        /// <returns>The distance as kilometers.</returns>
        public static double MetersToKilometers(double meters) => meters / 1000;

        /// <summary>
        /// Converts meters to feet.
        /// </summary>
        /// <param name="meters">The meter distance to convert.</param>
        /// <returns>The distance as feet.</returns>
        public static double MetersToFeet(double meters) => meters * FEET_PER_METER;

        /// <summary>
        /// Converts meters to miles.
        /// </summary>
        /// <param name="meters">The meter distance to convert.</param>
        /// <returns>The distance as miles.</returns>
        public static double MetersToMiles(double meters) => KilometersToMiles(MetersToKilometers(meters));

        /// <summary>
        /// Converts kilometers to meters.
        /// </summary>
        /// <param name="kilometers">The kilometer distance to convert.</param>
        /// <returns>The distance as meters.</returns>
        public static double KilometersToMeters(double kilometers) => kilometers * 1000;

        /// <summary>
        /// Converts kilometers to feet.
        /// </summary>
        /// <param name="kilometers">The kilometer distance to convert.</param>
        /// <returns>The distance as feet.</returns>
        public static double KilometersToFeet(double kilometers) => MetersToFeet(KilometersToMeters(kilometers));

        /// <summary>
        /// Converts kilometers to miles.
        /// </summary>
        /// <param name="kilometers">The kilometer distance to convert.</param>
        /// <returns>The distance as miles.</returns>
        public static double KilometersToMiles(double kilometers) => kilometers * MILES_PER_KILOMETER;

        /// <summary>
        /// Converts feet to meters.
        /// </summary>
        /// <param name="kilometers">The feet distance to convert.</param>
        /// <returns>The distance in meters.</returns>
        public static double FeetToMeters(double feet) => feet / FEET_PER_METER;

        /// <summary>
        /// Converts feet to kilometers.
        /// </summary>
        /// <param name="kilometers">The feet distance to convert.</param>
        /// <returns>The distance in kilometers.</returns>
        public static double FeetToKilometers(double feet) => MetersToKilometers(FeetToMeters(feet));

        /// <summary>
        /// Converts feet to miles.
        /// </summary>
        /// <param name="kilometers">The feet distance to convert.</param>
        /// <returns>The distance in miles.</returns>
        public static double FeetToMiles(double feet) => feet / FEET_PER_MILE;

        /// <summary>
        /// Converts miles to kilometers.
        /// </summary>
        /// <param name="kilometers">The mile distance to convert.</param>
        /// <returns>The distance in kilometers.</returns>
        public static double MilesToKilometers(double miles) => miles / MILES_PER_KILOMETER;

        /// <summary>
        /// Converts miles to meters.
        /// </summary>
        /// <param name="kilometers">The mile distance to convert.</param>
        /// <returns>The distance in meters.</returns>
        public static double MilesToMeters(double miles) => KilometersToMeters(MilesToKilometers(miles));

        /// <summary>
        /// Converts miles to feet.
        /// </summary>
        /// <param name="kilometers">The mile distance to convert.</param>
        /// <returns>The distance in feet.</returns>
        public static double MilesToFeet(double miles) => miles * FEET_PER_MILE;

        #endregion

        #region Speed

        /// <summary>
        /// Converts kilometers per hour(km/h) to miles per hour(mph).
        /// </summary>
        /// <param name="kilometers">The kilometers per hour speed to convert.</param>
        /// <returns>The speed as miles per hour.</returns>
        public static double KilometersPerHourToMilesPerHour(double kilometersPerHour) => kilometersPerHour * MILES_PER_KILOMETER;

        /// <summary>
        /// Converts miles per hour(mph) to kilometers per hour(km/h).
        /// </summary>
        /// <param name="kilometers">The miles per hour speed to convert.</param>
        /// <returns>The speed as kilometers per hour.</returns>
        public static double MilesPerHourToKilometersPerHour(double milesPerHour) => milesPerHour / MILES_PER_KILOMETER;

        #endregion

        #region Area

        /// <summary>
        /// Converts square meter to square kilometer.
        /// </summary>
        /// <param name="squareMeter">The square meter area to convert.</param>
        /// <returns>The area in square kilometer.</returns>
        public static double SquareMeterToSquareKilometer(double squareMeter) => squareMeter * Math.Pow((double)1 / 1000, 2);

        /// <summary>
        /// Converts square meter to square foot.
        /// </summary>
        /// <param name="squareMeter">The square meter area to convert.</param>
        /// <returns>The area in square foot.</returns>
        public static double SquareMeterToSquareFoot(double squareMeter) => squareMeter * Math.Pow(FEET_PER_METER, 2);

        /// <summary>
        /// Converts square meter to square mile.
        /// </summary>
        /// <param name="squareMeter">The square meter area to convert.</param>
        /// <returns>The area in square mile.</returns>
        public static double SquareMeterToSquareMile(double squareMeter) => squareMeter * Math.Pow(MILES_PER_KILOMETER / 1000, 2);

        /// <summary>
        /// Converts square kilometer to square meter.
        /// </summary>
        /// <param name="squareMeter">The square kilometer area to convert.</param>
        /// <returns>The area in square meter.</returns>
        public static double SquareKilometerToSquareMeter(double squareKilometer) => squareKilometer * Math.Pow(1000, 2);

        /// <summary>
        /// Converts square kilometer to square foot.
        /// </summary>
        /// <param name="squareMeter">The square kilometer area to convert.</param>
        /// <returns>The area in square foot.</returns>
        public static double SquareKilometerToSquareFoot(double squareKilometer) => squareKilometer * Math.Pow(FEET_PER_METER * 1000, 2);

        /// <summary>
        /// Converts square kilometer to square mile.
        /// </summary>
        /// <param name="squareMeter">The square kilometer area to convert.</param>
        /// <returns>The area in square mile.</returns>
        public static double SquareKilometerToSquareMile(double squareKilometer) => squareKilometer * Math.Pow(MILES_PER_KILOMETER, 2);

        /// <summary>
        /// Converts square foot to square meter.
        /// </summary>
        /// <param name="squareMeter">The square foot area to convert.</param>
        /// <returns>The area in square meter.</returns>
        public static double SquareFootToSquareMeter(double squareFoot) => squareFoot * Math.Pow(1 / FEET_PER_METER, 2);

        /// <summary>
        /// Converts square foot to square kilometer.
        /// </summary>
        /// <param name="squareMeter">The square foot area to convert.</param>
        /// <returns>The area in square kilometer.</returns>
        public static double SquareFootToSquareKilometer(double squareFoot) => squareFoot * Math.Pow(1 / FEET_PER_METER / 1000, 2);

        /// <summary>
        /// Converts square foot to square mile.
        /// </summary>
        /// <param name="squareMeter">The square foot area to convert.</param>
        /// <returns>The area in square mile.</returns>
        public static double SquareFootToSquareMile(double squareFoot) => squareFoot * Math.Pow(1 / FEET_PER_MILE, 2);

        /// <summary>
        /// Converts square mile to square meter.
        /// </summary>
        /// <param name="squareMeter">The square mile area to convert.</param>
        /// <returns>The area in square meter.</returns>
        public static double SquareMileToSquareMeter(double squareMile) => squareMile * Math.Pow(1 / MILES_PER_KILOMETER, 2);

        /// <summary>
        /// Converts square mile to square kilometer.
        /// </summary>
        /// <param name="squareMeter">The square mile area to convert.</param>
        /// <returns>The area in square kilometer.</returns>
        public static double SquareMileToSquareKilometer(double squareMile) => squareMile * Math.Pow(MILES_PER_KILOMETER, 2);

        /// <summary>
        /// Converts square mile to square foot.
        /// </summary>
        /// <param name="squareMeter">The square mile area to convert.</param>
        /// <returns>The area in square foot.</returns>
        public static double SquareMileToSquareFoot(double squareMile) => squareMile * Math.Pow(FEET_PER_MILE, 2);

        #endregion

        #endregion
    }
}
