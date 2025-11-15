using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LearningPhase2.AppCode
{
    public class LoginCheckFilter : IPageFilter
    {
        public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }
        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var httpContext = context.HttpContext;
            var user = httpContext.User;

            // Ensure required session values are set
            var companyId = httpContext.Session.GetInt32("CompanyId");
            var employeeId = httpContext.Session.GetInt32("EmployeeId");

            bool isAuthenticated = user?.Identity?.IsAuthenticated ?? false;

            if (!isAuthenticated || companyId == null || employeeId == null)
            {
                // Optional: log a warning or handle edge cases
                context.Result = new RedirectToPageResult("/Account/Login");
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }

    }
}

