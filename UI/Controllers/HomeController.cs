﻿using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using UI.Models;
using UI.ViewModels;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory = null;
        private readonly IConfiguration _configuration = null;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Sanpham()
        {
            string? httpClientName = _configuration["HttpClientName"];
            using HttpClient client = _httpClientFactory.CreateClient(httpClientName?? "");
            using HttpResponseMessage response = await client.GetAsync("api/SanPhamChiTiets");
            //using HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/SanPhamChiTiets");
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var list = new Sanphamvm() { sanPhamChiTiets = JsonConvert.DeserializeObject<List<SanPhamChiTiet>>(jsonResponse) };
            response.EnsureSuccessStatusCode();
            return View(list);
        }

        public async Task<IActionResult> AddProduct([Bind()]Sanphamvm product)
        {       
            using HttpClient client = _httpClientFactory.CreateClient();
            using HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44308/api/SanPhamChiTiets", product.SanPhamChiTiet);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Sanpham");
            }
            return NotFound();
        }
        public async Task<IActionResult> AddGiohang(GioHang gioHang)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            using HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44308/api/Giohangs", gioHang);
            if (response.IsSuccessStatusCode)
            {
                return View();
            }
            return View();
        }
        [HttpPost]
        [Route("Home/Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]SanPhamChiTiet sanpham)
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            using HttpResponseMessage response = await client.PutAsJsonAsync($"api/SanPhamChiTiets/{id}", sanpham);
            response.EnsureSuccessStatusCode();
            return Ok();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Giohang()
        {
            return View();
        }
        public IActionResult Banggia()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {

            return RedirectToAction(nameof(Sanpham));

        }
    }
}
