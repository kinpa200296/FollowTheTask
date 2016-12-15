using System.Collections.Generic;
using System.Web.Mvc;

namespace FollowTheTask.Web.Infrastructure
{
    public static class DictSelectHelper
    {
        public static MvcHtmlString DictionarySelect(this HtmlHelper html, Dictionary<int, string> dict,
            string fieldName)
        {
            var select = new TagBuilder("select");
            select.MergeAttribute("id", fieldName);
            select.MergeAttribute("name", fieldName);
            select.MergeAttribute("class", "form-control");
            var options = "";
            foreach (var key in dict.Keys)
            {
                var value = key == 0 ? null : (int?)key;
                var option = new TagBuilder("option");
                option.MergeAttribute("value", value.ToString());
                option.SetInnerText(dict[key]);
                options += option.ToString(TagRenderMode.Normal) + "\n";
            }
            select.InnerHtml = options;
            return new MvcHtmlString(select.ToString(TagRenderMode.Normal));
        }
    }
}