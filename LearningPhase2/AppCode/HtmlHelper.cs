using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LearningPhase2.AppCode
{
    public static class IHtmlHelpers
    {
        public static HtmlString DisplayEmployeeStatus(this IHtmlHelper helper, string status)
        {
            if (status == Constants.Active)
            {
                return new HtmlString($"<span class= 'badge bg-success'>{status}</span>");
            }
            else
            {
                return new HtmlString($"<span class='badge bg-danger'> {status} </span>");
            }
        }
        public static HtmlString DisplayDate(this IHtmlHelper helper, DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return new HtmlString(String.Empty);
            }
            else
            {
                return new HtmlString(date.ToString("MMM dd, yyyy"));
            }
        }
    }
}
