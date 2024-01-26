using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace C4PHub.Web.TagHelpers
{
    [HtmlTargetElement("generateCalendar",TagStructure=TagStructure.NormalOrSelfClosing)]
    public class GenerateCalendarTagHelper : TagHelper
    {
        [HtmlAttributeName("c4pId")]
        public string C4PId { get; set; }
        
        [HtmlAttributeName("year")] 
        public string Year { get; set; }

        [HtmlAttributeName("type")]
        public string Type { get; set; }

        [HtmlAttributeName("format")]
        public string Format { get; set; } = "Ics";


        private readonly HtmlEncoder _htmlEncoder;
        private readonly LinkGenerator _linkGenerator;

        public GenerateCalendarTagHelper(LinkGenerator linkGenerator, HtmlEncoder htmlEncoder)
        {
            _linkGenerator = linkGenerator;
            _htmlEncoder = htmlEncoder;
        }

        public override async void Process(TagHelperContext context, TagHelperOutput output)
        {
            await ProcessAsync(context, output);
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;

            context.AllAttributes.ToList()
                .Where(x => x.Name != "c4pId" && x.Name != "year" && x.Name != "type" && x.Name != "format")
                .ToList()
                .ForEach(x => output.Attributes.Add(x));

            var calendarUrl = _linkGenerator.GetPathByAction("Index", "Calendar");

            calendarUrl = $"{calendarUrl}?c4pId={Uri.UnescapeDataString(C4PId)}&year={Year}&type={Type}&format={Format}";
            
            output.Attributes.SetAttribute("href", calendarUrl);
            output.Content.SetHtmlContent(await output.GetChildContentAsync());

        }
    }
}
