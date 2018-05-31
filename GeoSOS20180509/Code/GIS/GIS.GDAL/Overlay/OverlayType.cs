using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GIS.GDAL.Overlay
{
    public enum OverlayType
    {
        Undefined = 0,
        Intersects = 1,
        Union = 2,
        SymDifference = 3,
        Identity = 4,
        Update = 5,
        Clip = 6,
        Erase = 7
    }
}
