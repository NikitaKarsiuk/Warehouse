﻿#pragma checksum "..\..\..\Views\RequestWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E7059002FE720D70AE6905E358FF64BD63BDC763"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TemplateProject.Views;


namespace TemplateProject.Views {
    
    
    /// <summary>
    /// RequestWindow
    /// </summary>
    public partial class RequestWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\Views\RequestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddButton;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Views\RequestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\Views\RequestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button PrintButton;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\RequestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button TtnButton;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Views\RequestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ContractorComboBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Views\RequestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox EmployeeComboBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Views\RequestWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid ProductDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/TemplateProject;component/views/requestwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\RequestWindow.xaml"
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
            
            #line 8 "..\..\..\Views\RequestWindow.xaml"
            ((TemplateProject.Views.RequestWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 10 "..\..\..\Views\RequestWindow.xaml"
            ((System.Windows.Controls.StackPanel)(target)).Loaded += new System.Windows.RoutedEventHandler(this.StackPanel_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.AddButton = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\Views\RequestWindow.xaml"
            this.AddButton.Click += new System.Windows.RoutedEventHandler(this.AddButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\Views\RequestWindow.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PrintButton = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.TtnButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.ContractorComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.EmployeeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.ProductDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 29 "..\..\..\Views\RequestWindow.xaml"
            this.ProductDataGrid.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.ProductDataGrid_CellEditEnding);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
