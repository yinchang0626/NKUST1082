using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;

namespace WebApplication.Controllers
{
    public class StationsController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StationsController(
            ApplicationDbContext applicationDbContext
            ) 
        {
            _applicationDbContext = applicationDbContext;
        }


        public IActionResult Index()
        {
            List<YC.Models.Station> model = new List<YC.Models.Station>();
            model = _applicationDbContext.Stations.ToList();

            return View(model);
        }

        public IActionResult ImportDatas() 
        {
            ImportService importService = new ImportService();
            importService.SaveToDb();
            return Content("Imports done");

        }
    }
}