using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUD_Smartphone_Marca.Models;
using Microsoft.CodeAnalysis.Options;
using CRUD_Smartphone_Marca.Model.Options;
using Microsoft.Extensions.Options;

namespace CRUD_Smartphone_Marca.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<TestOption> _testOption;

        public HomeController(
            ILogger<HomeController> logger,
            IOptions<TestOption> testOption)
        {
            _logger = logger;
            _testOption = testOption;

            //if (testOption.CurrentValue.ExampleString == "Asd")
            //{

            //}
            //else if (testOption.CurrentValue.ExampleString == "qwe")
            //{

            //}
            //else if (testOption.CurrentValue.ExampleString == "zxc")
            //{

            //}
            //else
            //{
            //    throw new ArgumentException("Opção inválida", nameof(testOption.CurrentValue.ExampleString));
            //}
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
    }
}
