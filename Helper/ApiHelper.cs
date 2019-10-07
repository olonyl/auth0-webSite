using Auth0WebSite.ViewModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Auth0WebSite.Helper
{
    public class ApiHelper
    {
        private readonly IConfiguration _configuration;
        public ApiHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Token> GenerateToken()
        {
            Token token = new Token();
        
            using (var httpClient = new HttpClient())
            {

                var payload = new
                {
                    client_id = _configuration["Auth0:ClientId"],
                    client_secret = _configuration["Auth0:ClientSecret"],
                    audience = _configuration["Auth0:ApiIdentifier"],
                    grant_type = "client_credentials"
                };
                HttpContent content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"https://{_configuration["Auth0:Domain"]}/oauth/token", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    token = JsonConvert.DeserializeObject<Token>(apiResponse);
                }              
            }

            return token;
        }
    }
}
