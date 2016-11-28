using Common;
using System.Runtime.Serialization;

namespace Measurement
{
    /// <summary>
    /// This class represents a measurable speed expressed in kilometers per hour  and miles per hour.
    /// </summary>
    /// <remarks>
    /// Setting a property will automatically calculate and set the other property values.  This was specifically
    /// done to assist with serialization.
    /// </remarks>
    [DataContract]
    public sealed class Speed : Equatable<Speed>
    {
        #region Fields

        double m_KilometersPerHour = 0D;
        double m_MilesPerHour = 0D;

        #endregion

        #region Properties

        /// <summary>
        /// Sets and gets the kilometers per hour(km/h) speed.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double KilometersPerHour
        {
            get
            {
                return m_KilometersPerHour;
            }
            set
            {
                m_KilometersPerHour = value;
                m_MilesPerHour = UnitConverter.KilometersPerHourToMilesPerHour(value);
            }
        }

        /// <summary>
        /// Sets and gets the miles per hour(mph) speed.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double MilesPerHour
        {
            get
            {
                return m_MilesPerHour;
            }
            set
            {
                m_MilesPerHour = value;
                m_KilometersPerHour = UnitConverter.MilesPerHourToKilometersPerHour(value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Speed"/> class.
        /// </summary>
        /// <param name="meters">The distance in meters.</param>
        public Speed(double kilometersPerHour)
        {
            KilometersPerHour = kilometersPerHour;
        }

        #endregion

        #region Public Methods

        public override int GetHashCode()
        {
            return CreateHash(KilometersPerHour);
        }

        #endregion
    }
}
