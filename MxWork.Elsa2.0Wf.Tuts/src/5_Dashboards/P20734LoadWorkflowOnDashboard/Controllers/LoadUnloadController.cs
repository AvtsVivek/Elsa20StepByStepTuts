using Elsa;
using Elsa.Builders;
using Elsa.Providers.Workflow;
using Elsa.Serialization;
using Elsa.Server.Api.Services;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MxWork.Elsa2Wf.Tuts.BasicActivities.Workflows;
using Open.Linq.AsyncExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace P20734LoadWorkflowOnDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoadUnloadController : Controller
    {
        private readonly ElsaOptions _elsaOptions;
        private readonly IWorkflowRegistry _workflowRegistry;
        private readonly IEnumerable<IWorkflowProvider> _workflowProviders;
        private readonly IWorkflowBlueprintMapper _workflowBlueprintMapper;
        private readonly IContentSerializer _contentSerializer;

        public LoadUnloadController(ElsaOptions elsaOptions, 
            IWorkflowRegistry workflowRegistry,
            IEnumerable<IWorkflowProvider> workflowProviders,
            IWorkflowBlueprintMapper workflowBlueprintMapper,
            IContentSerializer contentSerializer
            )
        {
            _elsaOptions = elsaOptions;
            _workflowRegistry = workflowRegistry;
            _workflowProviders = workflowProviders;
            _workflowBlueprintMapper = workflowBlueprintMapper;
            _contentSerializer = contentSerializer;
    }

        [HttpPost("LoadWorkflowIntoRegistry")]
        public async Task<JsonResult> LoadWorkflowIntoRegistry(WorkflowToLoadData workflowToLoadData, 
            CancellationToken cancellationToken)
        {
            var workflowBlueprintList = new List<IWorkflowBlueprint>();

            await GetWorkflowBlueprintList(workflowBlueprintList, cancellationToken);

            UnloadTypesFromWorkflowFactory();

            await GetWorkflowBlueprintList(workflowBlueprintList, cancellationToken);

            LoadTypeIntoWorkflowFactory(workflowToLoadData.WorkflowName);

            await GetWorkflowBlueprintList(workflowBlueprintList, cancellationToken);

            var returnObject = new { FinalResult = "Successifully Loaded" };
            var jsonResult = Json(returnObject);

            return jsonResult;
        }

        [HttpGet("GetJsonForLoadedWorkflow/{workflowName}")]
        public async Task<JsonResult> GetJsonForLoadedWorkflow(string workflowName)
        {
            //var workflowBlueprint = await workflowRegistry.GetAsync(id, null, versionOptions.Value, cancellationToken);
            var workflowBlueprints = await _workflowRegistry.ListActiveAsync().ToList();
            //var workflowBlueprintList = await workflowRegistry.ListAsync().ToList();

            if (workflowBlueprints.ToList().Count == 0)
            {
                // Something wrong.
                var returnJson = new { ResultValue = "Something wrong" };
                return Json(returnJson);
            }

            var workflowBlueprint = workflowBlueprints.Where(blueprint => blueprint.Name == workflowName).FirstOrDefault();

            var workflowBlueprintModel = await _workflowBlueprintMapper.MapAsync(workflowBlueprint);
            var jsonString = _contentSerializer.Serialize(workflowBlueprintModel);
            var jsonResult = Json(jsonString);
            return jsonResult;
        }

        [HttpGet("GetWorkflowsList")]
        public ActionResult<List<string>> GetWorkflowsList()
        {
            var workflowType = typeof(IWorkflow);

            var workflowsAssembly = typeof(HelloWorldWorkflow).Assembly;
            var workflowTypes = 
                workflowsAssembly.GetTypes()
                .Where(p => workflowType.IsAssignableFrom(p));
            
            var workflowList = new List<string>();

            foreach (Type type in workflowTypes)
                workflowList.Add(type.Name);
                        
            return workflowList;
        }

        private void LoadTypeIntoWorkflowFactory(string typeNameToBeLoaded)
        {
            var workflowFactory = _elsaOptions.WorkflowFactory;

            var workflowsAssembly = typeof(HelloWorldWorkflow).Assembly;

            var workflowType = workflowsAssembly.GetTypes()
                .Where(p => p.Name == typeNameToBeLoaded).FirstOrDefault();

            if (!workflowFactory.Types.Contains(workflowType))
                workflowFactory.Add(workflowType, provider =>
                (IWorkflow)ActivatorUtilities.GetServiceOrCreateInstance(provider, workflowType));
        }

        private void UnloadTypesFromWorkflowFactory()
        {
            var workflowFactory = _elsaOptions.WorkflowFactory;
            var listOfTypesLoaded = workflowFactory.Types;

            foreach (var loadedType in listOfTypesLoaded)
                workflowFactory.Remove(loadedType);
        }

        private async Task GetWorkflowBlueprintList(List<IWorkflowBlueprint> workflowBlueprintList, CancellationToken cancellationToken)
        {
            workflowBlueprintList.Clear();

            foreach (var provider in _workflowProviders)
                await foreach (var workflowBlueprint in
                    provider.GetWorkflowsAsync(cancellationToken)
                    .WithCancellation(cancellationToken))
                {
                    workflowBlueprintList.Add(workflowBlueprint);
                }
        }
    }
    public class WorkflowToLoadData
    {
        public string WorkflowName { get; set; }
    }
}
