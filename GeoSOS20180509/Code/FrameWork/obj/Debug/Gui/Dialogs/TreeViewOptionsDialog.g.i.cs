﻿#pragma checksum "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "13D0775579F356523364227C10509668"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using ICSharpCode.Core.Presentation;
using ICSharpCode.SharpDevelop.Widgets;
using Microsoft.Windows.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FrameWork.Gui {
    
    
    /// <summary>
    /// TreeViewOptionsDialog
    /// </summary>
    public partial class TreeViewOptionsDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView treeView;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer optionPanelScrollViewer;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock optionPanelTitle;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentPresenter optionPanelContent;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button okButton;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FrameWork;component/gui/dialogs/treeviewoptionsdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.treeView = ((System.Windows.Controls.TreeView)(target));
            return;
            case 2:
            this.optionPanelScrollViewer = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 3:
            this.optionPanelTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.optionPanelContent = ((System.Windows.Controls.ContentPresenter)(target));
            return;
            case 5:
            this.okButton = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
            this.okButton.Click += new System.Windows.RoutedEventHandler(this.okButtonClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cancelButton = ((System.Windows.Controls.Button)(target));
            
            #line 79 "..\..\..\..\Gui\Dialogs\TreeViewOptionsDialog.xaml"
            this.cancelButton.Click += new System.Windows.RoutedEventHandler(this.cancelButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

