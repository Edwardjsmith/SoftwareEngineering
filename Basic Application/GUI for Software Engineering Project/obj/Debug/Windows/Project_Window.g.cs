﻿#pragma checksum "..\..\..\Windows\Project_Window.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "48AF8271238505135A0AC90DA2886699377546B4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GUI_for_Software_Engineering_Project.GUI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace GUI_for_Software_Engineering_Project.GUI {
    
    
    /// <summary>
    /// Project_Window
    /// </summary>
    public partial class Project_Window : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Windows\Project_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxProjectSelection;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\Windows\Project_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtbxSearchBar;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Windows\Project_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lbProjectFiles;
        
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
            System.Uri resourceLocater = new System.Uri("/GUI for Software Engineering Project;component/windows/project_window.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\Project_Window.xaml"
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
            this.cbxProjectSelection = ((System.Windows.Controls.ComboBox)(target));
            
            #line 10 "..\..\..\Windows\Project_Window.xaml"
            this.cbxProjectSelection.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbxProjectSelection_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\Windows\Project_Window.xaml"
            this.cbxProjectSelection.ContextMenuClosing += new System.Windows.Controls.ContextMenuEventHandler(this.CbxProjectSelection_ContextMenuClosing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtbxSearchBar = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.lbProjectFiles = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

