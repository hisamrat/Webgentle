using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Webgentle.Models;
using Webgentle.Services;

namespace Webgentle.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewBookAlertConfig _newBookAlertConfig;
        private readonly IConfiguration _configuration;
        private readonly IOptions<NewBookAlertConfig> newBookAlertConfig;
        private readonly IUserService _userService;

        public HomeController(IConfiguration configuration,IOptions<NewBookAlertConfig> newBookAlertConfig,IUserService userService)
        {
            _newBookAlertConfig = newBookAlertConfig.Value;
            _configuration = configuration;
            this.newBookAlertConfig = newBookAlertConfig;
            _userService = userService;
        }

        [ViewData]
        public BookModel Book { get; set; }
        public ViewResult Index()
        {
            var userid = _userService.GetUserId();
            var result = _configuration.GetValue<bool>("NewBookAlert:DispleyNewBookAlert");
            var result1 = _configuration.GetValue<string>("NewBookAlert:BookName");
            //bool isDisplay = _newBookAlertConfig.DisplayNewBookAlert;
            Book = new BookModel() { Id = 1, Author = "samrat" };

           // var result = _configuration["AppName"];
            //dynamic data = new ExpandoObject();
            //data.Id = 123;
            //data.Name = "samrat";

            //ViewBag.Data = new BookModel() { Id = 1, Author = "samrat" };
            return View();
        }
        public ViewResult AboutUs()
        {
            return View();
        }
        public ViewResult ContactUs()
        {
            return View();
        }
    }
}
