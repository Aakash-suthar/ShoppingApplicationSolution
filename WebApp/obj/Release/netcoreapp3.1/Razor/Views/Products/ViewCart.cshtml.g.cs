#pragma checksum "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\Products\ViewCart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f19d2b1b8a93c9dfcba0285cabeabdf377bbb7dc"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Products_ViewCart), @"mvc.1.0.view", @"/Views/Products/ViewCart.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\_ViewImports.cshtml"
using WebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\_ViewImports.cshtml"
using WebApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f19d2b1b8a93c9dfcba0285cabeabdf377bbb7dc", @"/Views/Products/ViewCart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc48f17eb9bac3476d8060730298bf398eb2fa5e", @"/Views/_ViewImports.cshtml")]
    public class Views_Products_ViewCart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebApp.Models.Cart>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Payment", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\Products\ViewCart.cshtml"
  
    ViewData["Title"] = "ViewCart";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>ViewCart</h1>\r\n<br />\r\n<br />\r\n\r\n<div class=\"col-lg-12\">\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 13 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\Products\ViewCart.cshtml"
         foreach (var item in ViewBag.cartlist)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-lg-3\" style=\"margin:5px;background-color:palegreen\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f19d2b1b8a93c9dfcba0285cabeabdf377bbb7dc4306", async() => {
                WriteLiteral("\r\n                    <input type=\"hidden\" name=\"counter\"");
                BeginWriteAttribute("value", " value=\"", 447, "\"", 463, 1);
#nullable restore
#line 17 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\Products\ViewCart.cshtml"
WriteAttributeValue("", 455, item.id, 455, 8, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n");
                WriteLiteral("\r\n                    <a style=\" all:unset;cursor:pointer\">\r\n                        <div style=\"text-align:center;\">\r\n                            <h4 style=\"padding-top:20px\">\r\n                                Name :  ");
#nullable restore
#line 25 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\Products\ViewCart.cshtml"
                                   Write(item.product.Productname);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </h4>\r\n                        </div>\r\n                        <div style=\"text-align:center;\">\r\n                            <h5 style=\"padding-top:20px\">\r\n                                Price : ");
#nullable restore
#line 30 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\Products\ViewCart.cshtml"
                                   Write(item.totalprice);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                            </h5>\r\n                        </div>\r\n                        <div style=\"text-align:center;\">\r\n                            <h5 style=\"padding-top:20px\">\r\n                                Quantity :  ");
#nullable restore
#line 35 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\Products\ViewCart.cshtml"
                                       Write(item.Quantitys);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                            </h5>
                        </div>
                        <div style=""text-align:center;padding-bottom:10px;"">
                            <input type=""submit"" value=""Order Now"" class=""btn btn-primary"" />
                        </div>
                    </a>

                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n");
#nullable restore
#line 45 "C:\Users\Vishal-PC\Desktop\ShoppingApplicationSolution\WebApp\Views\Products\ViewCart.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </div>\r\n\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApp.Models.Cart> Html { get; private set; }
    }
}
#pragma warning restore 1591