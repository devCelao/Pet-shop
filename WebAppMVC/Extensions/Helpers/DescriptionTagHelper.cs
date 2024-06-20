using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel;

namespace WebAppMVC.Extensions.Helpers;

[HtmlTargetElement("label", Attributes = ForAttributeName)]
public class DescriptionTagHelper : TagHelper
{
    private const string ForAttributeName = "asp-for";
    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression For { get; set; }

    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var descption = GetDescription();

        output.Content.SetContent(descption);

        return Task.CompletedTask;
    }

    private string GetDescription()
    {
        try
        {
            string propertyName = ReturnsFieldName();
            var type = Type.GetType(ReturnsFullName());

            if (type == null)  return propertyName;

            var property = type.GetProperty(propertyName);

            if (property == null) return propertyName;

            var description = (DescriptionAttribute)property
                                                       .GetCustomAttributes(typeof(DescriptionAttribute), false)
                                                       .FirstOrDefault();

            return description?.Description ?? propertyName;
        }
        catch
        {
            return ReturnsFieldName();
        }
    }


    private string GetFirstPartBeforDot()
    {
        string input = For.Metadata.ContainerMetadata.ModelType.FullName;

        if (String.IsNullOrEmpty(input)) return input;

        var parts = input.Split('.');

        return parts.Length > 0 ? parts[0] : input;
    }

    private string ReturnsFullName()
    {
        return $"{For.Metadata.ContainerMetadata.ModelType.FullName}, {GetFirstPartBeforDot()}";
    }
    private string ReturnsFieldName()
    {
        return For.Name;
    }
}
