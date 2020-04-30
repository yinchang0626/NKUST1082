using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Core;
using YC.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var stations = FindStations();

            ShowStation(stations);

            Console.WriteLine("按下任一鍵進行新增資料庫");
            Console.ReadKey();
        }
        static List<Station> FindStations()
        {

            List<Station> stations = new List<Station>();



            var xml = XElement.Load(@"d:\THBRM.xml");
            XNamespace gml = @"http://www.opengis.net/gml/3.2";
            XNamespace twed = @"http://twed.wra.gov.tw/twedml/opendata";

            List<XElement> stationsNode = xml.Descendants(twed + "RiverStageObservatoryProfile").ToList();


            for (var i = 0; i < stationsNode.Count(); i++)
            {
                var stationNode = stationsNode[i];


            }

            foreach (var stationNode in stationsNode)
            {

            }


            stationsNode
                .Where((x, y) =>
                {
                    return !x.IsEmpty;
                })
                .ToList()
                .ForEach(stationNode =>
                {
                    var BasinIdentifier = stationNode.Element(twed + "BasinIdentifier").Value.Trim();
                    var ObservatoryName = stationNode.Element(twed + "ObservatoryName").Value.Trim();
                    var LocationAddress = stationNode.Element(twed + "LocationAddress").Value.Trim();

                    var LocationByTWD67pos = stationNode.Element(twed + "LocationByTWD67_XY").Value.Trim();
                    var LocationByTWD97pos = stationNode.Element(twed + "LocationByTWD97_XY").Value.Trim();
                    Station stationData = new Station();
                    stationData.ID = BasinIdentifier;
                    stationData.LocationAddress = LocationAddress;
                    stationData.LocationByTWD67 = LocationByTWD67pos;
                    stationData.ObservatoryName = ObservatoryName;
                    stationData.CreateTime = DateTime.Now;
                    stations.Add(stationData);

                });

            



            return stations;

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
