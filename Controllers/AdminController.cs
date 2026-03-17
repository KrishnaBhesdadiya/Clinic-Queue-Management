using Frontend_Exam.Services;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Frontend_Exam.Models;
using System.Text;
using Newtonsoft.Json.Serialization;

namespace Frontend_Exam.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        #region ConfigurationField
        public AdminController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud/admin/");
        }
        #endregion

        #region AddToken
        private void AddToken(HttpRequestMessage request)
        {
            string token = CommonVariables.Token();
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
        #endregion

        #region Get Clinic Data
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "clinic");
            AddToken(request);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;

                var clinic = JsonConvert.DeserializeObject<Clinic>(result);

                return View(new List<Clinic> { clinic });
            }
            else
            {
                TempData["ErrorMessage"] = "Unable to fetch Clinic data. Please try again later.";
            }

            // If error, redirect to Index view
            return View("Index");
        }
        #endregion

        #region Users
        [HttpGet]
        public async Task<IActionResult> Users()
        {

            var request = new HttpRequestMessage(HttpMethod.Get, "users");
            AddToken(request);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                var users = JsonConvert.DeserializeObject<List<ClinicUser>>(result);

                return View(users);
            }

            TempData["Error"] = "Failed to load users";
            return View(new List<ClinicUser>());
        }
        #endregion

        #region AddUser
        [HttpPost]
        //public async Task<IActionResult> AddUser(string name, string email, string password, string role, string phone)
        //{
        //    var payload = new
        //    {
        //        name,
        //        email,
        //        password,
        //        role,
        //        phone
        //    };

        //    var request = new HttpRequestMessage(HttpMethod.Post, "users");
        //    AddToken(request);
        //    request.Content = new StringContent(
        //        JsonConvert.SerializeObject(payload),
        //        Encoding.UTF8,
        //        "application/json"
        //    );

        //    var response = await _httpClient.SendAsync(request);

        //    return RedirectToAction("Users");
        //}
        public async Task<IActionResult> AddUser(string name, string email, string password, string role, string phone)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, "users");
            AddToken(request);

            var payload = new { name, email, password, role, phone };
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string jsonPayload = JsonConvert.SerializeObject(payload, settings);

            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                // This string will tell you EXACTLY why the API said "Bad Request"
                var errorDetails = await response.Content.ReadAsStringAsync();
                Console.WriteLine(errorDetails); // Or set a breakpoint here
            }

            if (response.IsSuccessStatusCode)
            {
                // Set the success message for the next request
                TempData["SuccessMessage"] = $"{role} created. They can sign in with the email and password you set.";
            }

            return RedirectToAction("Users");
        }
        #endregion
    }
}
