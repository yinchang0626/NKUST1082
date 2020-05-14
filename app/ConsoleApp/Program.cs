using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WebApplication.Data;
using YC.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            ImportService importService = new ImportService();
            var stations = importService.FindStations();

            ShowStation(stations);

            Console.WriteLine("按下任一鍵進行新增資料庫");

            Console.ReadKey();

            importService.SaveToDb(stations);

            Console.WriteLine("新增資料庫完成");

            Console.ReadKey();
        }
  


        private static void ShowStation(List<Station> stations)
        {
            Console.WriteLine(string.Format("共收到{0}筆監測站的資料", stations.Count));
            stations.ForEach(x =>
            {
                Console.WriteLine(string.Format("站點名稱：{0},地址:{1}", x.ObservatoryName, x.LocationAddress));


            });


        }


    }
}
