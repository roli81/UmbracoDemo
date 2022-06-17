using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmbracoDemo.TestProject.Models.Widgets
{
    public interface IWidget
    {
        string Title { get;  }
        bool IsCollapsible { get; }

        /// <summary>
        /// render method with default implementation
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        Task<IHtmlContent> RenderModule(IHtmlHelper helper)
        {
            return Task.FromResult<IHtmlContent>(new HtmlString("No Rendering for Module"));
        }
    }
}
