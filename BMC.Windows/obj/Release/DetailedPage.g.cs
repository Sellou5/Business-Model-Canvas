﻿

#pragma checksum "D:\Assembly 5-1\Business Model Canvas\BMC\BMC\BMC.Windows\DetailedPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4065DF248BFCD039F75021AEAC824BEB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BMC
{
    partial class DetailedPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 67 "..\..\DetailedPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarButton_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 75 "..\..\DetailedPage.xaml"
                ((global::Windows.UI.Xaml.Controls.MediaElement)(target)).MediaOpened += this.OnMediaOpened;
                 #line default
                 #line hidden
                #line 76 "..\..\DetailedPage.xaml"
                ((global::Windows.UI.Xaml.Controls.MediaElement)(target)).MediaFailed += this.OnMediaFailed;
                 #line default
                 #line hidden
                #line 79 "..\..\DetailedPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DoubleTapped += this.YouTubePlayer_DoubleTapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 105 "..\..\DetailedPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Previousbtn_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 112 "..\..\DetailedPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Pausebtn_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 116 "..\..\DetailedPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Seekbtn_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 121 "..\..\DetailedPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.YouTubePlayer_DoubleTapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


