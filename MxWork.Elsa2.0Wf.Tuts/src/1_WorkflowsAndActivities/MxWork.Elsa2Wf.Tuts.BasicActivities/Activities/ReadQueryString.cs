using Elsa.ActivityResults;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Http;

namespace MxWork.Elsa2Wf.Tuts.BasicActivities.Activities
{
    public class ReadQueryString : Activity
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ReadQueryString(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override IActivityExecutionResult OnExecute(ActivityExecutionContext context)
        {
            var query = _httpContextAccessor.HttpContext!.Request.Query;

            return Done(query);
        }
    }
}
