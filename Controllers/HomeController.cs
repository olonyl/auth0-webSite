using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Auth0WebSite.ViewModels;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Auth0WebSite.Helper;

namespace Auth0WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<Comment> comments = new List<Comment>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{_configuration["API:Url"]}/api/comments"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    comments = JsonConvert.DeserializeObject<List<Comment>>(apiResponse);
                }
            }

            return View(comments);
        }

        [Authorize]
        public async Task<IActionResult> Employments()
        {
       
            List<EmploymentViewModel> employments = new List<EmploymentViewModel>();
            using (var httpClient = new HttpClient())
            {
                ApiHelper helper = new ApiHelper(_configuration);
                Token token = await helper.GenerateToken();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.token_type, token.access_token);
                using (var response = await httpClient.GetAsync($"{_configuration["API:Url"]}/api/employments"))
                {                   
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employments = JsonConvert.DeserializeObject<List<EmploymentViewModel>>(apiResponse);
                }
            }

            return View(employments);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
