using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.Core;
using FrameWork.Gui;
using DotSpatial.Controls;
using DotSpatial.Symbology;


namespace GIS.AddIns.Attributes
{
    class ClearSelectionCommand: AbstractCommand
    {
        private IMap _map = GIS.FrameWork.Application.App.Map;
        public override void Run()
        {
            _map.ClearSelection();
        }
    }
}
