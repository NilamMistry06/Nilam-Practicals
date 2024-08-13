using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Nodes;

namespace Practical_Nilam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        IConfiguration configuration;
        public PassengersController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet("candidate")]
        public IActionResult GetCandidate()
        {
            return Ok(new Candidate { name = "test", phone = "test" });
        }
        [HttpGet("Location")]
        public IActionResult GetLocation(string ip)
        {
            try
            {
                string url = string.Format(configuration["locationUrl"], ip);
                string json = getResponse(url);

                var obj = JsonObject.Parse(json);
                return Ok("City: " + (string)obj["city"]);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("Listing")]
        public IActionResult GetListing(int passengers)
        {
            try
            {
                string url = configuration["quoteRequest"];
                string json = getResponse(url);
                Ride objRide = JsonConvert.DeserializeObject<Ride>(json);

                List<RideDetails> listing = objRide.listings;

                var passengerListing = listing.Where(x => x.vehicleType.maxPassengers == passengers).ToList();
                passengerListing.ForEach(x => x.totalPrice = x.pricePerPassenger * x.vehicleType.maxPassengers);

                return Ok(passengerListing);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string getResponse(string url)
        {
            var request = System.Net.WebRequest.Create(url);

            using (WebResponse wrs = request.GetResponse())
            {
                using (Stream stream = wrs.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }
    }
}
