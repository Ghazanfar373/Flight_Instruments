using OSGeo.GDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopographySRTM
{
    internal class GdalConfigurator
    {

        public GdalConfigurator()
        {
            Gdal.SetConfigOption("GDAL_DATA", "C:\\Users\\Ghazanfar\\Music\\data");
            Gdal.SetConfigOption("GDAL_DRIVER_PATH", "C:\\Users\\Ghazanfar\\Music\\data");
            Gdal.AllRegister();
        }

    }
}
