// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Windows;

namespace FrameWork.Gui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    partial class App : Application
    {
        public App()
        {
            /* Default Theme 缺省的主题 */
            string uri = "Themes/aero.normalcolor.xaml";
            AvalonDock.ThemeFactory.ChangeTheme(new Uri(uri, UriKind.RelativeOrAbsolute));

            InitializeComponent();
        }
    }
}