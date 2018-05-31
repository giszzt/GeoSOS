using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using System.Windows.Forms;
using System.IO;

namespace GIS.AddIns.Ca
{
    class AddTiffCommand : AbstractCommand
    {
        public override void Run()
        {
            OpenFileDialog openTiffDialog = new OpenFileDialog();
            if (openTiffDialog.ShowDialog() == DialogResult.OK)
            {
                var tiffName = openTiffDialog.FileName;
                OSGeo.GDAL.Dataset tiffDataset = OSGeo.GDAL.Gdal.Open(tiffName, OSGeo.GDAL.Access.GA_ReadOnly);
                OSGeo.GDAL.Band band = tiffDataset.GetRasterBand(1);
                DotSpatial.Data.IRaster raster = GIS.GDAL.RasterConverter.Gdal2DSRaster(tiffDataset, 1);
                FileInfo fileInfo = new FileInfo(tiffName);
                raster.Name = fileInfo.Name;
                GIS.FrameWork.Application.App.Map.Layers.Add(raster);

            }
        }
    }
}
