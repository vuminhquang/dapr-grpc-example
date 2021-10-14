using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Dapr.Client.Autogen.Grpc.v1;
using Grpc.Core;

namespace dapr_grpc_client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var channel = new Channel("127.0.0.1:50001", ChannelCredentials.Insecure);
            var daprClient = new Dapr.Client.Autogen.Grpc.v1.Dapr.DaprClient(channel);
            var request = new InvokeServiceRequest
            {
                Id = "api-server",
                Message = new InvokeRequest
                {
                    Method = "weatherforecast",
                    HttpExtension = new HTTPExtension
                    {
                        Verb = HTTPExtension.Types.Verb.Get,
                        //Querystring = "page=1"
                    }
                }
            };
            
            var request2 = new InvokeServiceRequest
            {
                Id = "api-server",
                Message = new InvokeRequest
                {
                    Method = "weatherforecast/Function2",
                    HttpExtension = new HTTPExtension
                    {
                        Verb = HTTPExtension.Types.Verb.Get,
                        //Querystring = "page=1"
                    }
                }
            };

            var invokeResponse = daprClient.InvokeService(request2);
            var json = invokeResponse.Data.Value.ToStringUtf8();

            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var weatherForecasts =
                JsonSerializer.Deserialize<List<WeatherForecast>>(json, jsonSerializerOptions);


            return weatherForecasts;
        }
    }
}
