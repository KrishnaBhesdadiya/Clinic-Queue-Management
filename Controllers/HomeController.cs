using System.Diagnostics;
using System.Net.Http.Headers;
using Frontend_Exam.Models;
using Frontend_Exam.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Frontend_Exam.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        #region ConfigurationField
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://cmsback.sampaarsh.cloud/appointments");
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

        #region Get
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
