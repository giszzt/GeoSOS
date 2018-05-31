using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.GDAL.SpatialQuery
{
    public enum SpatialRel
    {
        Undefined = 0,
        Intersects = 1,
        Overlaps = 2,
        Contains = 3,
        Crosses = 4,
        WithIn = 5,
        Touches= 6
    }
}
