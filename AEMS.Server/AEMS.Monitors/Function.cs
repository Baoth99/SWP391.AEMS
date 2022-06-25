using System;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

// AEMS.Monitors::AEMS.Monitors.Function::FunctionHandler
// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AEMS.Monitors
{
    public class Function
    {
        /// <summary>
        /// The cosmos database connection
        /// </summary>
        public static string CosmosDBConnection = Environment.GetEnvironmentVariable("CosmosDBConnection");

        /// <summary>
        /// The cosmos database
        /// </summary>
        public static string CosmosDatabase = Environment.GetEnvironmentVariable("CosmosDatabase");

        /// <summary>
        /// The cosmos container
        /// </summary>
        public static string CosmosContainer = Environment.GetEnvironmentVariable("CosmosContainer");

        /// <summary>
        /// The cosmos device information container
        /// </summary>
        public static string CosmosDeviceInfoContainer = Environment.GetEnvironmentVariable("CosmosDeviceInfoContainer");

        /// <summary>
        /// The aws access key
        /// </summary>
        public static string AWSAccessKey = Environment.GetEnvironmentVariable("AWSAccessKey");

        /// <summary>
        /// The aws secret key
        /// </summary>
        public static string AWSSecretKey = Environment.GetEnvironmentVariable("AWSSecretKey");

        /// <summary>
        /// Gets the date time now.
        /// </summary>
        /// <value>
        /// The date time now.
        /// </value>
        public static DateTime DateTimeNow => string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TimeZoneInfo")) ? TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(Environment.GetEnvironmentVariable("TimeZoneInfo"))) : DateTime.UtcNow;

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task FunctionHandler(AEMSDeviceMessageRequestModel requestModel, ILambdaContext context)
        {
            using (CosmosClient client = new CosmosClient(connectionString: CosmosDBConnection))
            {
                try
                {
                    var container = client.GetContainer(CosmosDatabase, CosmosContainer);

                    if (requestModel.Status == 2)
                    {
                        // Update status in DB
                    }

                    var document = new DeviceLogDocument()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CreatedAt = requestModel.CreatedAt,
                        DeviceId = requestModel.DeviceId,
                        EventTime = requestModel.EventTime,
                        EventType = requestModel.EventType,
                        Message = requestModel.Message,
                        ReceivedAt = DateTimeNow,
                        Status = requestModel.Status,
                        StatusMessage = requestModel.StatusMessage,

                        
                        BatteryLevel = requestModel.BatteryLevel,
                        Power = requestModel.Power,
                        Energy = requestModel.Energy,
                        Voltage = requestModel.Voltage,
                        Temperature = requestModel.Temperature,
                        MeterType = requestModel.MeterType,
                        Version = requestModel.Version,

                        Latitude = requestModel.Geolocation.Latitude,
                        Longitude = requestModel.Geolocation.Longitude,
                        Altitude = requestModel.Geolocation.Altitude,
                        Magnetometer_x = requestModel.Magnetometer.x,
                        Magnetometer_y = requestModel.Magnetometer.y,
                        Magnetometer_z = requestModel.Magnetometer.z,   
                    };

                    var newItem = await container.CreateItemAsync<DeviceLogDocument>(document, new PartitionKey(document.DeviceId));
                    if (newItem.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        LambdaLogger.Log($"Message is created at {requestModel.CreatedAt} from {requestModel.DeviceId} was got at {DateTimeNow} successfully !");

                        var deviceContainer = client.GetContainer(CosmosDatabase, CosmosDeviceInfoContainer);
                        var deviceInfo = await deviceContainer.ReadItemAsync<DeviceInfoDocument>(requestModel.DeviceId, new PartitionKey(document.DeviceId));

                        if (deviceInfo.StatusCode == HttpStatusCode.OK)
                        {
                            var powerBiDashboardEndpoint = deviceInfo.Resource.PowerBiDashboardEndpoint;

                            if (!string.IsNullOrEmpty(powerBiDashboardEndpoint))
                            {
                                var pbiModel = new DeviceLogPowerBiDashboardModel()
                                {
                                    deviceId = requestModel.DeviceId,
                                    eventTime = requestModel.EventTime,
                                    eventType = requestModel.EventType,
                                    message = requestModel.Message,
                                    receivedAt = document.ReceivedAt,
                                    Status = requestModel.Status,
                                    statusMessage = requestModel.StatusMessage,

                                    batteryLevel = requestModel.BatteryLevel,
                                    power = requestModel.Power,
                                    energy = requestModel.Energy,
                                    voltage = requestModel.Voltage,
                                    temperature = requestModel.Temperature,
                                    meterType = requestModel.MeterType,
                                    version = requestModel.Version,

                                    latitude = requestModel.Geolocation.Latitude,
                                    longitude = requestModel.Geolocation.Longitude,
                                    altitude = requestModel.Geolocation.Altitude,
                                    magnetometer_x = requestModel.Magnetometer.x,
                                    magnetometer_y = requestModel.Magnetometer.y,
                                    magnetometer_z = requestModel.Magnetometer.z,
                                };

                                var jsonMessage = JsonConvert.SerializeObject(pbiModel);

                                await HttpClientUtil.ClientPost(powerBiDashboardEndpoint, jsonMessage);
                            }
                        }
                    }
                    else
                    {
                        LambdaLogger.Log($"Fail to get message from {requestModel.DeviceId} at {DateTimeNow}");
                    }
                }
                catch (Exception ex)
                {
                    LambdaLogger.Log(ex.Message);
                }
            }

        }
    }
}
