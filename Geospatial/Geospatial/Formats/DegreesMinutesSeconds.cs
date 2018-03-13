using Common;
using System;

namespace Geospatial.Formats
{
    public sealed class DegreesMinutesSeconds : Equatable<DegreesMinutesSeconds>, IComparable<DegreesMinutesSeconds>
    {
        #region Fields

        readonly double m_Value;
        readonly double m_Degrees;
        readonly double m_Minutes;
        readonly double m_Seconds;
        readonly char m_Direction;

        #endregion

        #region Constructors

        public DegreesMinutesSeconds(double value, bool isLongitude = false)
        {
            m_Value = value;
            m_Degrees = Math.Floor(Math.Abs(value));
            m_Minutes = Math.Floor(60 * (Math.Abs(value) - m_Degrees));
            m_Seconds = 3600 * (Math.Abs(value) - m_Degrees) - 60 * m_Minutes;
            m_Direction = isLongitude ? (value < 0) ? 'W' : 'E' : (value < 0) ? 'S' : 'N';
        }

        #endregion

        #region Public Methods

        public override string ToString() => $"{m_Degrees:N0}° {m_Minutes:N0}' {m_Seconds:N4}\" {m_Direction}";

        #region Equatable Implementation

        public override int GetHashCode()
        {
            return CreateHash(m_Value);
        }

        #endregion

        #region IComparable Implementation

        public int CompareTo(DegreesMinutesSeconds other)
        {
            if (other == null)
            {
                return 1;
            }

            return m_Value.CompareTo(other.m_Value);
        }

        public static bool operator >(DegreesMinutesSeconds operand1, DegreesMinutesSeconds operand2) => operand1.CompareTo(operand2) == 1;

        public static bool operator <(DegreesMinutesSeconds operand1, DegreesMinutesSeconds operand2) => operand1.CompareTo(operand2) == -1;

        public static bool operator >=(DegreesMinutesSeconds operand1, DegreesMinutesSeconds operand2) => operand1.CompareTo(operand2) >= 0;

        public static bool operator <=(DegreesMinutesSeconds operand1, DegreesMinutesSeconds operand2) => operand1.CompareTo(operand2) <= 0;

        #endregion

        #endregion
    }
}
