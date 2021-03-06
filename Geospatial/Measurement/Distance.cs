﻿using Common;
using System;
using System.Runtime.Serialization;

namespace Measurement
{
    /// <summary>
    /// This class represents a measurable distance expressed in meters, kilometers, feet and miles.
    /// </summary>
    /// <remarks>
    /// Setting a property will automatically calculate and set the other property values.  This was specifically
    /// done to assist with serialization.
    /// </remarks>
    [DataContract]
    public sealed class Distance : Equatable<Distance>, IComparable<Distance>
    {
        #region Fields

        double m_Meters;
        double m_Kilometers;
        double m_Feet;
        double m_Miles;

        #endregion

        #region Properties

        /// <summary>
        /// Sets and gets the meter(m) distance.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double Meters
        {
            get
            {
                return m_Meters;
            }
            set
            {
                m_Meters = value;
                m_Kilometers = UnitConverter.MetersToKilometers(value);
                m_Feet = UnitConverter.MetersToFeet(value);
                m_Miles = UnitConverter.MetersToMiles(value);
            }
        }

        /// <summary>
        /// Sets and gets the kilometer(km) distance.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double Kilometers
        {
            get
            {
                return m_Kilometers;
            }
            set
            {
                m_Kilometers = value;
                m_Meters = UnitConverter.KilometersToMeters(value);
                m_Feet = UnitConverter.KilometersToFeet(value);
                m_Miles = UnitConverter.KilometersToMiles(value);
            }
        }

        /// <summary>
        /// Sets and gets the feet(ft) distance.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double Feet
        {
            get
            {
                return m_Feet;
            }
            set
            {
                m_Feet = value;
                m_Meters = UnitConverter.FeetToMeters(value);
                m_Kilometers = UnitConverter.FeetToKilometers(value);
                m_Miles = UnitConverter.FeetToMiles(value);
            }
        }

        /// <summary>
        /// Sets and gets the mile(mi) distance.
        /// </summary>
        /// <remarks>Setting this property will automatically calculate and set the other property values.</remarks>
        [DataMember]
        public double Miles
        {
            get
            {
                return m_Miles;
            }
            set
            {
                m_Miles = value;
                m_Meters = UnitConverter.MilesToMeters(value);
                m_Kilometers = UnitConverter.MilesToKilometers(value);
                m_Feet = UnitConverter.MilesToFeet(value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Measurement.Distance"/> class.
        /// </summary>
        public Distance()
            : this(0D)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Measurement.Distance"/> class.
        /// </summary>
        /// <param name="meters">The distance in meters.</param>
        public Distance(double meters)
        {
            Meters = meters;
        }

        #endregion

        #region Public Methods

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return CreateHash(Meters);
        }

        #region IComparable Implementation

        public int CompareTo(Distance other)
        {
            if (other == null)
            {
                return 1;
            }

            return m_Meters.CompareTo(other.m_Meters);
        }

        public static bool operator ==(Distance operand1, Distance operand2) => operand1.Equals(operand2);

        public static bool operator !=(Distance operand1, Distance operand2) => !operand1.Equals(operand2);

        public static bool operator >(Distance operand1, Distance operand2) => operand1.CompareTo(operand2) == 1;

        public static bool operator <(Distance operand1, Distance operand2) => operand1.CompareTo(operand2) == -1;

        public static bool operator >=(Distance operand1, Distance operand2) => operand1.CompareTo(operand2) >= 0;

        public static bool operator <=(Distance operand1, Distance operand2) => operand1.CompareTo(operand2) <= 0;

        #endregion

        #endregion
    }
}
