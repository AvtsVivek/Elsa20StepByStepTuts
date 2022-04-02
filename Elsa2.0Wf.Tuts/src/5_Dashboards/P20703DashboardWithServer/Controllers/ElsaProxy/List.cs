using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Elsa.Models;
using Elsa.Persistence;
using Elsa.Persistence.Specifications;
using Elsa.Persistence.Specifications.WorkflowDefinitions;
using Elsa.Serialization;
using Elsa.Server.Api.Models;
using Elsa.Server.Api.Swagger.Examples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Elsa.Server.Api.Endpoints.WorkflowDefinitions
{
    [ApiController]
    [ApiVersion("1")]
    [Route("temp/v{apiVersion:apiVersion}/workflow-definitions")]
    [Produces("application/json")]
    public class List : Controller
    {
        private readonly IWorkflowDefinitionStore _workflowDefinitionStore;
        private readonly IContentSerializer _serializer;
        private readonly IMapper _mapper;

        public List(IWorkflowDefinitionStore workflowDefinitionStore, IContentSerializer serializer, IMapper mapper)
        {
            _workflowDefinitionStore = workflowDefinitionStore;
            _serializer = serializer;
            _mapper = mapper;
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
        public async Task<ActionResult<string>> Handle(int? page = default, int? pageSize = default, VersionOptions? version = default, CancellationToken cancellationToken = default)
        {
            var client = new HttpClient();
            var requestUrl = $"{Request.Scheme}://{Request.Host.Value}/";
            client.BaseAddress = new Uri(requestUrl);
            var response = await client.GetAsync("v1/workflow-definitions");
            var workflowDefs = JsonConvert.DeserializeObject<PagedList<WorkflowDefinitionSummaryModel>>(await response.Content.ReadAsStringAsync());
            var jsonResult_New = Json(workflowDefs, _serializer.GetSettings());
            return jsonResult_New;
        }
    }
}