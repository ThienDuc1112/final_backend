using Application.Domain.DTOs.Zoom;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Application.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ZoomController : Controller
    {
        private const string clientId = "CuTpPTdMS3694ytsQf7icA";
        private const string clientSecret = "CjunSQB2GWcxxVj8Xc7JSLGtTekBNEhp";
        private const string accountId = "sPd9Cpm6RcOsCjiXDNX1lw";

        [HttpGet]
        public async Task<ActionResult<string>> CreateMeeting()
        {
            string jwtToken = await GetAccessToken();

            string meetingTopic = "JobBox platform Meeting";
            string meetingStartTime = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
            int meetingDuration = 60;


            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string apiUrl = "https://api.zoom.us/v2/users/me/meetings";
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", jwtToken);
            var requestBody = new
            {
                topic = meetingTopic,
                type = 2,
                start_time = meetingStartTime,
                duration = meetingDuration,
                timeZone = "UTC",
                settings = new
                {
                    host_video = true,
                    participant_video = true,
                }
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return Ok(responseContent);
            }
            else
            {
                return BadRequest(responseContent);
            }

        }


        private async Task<string> GetAccessToken()
        {
            string accountId = "sPd9Cpm6RcOsCjiXDNX1lw";
            string clientId = "CuTpPTdMS3694ytsQf7icA";
            string clientSecret = "CjunSQB2GWcxxVj8Xc7JSLGtTekBNEhp";
            using (var client = new HttpClient())
            {
                var credentials = $"{clientId}:{clientSecret}";
                var encodedCredentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", encodedCredentials);

                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("grant_type", "account_credentials"),
                new KeyValuePair<string, string>("account_id", accountId)
            });

                var response = await client.PostAsync("https://zoom.us/oauth/token", content);
                var jsonString = await response.Content.ReadAsStringAsync();


                var tokenResponse = JsonSerializer.Deserialize<ZoomTokenResponse>(jsonString);
                return tokenResponse.AccessToken;
            }
        }
    }
}
