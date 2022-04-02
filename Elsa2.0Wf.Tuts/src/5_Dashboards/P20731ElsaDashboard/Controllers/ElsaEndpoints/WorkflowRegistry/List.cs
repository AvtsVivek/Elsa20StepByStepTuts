using AutoMapper;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Persistence.Specifications;
using Elsa.Persistence.Specifications.WorkflowDefinitions;
using Elsa.Serialization;
//using Elsa.Server.Api.Endpoints.WorkflowDefinitions;
//using Elsa.Server.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using P20731ElsaDashboard.Infra;
using P20731ElsaDashboard.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace P20731ElsaDashboard.Controllers.ElsaEndpoints.WorkflowRegistry
{
    [ApiController]
    //[ApiVersion("1")]
    //[Route("v{apiVersion:apiVersion}/workflow-definitions")]
    [Route("v1/workflow-registry")]
    [Produces("application/json")]
    public class List : Controller
    {
        private readonly IConfiguration _configuration;

        public List(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<WorkflowDefinitionSummaryModel>))]
        //public async Task<ActionResult<PagedList<WorkflowDefinitionSummaryModel>>> Handle_old(int? page = default, int? pageSize = default, VersionOptions? version = default, CancellationToken cancellationToken = default)
        //{
        //    version ??= VersionOptions.Latest;
        //    var specification = new VersionOptionsSpecification(version.Value);
        //    var totalCount = await _workflowDefinitionStore.CountAsync(specification, cancellationToken);
        //    var paging = page == null || pageSize == null ? default : Paging.Page(page.Value, pageSize.Value);
        //    var items = await _workflowDefinitionStore.FindManyAsync(specification, paging: paging, cancellationToken: cancellationToken);
        //    var summaries = _mapper.Map<IList<WorkflowDefinitionSummaryModel>>(items);
        //    var pagedList = new PagedList<WorkflowDefinitionSummaryModel>(summaries, page, pageSize, totalCount);
        //    var jsonResult = Json(pagedList, _serializer.GetSettings());
        //    return jsonResult;
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<WorkflowDefinitionSummaryModel>))]
        //[SwaggerResponseExample(StatusCodes.Status200OK, typeof(WorkflowDefinitionPagedListExample))]
        //[SwaggerOperation(
        //    Summary = "Returns a list of workflow definitions.",
        //    Description = "Returns paginated a list of workflow definition summaries. When no version options are specified, the latest versions are returned.",
        //    OperationId = "WorkflowDefinitions.List",
        //    Tags = new[] { "WorkflowDefinitions" })
        //]
        public async Task<ActionResult<PagedList<WorkflowBlueprintSummaryModel>>> Handle(int? page = default, int? pageSize = default, VersionOptions? version = default, CancellationToken cancellationToken = default)
        {
            var elsaApiUrl = _configuration.GetValue<string>("Elsa:Server:BaseUrl");
            var client = new HttpClient();
            client.BaseAddress = new Uri(elsaApiUrl);
            var query = new Dictionary<string, string>
            {
                ["page"] = page.ToString()!,
                ["pageSize"] = pageSize.ToString()!,
                ["version"] = version.ToString()!
            };

            var endpointString = QueryHelpers.AddQueryString("/v1/workflow-registry/", query!);
            var response = await client.GetAsync(endpointString);
            var jsonResponseString = await response.Content.ReadAsStringAsync();
            var workflowDefs = JsonConvert.DeserializeObject<PagedList<WorkflowBlueprintSummaryModel>>(jsonResponseString);
            var jsonResult_New = Json(workflowDefs);
            return jsonResult_New;
        }
    }
}