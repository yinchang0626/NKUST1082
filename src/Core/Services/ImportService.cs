using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using WebApplication.Data;
using YC.Models;

namespace Core.Services
{
    public class ImportService
    {
        public List<Station> FindStations()
        {

            List<Station> stations = new List<Station>();



            var xml = XElement.Load(@"d:\THBRM.xml");
            XNamespace gml = @"http://www.opengis.net/gml/3.2";
            XNamespace twed = @"http://twed.wra.gov.tw/twedml/opendata";

            List<XElement> stationsNode = xml.Descendants(twed + "RiverStageObservatoryProfile").ToList();


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

        public void SaveToDb(List<Station> stations = null)
        {
            List<Station> datas = stations;

            var option = new Microsoft.EntityFrameworkCore.DbContextOptions<ApplicationDbContext>();
            ApplicationDbContext db = new ApplicationDbContext(option);

            if (stations == null || stations.Count == 0)
            {
                datas = this.FindStations();
            }

            datas.ForEach(item =>
            {
                db.Stations.Add(item);
            });

            db.SaveChanges();


        }
    }
}
