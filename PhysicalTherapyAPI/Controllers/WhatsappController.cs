using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;



namespace PhysicalTherapyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WhatsappController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();
        [HttpPost("SendLinksToClient")]
        public async Task<IActionResult> SendLinksToClient([FromBody] SendLinksRequest request)
        {
            var url = "https://api.maytapi.com/api/5d6d5270-3c3f-4cd2-b0f7-d202c3aa363a/57279/sendMessage";
            var linksMessage = string.Join("\n", request.Links);

            var payload = new
            {
                to_number = request.ClientNumber,
                type = "text",
                message = linksMessage
            };

            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
            var requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(url),
                Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
            };
            requestMessage.Headers.Add("x-maytapi-key", "732e9176-f77b-49d7-b703-523f5d98a6a8");

            var response = await client.SendAsync(requestMessage);
            var responseText = await response.Content.ReadAsStringAsync();

            return Ok(responseText);
        }

    }

    public class SendLinksRequest
    {
        public List<string> Links { get; set; }
        public string ClientNumber { get; set; }
    }

}