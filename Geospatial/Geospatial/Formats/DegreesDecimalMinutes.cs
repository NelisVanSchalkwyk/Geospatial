using Common;
using System;

namespace Geospatial.Formats
{
    public sealed class DegreesDecimalMinutes : Equatable<DegreesDecimalMinutes>, IComparable<DegreesDecimalMinutes>
    {
        #region Fields

        double m_Value;
        double m_Degrees;
        double m_Minutes;
        char m_Direction;

        #endregion

        #region Constructors

        public DegreesDecimalMinutes(double value, bool isLongitude = false)
        {
            m_Value = value;
            m_Degrees = Math.Floor(Math.Abs(value));
            m_Minutes = 60 * (Math.Abs(value) - m_Degrees);
            m_Direction = isLongitude ? (value < 0) ? 'W' : 'E' : (value < 0) ? 'S' : 'N';
        }

        #endregion

        #region Public Methods

        public override string ToString() => $"{m_Degrees:N0}° {m_Minutes:N6}' {m_Direction}";

        #region Equatable Implementation

        public override int GetHashCode()
        {
            return CreateHash(m_Value);
        }

        #endregion

        #region IComparable Implementation

        public int CompareTo(DegreesDecimalMinutes other)
        {
            if (other == null)
            {
                return 1;
            }

            return m_Value.CompareTo(other.m_Value);
        }

        public static bool operator >(DegreesDecimalMinutes operand1, DegreesDecimalMinutes operand2) => operand1.CompareTo(operand2) == 1;

        public static bool operator <(DegreesDecimalMinutes operand1, DegreesDecimalMinutes operand2) => operand1.CompareTo(operand2) == -1;

        public static bool operator >=(DegreesDecimalMinutes operand1, DegreesDecimalMinutes operand2) => operand1.CompareTo(operand2) >= 0;

        public static bool operator <=(DegreesDecimalMinutes operand1, DegreesDecimalMinutes operand2) => operand1.CompareTo(operand2) <= 0;

        #endregion

        #endregion
    }
}
