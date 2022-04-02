using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;

using Elsa.Metadata;
using Elsa.Serialization.Converters;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace P20731ElsaDashboard.Controllers.ElsaEndpoints.Activities
{
    [ApiController]
    //    [ApiVersion("1")]
    //    [Route("v{version:apiVersion}/activities")]
    [Route("v1/activities")]
    [Produces("application/json")]
    public class List : Controller
    {
        //        private readonly IActivityTypeService _activityTypeService;
        //        private readonly IEndpointContentSerializerSettingsProvider _serializerSettingsProvider;

        private readonly IConfiguration _configuration;
        public List(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //        public List(IActivityTypeService activityTypeService, IEndpointContentSerializerSettingsProvider serializerSettingsProvider)
        //        {
        //            _activityTypeService = activityTypeService;
        //            _serializerSettingsProvider = serializerSettingsProvider;
        //        }

        //        [HttpGet]
        //        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActivityDescriptor>))]
        //        [SwaggerOperation(
        //            Summary = "Returns all available activities.",
        //            Description = "Returns all available activities from which workflow definitions can be built.",
        //            OperationId = "Activities.List",
        //            Tags = new[] { "Activities" })
        //        ]
        //        public async Task<IActionResult> Handle(CancellationToken cancellationToken)
        //        {
        //            var activityTypes = await _activityTypeService.GetActivityTypesAsync(cancellationToken);
        //            var tasks = activityTypes.Where(x => x.IsBrowsable).Select(x => DescribeActivity(x, cancellationToken)).ToList();
        //            var descriptors = await Task.WhenAll(tasks);
        //            return Json(descriptors, _serializerSettingsProvider.GetSettings());
        //        }

        //        private async Task<ActivityDescriptor> DescribeActivity(ActivityType activityType, CancellationToken cancellationToken) => await _activityTypeService.DescribeActivityType(activityType, cancellationToken);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActivityDescriptor>))]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        //        [SwaggerOperation(
        //            Summary = "Returns all available activities.",
        //            Description = "Returns all available activities from which workflow definitions can be built.",
        //            OperationId = "Activities.List",
        //            Tags = new[] { "Activities" })
        //        ]

        public async Task<IActionResult> Handle(CancellationToken cancellationToken)
        {
            //            var activityTypes = await _activityTypeService.GetActivityTypesAsync(cancellationToken);
            //            var tasks = activityTypes.Where(x => x.IsBrowsable).Select(x => DescribeActivity(x, cancellationToken)).ToList();
            //            var descriptors = await Task.WhenAll(tasks);
            //            return Json(descriptors, _serializerSettingsProvider.GetSettings());
            var elsaApiUrl = _configuration.GetValue<string>("Elsa:Server:BaseUrl");
            var client = new HttpClient();
            client.BaseAddress = new Uri(elsaApiUrl);

            var endpointString = "/v1/activities/";
            var response = await client.GetAsync(endpointString);
            var jsonResponseString = await response.Content.ReadAsStringAsync();

            //jsonResponseString = jsonResponseString.Replace("\"", "\'");
            //var serializer = new EndpointContentSerializerSettingsProvider();
            //var serializerSettings = serializer.GetSettings();
            //serializerSettings.Converters.Add(new Elsa.Client.Converters.TypeConverter());
            //serializerSettings.Converters.Add(new TypeJsonConverter());

            var workflowActivities = JsonConvert.DeserializeObject<List<ActivityDescriptor>>
                (jsonResponseString);
            //var workflowRegs = JsonConvert.DeserializeObject<List<ActivityDescriptor>>(jsonResponseString);

            var serializeOptions = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true,
                Converters =
                {
                    //new MsActivityPropertyDescriptorJsonConverter(),
                    new MsActivityDescriptorJsonConverterForList(),
                },
                //PropertyNamingPolicy = new Proper
                //DictionaryKeyPolicy = new UpperCaseNamingPolicy(),
                //PropertyNamingPolicy = new UpperCaseNamingPolicy()
            };

            var jsonResult = Json(workflowActivities, serializeOptions);
            return jsonResult;
            //return jsonResponseString;
        }
    }

}
