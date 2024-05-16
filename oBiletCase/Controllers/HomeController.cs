using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using oBiletCase.Models;
using oBiletCaseBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace oBiletCase.Controllers
{
    public class HomeController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5249/api");
        private readonly HttpClient client;

        public HomeController()
        {
            client = CreateHttpClient();
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
            return httpClient;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var busLocationData = GetBusLocationData();

            ViewBag.CurrentBusLocation = new SelectList(busLocationData, "id", "name");
            ViewBag.CurrentDate = DateTime.Now.AddDays(1);

            return View();
        }

        private List<Data> GetBusLocationData()
        {
            var busLocationDataResponseMessage = client.GetAsync(client.BaseAddress + "/Home/BusLocation").Result;

            if (busLocationDataResponseMessage.IsSuccessStatusCode)
            {
                var busLocationJsonData = busLocationDataResponseMessage.Content.ReadAsStringAsync().Result;
                var busLocationObjectData = JsonConvert.DeserializeObject<Root>(busLocationJsonData);
                return busLocationObjectData.data;
            }

            return new List<Data>();
        }

        public ActionResult TicketListView(string currentBusLocation, string destinationLocation, string destinationLocationText, string currentBusLocationText, string date)
        {
            SetViewBagValues(currentBusLocation, destinationLocation, destinationLocationText, currentBusLocationText, date);

            var ticketListData = GetTicketListData(currentBusLocation, destinationLocation, date);
            SetViewBagJourneys(ticketListData);

            return View("TicketListView");
        }

        private void SetViewBagValues(string currentBusLocation, string destinationLocation, string destinationLocationText, string currentBusLocationText, string date)
        {
            ViewBag.CurrentBusLocationId = currentBusLocation;
            ViewBag.CurrentBusLocationText = currentBusLocationText;
            ViewBag.DestinationLocationId = destinationLocation;
            ViewBag.DestinationLocationText = destinationLocationText;
            ViewBag.Date = date;
        }

        private List<TicketListResponseData> GetTicketListData(string currentBusLocation, string destinationLocation, string date)
        {
            var ticketListRequestContentObject = new TicketListRequestContent
            {
                currentBusLocation = currentBusLocation,
                destinationLocation = destinationLocation,
                date = DateTime.Parse(date)
            };

            var ticketListRequestContentJson = SerializeObject(ticketListRequestContentObject);
            var ticketListRequestContentData = new StringContent(ticketListRequestContentJson, Encoding.UTF8, "application/json");
            var ticketListDataResponse = client.PostAsync(client.BaseAddress + "/Home/TicketList", ticketListRequestContentData).Result;

            if (ticketListDataResponse.IsSuccessStatusCode)
            {
                var ticketListJsonData = ticketListDataResponse.Content.ReadAsStringAsync().Result;
                var ticketListData = JsonConvert.DeserializeObject<TicketListResponse>(ticketListJsonData);
                return ticketListData.data ?? new List<TicketListResponseData>();
            }

            return new List<TicketListResponseData>();
        }

        private void SetViewBagJourneys(List<TicketListResponseData> ticketListData)
        {
            var journeys = new List<Journey>();
            foreach (var ticket in ticketListData)
            {
                var journey = ticket.journey;
                if (journey != null)
                {
                    journeys.Add(journey);
                }
            }

            var sortedJourneysData = journeys.OrderBy(journey => journey.departure).ToList();
            ViewBag.Journeys = sortedJourneysData;
        }

        private string SerializeObject(object serializeObject)
        {
            return JsonConvert.SerializeObject(serializeObject);
        }
    }
}
