#pragma checksum "..\..\..\UCWindowApp.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5D8FD1A10C6398BDB351981F77AC7307B0A8321F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Menu_v2;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace Menu_v2 {
    
    
    /// <summary>
    /// UCWindowApp
    /// </summary>
    public partial class UCWindowApp : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\UCWindowApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ThisPC;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\UCWindowApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ControlPanel;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\UCWindowApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RecycleBin;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\UCWindowApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DiskCleanup;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\UCWindowApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Spotify;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\UCWindowApp.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Photoshop;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Menu;component/ucwindowapp.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UCWindowApp.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ThisPC = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\UCWindowApp.xaml"
            this.ThisPC.Click += new System.Windows.RoutedEventHandler(this.ThisPC_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ControlPanel = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\UCWindowApp.xaml"
            this.ControlPanel.Click += new System.Windows.RoutedEventHandler(this.ControlPanel_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RecycleBin = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\UCWindowApp.xaml"
            this.RecycleBin.Click += new System.Windows.RoutedEventHandler(this.RecycleBin_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.DiskCleanup = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\UCWindowApp.xaml"
            this.DiskCleanup.Click += new System.Windows.RoutedEventHandler(this.DiskCleanup_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Spotify = ((System.Windows.Controls.Button)(target));
            
            #line 57 "..\..\..\UCWindowApp.xaml"
            this.Spotify.Click += new System.Windows.RoutedEventHandler(this.Spotify_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Photoshop = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\..\UCWindowApp.xaml"
            this.Photoshop.Click += new System.Windows.RoutedEventHandler(this.Photoshop_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

