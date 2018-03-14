using System.Collections.Generic;

namespace Geospatial.Conversions
{
    internal class HelmertTransform
    {
        //Metres
        double m_Tx { get; set; }
        double m_Ty { get; set; }
        double m_Tz { get; set; }

        //Seconds
        double m_Rx { get; set; }
        double m_Ry { get; set; }
        double m_Rz { get; set; }

        //Parts per million
        double m_S { get; set; }

        CoordinateSystems m_OutputCoordinateSystem { get; set; }

        private HelmertTransform(double tx, double ty, double tz, double rx, double ry, double rz, double s, CoordinateSystems outputCoordinateSystem)
        {
            m_Tx = tx;
            m_Ty = ty;
            m_Tz = tz;
            m_Rx = rx;
            m_Ry = ry;
            m_Rz = rz;
            m_S = s;
            m_OutputCoordinateSystem = outputCoordinateSystem;
        }

        public static HelmertTransform GetTransform(HelmertTransformType type)
        {
            return m_Transforms[type];
        }

        private static Dictionary<HelmertTransformType, HelmertTransform> m_Transforms = new Dictionary<HelmertTransformType, HelmertTransform>
        {
           {HelmertTransformType.WGS84toOSGB36, new HelmertTransform(-446.448,125.157,-542.060,-0.1502,-0.2470,-0.8421,20.4894,CoordinateSystems.OSGB36)},
            {HelmertTransformType.OSGB36toWGS84, new HelmertTransform(446.448,-125.157,542.060,0.1502,0.2470,0.8421,-20.4894,CoordinateSystems.WGS84)}
        };
    }
}
