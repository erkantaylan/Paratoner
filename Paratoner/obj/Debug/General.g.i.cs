﻿#pragma checksum "..\..\General.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B14B69DD466C21672A5A65C5BC861D46"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using Paratoner.UserControls;
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


namespace Paratoner {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnInvoiceOperations;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGroupOperations;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnUserList;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSettings;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbxGroupList;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtPrice;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtProduct;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddProduct;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbxProductList;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddInvoice;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\General.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdOptions;
        
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
            System.Uri resourceLocater = new System.Uri("/Paratoner;component/general.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\General.xaml"
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
            this.btnInvoiceOperations = ((System.Windows.Controls.Button)(target));
            return;
            case 2:
            this.btnGroupOperations = ((System.Windows.Controls.Button)(target));
            return;
            case 3:
            this.btnUserList = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.btnSettings = ((System.Windows.Controls.Button)(target));
            
            #line 68 "..\..\General.xaml"
            this.btnSettings.Click += new System.Windows.RoutedEventHandler(this.btnSettings_OnClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.cbxGroupList = ((System.Windows.Controls.ComboBox)(target));
            
            #line 100 "..\..\General.xaml"
            this.cbxGroupList.Loaded += new System.Windows.RoutedEventHandler(this.cbxGroupList_OnLoaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtPrice = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtProduct = ((System.Windows.Controls.TextBox)(target));
            
            #line 123 "..\..\General.xaml"
            this.txtProduct.KeyUp += new System.Windows.Input.KeyEventHandler(this.txtProduct_OnKeyUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnAddProduct = ((System.Windows.Controls.Button)(target));
            
            #line 126 "..\..\General.xaml"
            this.btnAddProduct.Click += new System.Windows.RoutedEventHandler(this.btnAddProduct_OnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.lbxProductList = ((System.Windows.Controls.ListBox)(target));
            return;
            case 10:
            this.btnAddInvoice = ((System.Windows.Controls.Button)(target));
            
            #line 159 "..\..\General.xaml"
            this.btnAddInvoice.Click += new System.Windows.RoutedEventHandler(this.btnAddInvoice_OnClick);
            
            #line default
            #line hidden
            return;
            case 11:
            this.grdOptions = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

