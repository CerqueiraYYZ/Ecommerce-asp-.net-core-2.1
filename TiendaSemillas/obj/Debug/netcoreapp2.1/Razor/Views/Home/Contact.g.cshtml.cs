#pragma checksum "G:\Version final Tienda Semillas\TiendaSemillas\TiendaSemillas\Views\Home\Contact.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "261653c77d74b083b4eed36fb99f9e5c6d1d7846"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Contact), @"mvc.1.0.view", @"/Views/Home/Contact.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/Contact.cshtml", typeof(AspNetCore.Views_Home_Contact))]
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
#line 1 "G:\Version final Tienda Semillas\TiendaSemillas\TiendaSemillas\Views\_ViewImports.cshtml"
using TiendaSemillas;

#line default
#line hidden
#line 2 "G:\Version final Tienda Semillas\TiendaSemillas\TiendaSemillas\Views\_ViewImports.cshtml"
using TiendaSemillas.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"261653c77d74b083b4eed36fb99f9e5c6d1d7846", @"/Views/Home/Contact.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3075d9b9c46f622d553f6c2cc77b0e645a91b1d9", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Contact : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 1 "G:\Version final Tienda Semillas\TiendaSemillas\TiendaSemillas\Views\Home\Contact.cshtml"
  
    ViewData["Title"] = "Contact";

#line default
#line hidden
            BeginContext(43, 4, true);
            WriteLiteral("<h2>");
            EndContext();
            BeginContext(48, 17, false);
#line 4 "G:\Version final Tienda Semillas\TiendaSemillas\TiendaSemillas\Views\Home\Contact.cshtml"
Write(ViewData["Title"]);

#line default
#line hidden
            EndContext();
            BeginContext(65, 11, true);
            WriteLiteral("</h2>\r\n<h3>");
            EndContext();
            BeginContext(77, 19, false);
#line 5 "G:\Version final Tienda Semillas\TiendaSemillas\TiendaSemillas\Views\Home\Contact.cshtml"
Write(ViewData["Message"]);

#line default
#line hidden
            EndContext();
            BeginContext(96, 275, true);
            WriteLiteral(@"</h3>

<address>
    Calle 25/4<br />
    Zaragoza, 200008<br />
    <abbr title=""Phone"">P:</abbr>
    653530809
</address>

<address>
    <strong>Soporte Tecnico:</strong> <a href=""mailto:SeñorSemillas@etienda.com"">Señorsemillas@etienda.com</a><br />
</address>
");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
