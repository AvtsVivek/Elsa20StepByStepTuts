using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
//using AbpAppTmpltMvcPvt.Models.ElsaApi;
using Elsa.Models;
using Elsa.Serialization;
//using Elsa.Server.Api.Endpoints.WorkflowRegistry;
//using Elsa.Server.Api.Models;
//using Elsa.Server.Api.Services;
//using Elsa.Server.Api.Swagger.Examples;
using Elsa.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using P20731ElsaDashboard.Infra;
using P20731ElsaDashboard.Models;
//using P20731ElsaDashboard.Models;
//using Swashbuckle.AspNetCore.Annotations;
//using Swashbuckle.AspNetCore.Filters;

namespace P20731ElsaDashboard.Controllers.ElsaEndpoints.WorkflowRegistry
//namespace AbpAppTmpltMvcPvt.Controllers.ElsaEndpoints.WorkflowRegistry
//namespace Elsa.Server.Api.Endpoints.WorkflowRegistry
{
    [ApiController]
    //    [ApiVersion("1")]
    //    [Route("v{apiVersion:apiVersion}/workflow-registry/{id}/{versionOptions?}")]
    [Route("v1/workflow-registry/{id}/{versionOptions?}")]
    [Produces("application/json")]
    public class Get : Controller
    {
        //        private readonly IWorkflowRegistry _workflowRegistry;
        //        private readonly IWorkflowBlueprintMapper _workflowBlueprintMapper;
        //        private readonly IContentSerializer _contentSerializer;

        //        public Get(IWorkflowRegistry workflowRegistry, IWorkflowBlueprintMapper workflowBlueprintMapper, IContentSerializer contentSerializer)
        //        {
        //            _workflowRegistry = workflowRegistry;
        //            _workflowBlueprintMapper = workflowBlueprintMapper;
        //            _contentSerializer = contentSerializer;
        //        }

        private readonly IConfiguration _configuration;
        public Get(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //        [HttpGet]
        //        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<WorkflowBlueprintModel>))]
        //        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(WorkflowBlueprintModelExample))]
        //        [SwaggerOperation(
        //            Summary = "Returns a single workflow blueprint.",
        //            Description = "Returns a single workflow blueprint. When no version options are specified, the latest version is returned.",
        //            OperationId = "WorkflowBlueprints.Get",
        //            Tags = new[] { "WorkflowBlueprints" })
        //        ]
        //        public async Task<ActionResult<WorkflowBlueprintModel>> Handle(string id, VersionOptions? versionOptions = default, CancellationToken cancellationToken = default)
        //        {
        //            versionOptions ??= VersionOptions.Latest;
        //            var workflowBlueprint = await _workflowRegistry.GetAsync(id, null, versionOptions.Value, cancellationToken);

        //            if (workflowBlueprint == null)
        //                return NotFound();

        //            var model = await _workflowBlueprintMapper.MapAsync(workflowBlueprint, cancellationToken);
        //            return Json(model, _contentSerializer.GetSettings());
        //        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedList<WorkflowBlueprintModel>))]
        //        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(WorkflowBlueprintModelExample))]
        //        [SwaggerOperation(
        //            Summary = "Returns a single workflow blueprint.",
        //            Description = "Returns a single workflow blueprint. When no version options are specified, the latest version is returned.",
        //            OperationId = "WorkflowBlueprints.Get",
        //            Tags = new[] { "WorkflowBlueprints" })
        //        ]
        public async Task<ActionResult<WorkflowBlueprintModel>> Handle(string id, VersionOptions? versionOptions = default, CancellationToken cancellationToken = default)
        {
            versionOptions ??= VersionOptions.LatestOrPublished;
            var elsaApiUrl = _configuration.GetValue<string>("Elsa:Server:BaseUrl");
            var client = new HttpClient();
            client.BaseAddress = new Uri(elsaApiUrl);
            var query = new Dictionary<string, string>
            {
                ["id"] = id!,
                ["versionOptions"] = versionOptions.ToString()!
            };

            //var endpointString = QueryHelpers.AddQueryString("/v1/workflow-registry/", query!);
            var endpointString = "/v1/workflow-registry/" + id + "/" + versionOptions.ToString();
            var response = await client.GetAsync(endpointString);
            var jsonResponseString = await response.Content.ReadAsStringAsync();

            var serializer = new EndpointContentSerializerSettingsProvider();
            var serializerSettings = serializer.GetSettings();

            var workflowRegs = JsonConvert.DeserializeObject<WorkflowBlueprintModel>(jsonResponseString
                , serializerSettings
                );
            var jsonResult = Json(workflowRegs);
            return jsonResult;
        }
    }
}