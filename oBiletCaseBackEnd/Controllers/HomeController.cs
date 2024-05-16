using Microsoft.AspNetCore.Mvc;
using oBiletCaseBackEnd.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace oBiletCaseBackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        private const string AUTHORIZATION_TYPE = "Basic";
        private const string AUTHORIZATION_TYPE_VALUE = "JEcYcEMyantZV095WVc3G2JtVjNZbWx1";
        private const string APPLICATION_JSON = "application/json";
        private const string SESSION_ID = "PqtdftjloK3Kpka97+ILDzMa6D9740nggLiTzXiLlzA=";
        private const string DEVICE_ID = "PqtdftjloK3Kpka97+ILDzMa6D9740nggLiTzXiLlzA=";
        private const string LANGUAGE = "tr-TR";

        public HomeController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> BusLocation()
        {
            var busLocationRequest = CreateBusLocationRequest();

            string busLocationRequestJsonData = SerializeObject(busLocationRequest);

            var httpClient = CreateHttpClient();
            var busLocationDataResponseMessage = await httpClient.PostAsync("http://v2-api.obilet.com/api/location/getbuslocations", new StringContent(busLocationRequestJsonData, Encoding.UTF8, APPLICATION_JSON));
            var busLocationResponse = await busLocationDataResponseMessage.Content.ReadAsStringAsync();
            var busLocationData = JsonConvert.DeserializeObject<Root>(busLocationResponse);
            return Ok(busLocationData);
        }

        [HttpPost]
        public async Task<IActionResult> TicketList(TicketListRequestContent content)
        {
            var getTicketListRequestObject = CreateTicketListRequest(content);

            using var httpClient = CreateHttpClient();
            var getTicketListRequestJson = new StringContent(SerializeObject(getTicketListRequestObject), Encoding.UTF8, APPLICATION_JSON);
            var ticketListHttpResponseData = await httpClient.PostAsync("https://v2-api.obilet.com/api/journey/getbusjourneys", getTicketListRequestJson);
            var ticketListDataObject = await ticketListHttpResponseData.Content.ReadAsStringAsync();
            var ticketListData = JsonConvert.DeserializeObject<TicketListResponse>(ticketListDataObject);

            return Ok(ticketListData);
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AUTHORIZATION_TYPE, AUTHORIZATION_TYPE_VALUE);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(APPLICATION_JSON));
            return httpClient;
        }

        private GetBusLocationRequest CreateBusLocationRequest()
        {
            return new GetBusLocationRequest
            {
                Data = null,
                DeviceSession = new DeviceSession
                {
                    SessionId = SESSION_ID,
                    DeviceId = DEVICE_ID
                },
                Date = new DateTime(2024, 3, 11, 11, 33, 0),
                Language = LANGUAGE
            };
        }

        private TicketListRequest CreateTicketListRequest(TicketListRequestContent content)
        {
            return new TicketListRequest
            {
                DeviceSession = new DeviceSession
                {
                    SessionId = SESSION_ID,
                    DeviceId = DEVICE_ID
                },
                Date = new DateTime(content.date.Year, content.date.Month, content.date.Day),
                Language = LANGUAGE,
                Data = new TicketListRequestData
                {
                    OriginId = Convert.ToInt32(content.currentBusLocation),
                    DestinationId = Convert.ToInt32(content.destinationLocation),
                    DepartureDate = new DateTime(content.date.Year, content.date.Month, content.date.Day)
                }
            };
        }

        private string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
