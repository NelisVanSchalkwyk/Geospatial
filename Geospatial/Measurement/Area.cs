using Common;
using System;
using System.Runtime.Serialization;

namespace Measurement
{
    /// <summary>
    /// This class represents a measurable area expressed in square meters, square kilometers, square feet and square miles.
    /// </summary>
    /// <remarks>
    /// Setting a property will automatically calculate and set the other property values.  This was specifically
    /// done to assist with serialization.
    /// </remarks>
    [DataContract]
    public sealed class Area : Equatable<Area>, IComparable<Area>
    {
        #region Fields

        double m_SquareMeter = 0D;
        double m_SquareKilometer = 0D;
        double m_SquareFoot = 0D;
        double m_SquareMile = 0D;

        #endregion

        #region Properties

        /// <summary>
        /// Sets and gets the square meter(m^2) area.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double SquareMeter
        {
            get
            {
                return m_SquareMeter;
            }
            set
            {
                m_SquareMeter = value;
                m_SquareKilometer = UnitConverter.SquareMeterToSquareKilometer(value);
                m_SquareFoot = UnitConverter.SquareMeterToSquareFoot(value);
                m_SquareMile = UnitConverter.SquareMeterToSquareMile(value);
            }
        }

        /// <summary>
        /// Sets and gets the square kilometer(km^2) area.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double SquareKilometer
        {
            get
            {
                return m_SquareKilometer;
            }
            set
            {
                m_SquareKilometer = value;
                m_SquareMeter = UnitConverter.SquareKilometerToSquareMeter(value);
                m_SquareFoot = UnitConverter.SquareKilometerToSquareFoot(value);
                m_SquareMile = UnitConverter.SquareKilometerToSquareMile(value);
            }
        }

        /// <summary>
        /// Sets and gets the square foot(ft^2) area.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double SquareFoot
        {
            get
            {
                return m_SquareFoot;
            }
            set
            {
                m_SquareFoot = value;
                m_SquareMeter = UnitConverter.SquareFootToSquareMeter(value);
                m_SquareKilometer = UnitConverter.SquareFootToSquareKilometer(value);
                m_SquareMile = UnitConverter.SquareFootToSquareMile(value);
            }
        }

        /// <summary>
        /// Sets and gets the square mile(mi^2) area.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double SquareMile
        {
            get
            {
                return m_SquareMile;
            }
            set
            {
                m_SquareMile = value;
                m_SquareMeter = UnitConverter.SquareMileToSquareMeter(value);
                m_SquareKilometer = UnitConverter.SquareMileToSquareKilometer(value);
                m_SquareFoot = UnitConverter.SquareMileToSquareFoot(value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Area"/> class.
        /// </summary>
        public Area()
            : this(0D)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Area"/> class.
        /// </summary>
        /// <param name="squareMeters">The area in square meters.</param>
        public Area(double squareMeters)
        {
            SquareMeter = squareMeters;
        }

        #endregion

        #region Public Methods

        public override int GetHashCode()
        {
            return CreateHash(SquareMeter);
        }

        #region IComparable Implementation

        public int CompareTo(Area other)
        {
            if (other == null)
            {
                return 1;
            }

            return m_SquareMeter.CompareTo(other.m_SquareMeter);
        }

        public static bool operator >(Area operand1, Area operand2) => operand1.CompareTo(operand2) == 1;

        public static bool operator <(Area operand1, Area operand2) => operand1.CompareTo(operand2) == -1;

        public static bool operator >=(Area operand1, Area operand2) => operand1.CompareTo(operand2) >= 0;

        public static bool operator <=(Area operand1, Area operand2) => operand1.CompareTo(operand2) <= 0;

        #endregion

        #endregion
    }
}
