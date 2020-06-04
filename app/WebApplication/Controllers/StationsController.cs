using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Data;
using YC.Models;

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


        public IActionResult Index(string keyword)
        {
            List<YC.Models.Station> model = new List<YC.Models.Station>();

            var query = _applicationDbContext.Stations.AsQueryable();
            if (!string.IsNullOrEmpty(keyword)) 
            {
                query = query.Where(x => 
                x.ObservatoryName.Contains(keyword) || 
                x.LocationAddress.Contains(keyword));
                
            }

            model = query.OrderByDescending(x => x.CreateTime).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(YC.Models.Station station)
        {
            station.ID = System.Guid.NewGuid().ToString();
            station.CreateTime = System.DateTime.Now;
            _applicationDbContext.Stations.Add(station);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(string id) 
        {
            var model = _applicationDbContext.Stations.Find(new[] { id });
            return View(model);
        }

        public IActionResult Edit(string id, Station station) 
        {
            _applicationDbContext.Entry(station).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(string id) 
        {
            var model = _applicationDbContext.Stations.Find(new[] { id });
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Station station)
        {
            var model = _applicationDbContext.Stations.Find(new[] { station.ID });
            this._applicationDbContext.Stations.Remove(model);
            _applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private IActionResult ImportDatas() 
        {
            ImportService importService = new ImportService();
            importService.SaveToDb();
            return Content("Imports done");

        }
    }
}