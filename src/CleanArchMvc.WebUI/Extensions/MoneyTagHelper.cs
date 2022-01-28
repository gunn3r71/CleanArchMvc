using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace CleanArchMvc.WebUI.Extensions
{
    public class MoneyTagHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            var content = await output.GetChildContentAsync();
            var target = $"R$ {content.GetContent()}";

            output.Content.SetContent(target);
        }
    }
}