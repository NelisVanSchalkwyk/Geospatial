using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geospatial.Formats
{
    public class DegreesMinutesSeconds
    {
        public DegreesMinutesSeconds(double value)
        {
            
        }

        public static explicit operator DegreesMinutesSeconds(Coordinate coordinate)
        {
            throw new NotImplementedException();
        }
    }
}
