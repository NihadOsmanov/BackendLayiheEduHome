#pragma checksum "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1a386401b6832fbed15cc95c7c8cdc6b4a48201c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Footer_default), @"mvc.1.0.view", @"/Views/Shared/Components/Footer/default.cshtml")]
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
#line 1 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\_ViewImports.cshtml"
using Layihe;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\_ViewImports.cshtml"
using Layihe.ViewModels;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\_ViewImports.cshtml"
using Layihe.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1a386401b6832fbed15cc95c7c8cdc6b4a48201c", @"/Views/Shared/Components/Footer/default.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"976e314ee378bd27c74f5063e9d89ffcb4387865", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Footer_default : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LayoutViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Course", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "About", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<footer class=""footer-area"">
    <div class=""main-footer"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-md-4 col-sm-6 col-xs-12"">
                    <div class=""single-widget pr-60"">
                        <div class=""footer-logo pb-25"">
                            <a href=""index.html""><img");
            BeginWriteAttribute("src", " src=\"", 375, "\"", 411, 2);
            WriteAttributeValue("", 381, "img/logo/", 381, 9, true);
#nullable restore
#line 10 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
WriteAttributeValue("", 390, Model.Bio.SecondLogo, 390, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" alt=""eduhome""></a>
                        </div>
                        <p>I must explain to you how all this mistaken idea of denoung pleure and praising pain was born and give you a coete account of the system. </p>
                        <div class=""footer-social"">
                            <ul>
                                <li><a");
            BeginWriteAttribute("href", " href=\"", 761, "\"", 790, 1);
#nullable restore
#line 15 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
WriteAttributeValue("", 768, Model.Bio.FacebookUrl, 768, 22, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"zmdi zmdi-facebook\"></i></a></li>\r\n                                <li><a");
            BeginWriteAttribute("href", " href=\"", 875, "\"", 905, 1);
#nullable restore
#line 16 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
WriteAttributeValue("", 882, Model.Bio.PinterestUrl, 882, 23, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"zmdi zmdi-pinterest\"></i></a></li>\r\n                                <li><a");
            BeginWriteAttribute("href", " href=\"", 991, "\"", 1017, 1);
#nullable restore
#line 17 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
WriteAttributeValue("", 998, Model.Bio.VimeoUrl, 998, 19, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><i class=\"zmdi zmdi-vimeo\"></i></a></li>\r\n                                <li><a");
            BeginWriteAttribute("href", " href=\"", 1099, "\"", 1127, 1);
#nullable restore
#line 18 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
WriteAttributeValue("", 1106, Model.Bio.TwitterUrl, 1106, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@"><i class=""zmdi zmdi-twitter""></i></a></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class=""col-md-3 col-sm-6 col-xs-12"">
                    <div class=""single-widget"">
                        <h3>information</h3>
                        <ul>
                            <li><a href=""#"">addmission</a></li>
                            <li><a href=""#"">Academic Calender</a></li>
                            <li><a href=""event.html"">Event List</a></li>
                            <li><a href=""#"">Hostel &amp; Dinning</a></li>
                            <li><a href=""#"">TimeTable</a></li>
                        </ul>
                    </div>
                </div>
                <div class=""col-md-2 col-sm-6 col-xs-12"">
                    <div class=""single-widget"">
                        <h3>useful links</h3>
                        <ul>
                            <li>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1a386401b6832fbed15cc95c7c8cdc6b4a48201c8306", async() => {
                WriteLiteral("our courses");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</li>\r\n                            <li>");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "1a386401b6832fbed15cc95c7c8cdc6b4a48201c9709", async() => {
                WriteLiteral("about us");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"</li>
                            <li><a href=""teacher.html"">teachers &amp; faculty</a></li>
                            <li><a href=""#"">teams &amp; conditions</a></li>
                            <li><a href=""event.html"">our events</a></li>
                        </ul>
                    </div>
                </div>
                <div class=""col-md-3 col-sm-6 col-xs-12"">
                    <div class=""single-widget"">
                        <h3>get in touch</h3>
                        <p>");
#nullable restore
#line 50 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
                      Write(Html.Raw(Model.Contact.Adress));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p>");
#nullable restore
#line 51 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
                      Write(Html.Raw(Model.Contact.SecondNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p>");
#nullable restore
#line 52 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
                      Write(Model.Contact.FirstEmail);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p>");
#nullable restore
#line 53 "C:\Users\ASUS\Desktop\BackendLayiheEduHome\Layihe\Layihe\Views\Shared\Components\Footer\default.cshtml"
                      Write(Model.Contact.SecondEmail);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""footer-bottom text-center"">
        <div class=""container"">
            <div class=""row"">
                <div class=""col-xs-12"">
                    <p>Copyright © <a href=""#"" target=""_blank"">HasTech</a> 2017. All Right Reserved By Hastech.</p>
                </div>
            </div>
        </div>
    </div>
</footer>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LayoutViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
