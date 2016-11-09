using Asset.Models;
using Asset.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asset.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }


        public JsonResult Location()
        {
            string json = Request.InputStream.ReadToEnd();
            LocationModel inputData = JsonConvert.DeserializeObject<LocationModel>(json);
            bool success = false;
            string error = "";
            AssetEntities entities = new AssetEntities();
            try
            {
                AssetLocation newEntry = new AssetLocation();
                newEntry.Address = inputData.LocationAddress;
                newEntry.Code = inputData.LocationCode;
                newEntry.Name = inputData.LocationName;
                entities.AssetLocations.Add(newEntry);
                entities.SaveChanges();
                success = true;

            }

            catch (Exception ex)
            {
                error = ex.GetType().Name + ": " + ex.Message;
            }
            finally
            {
                entities.Dispose();
            }
            var result = new { success = success, error = error };

            return Json(result);
        }


        public JsonResult AssignLocation()
        {
            string json = Request.InputStream.ReadToEnd();
            AssignLocationModel inputData = JsonConvert.DeserializeObject<AssignLocationModel>(json);

            bool success = false;
            string error = "";
            AssetEntities entities = new AssetEntities();
            try
            {
                int LocationId = (from l in entities.AssetLocations
                                  where l.Code == inputData.LocationCode
                                  select l.Id).FirstOrDefault();
                int AssetId = (from a in entities.Assets
                               where a.Code == inputData.AssetCode
                               select a.Id).FirstOrDefault();

                if (LocationId > 0 && AssetId > 0)
                {
                    AssetLocation1 newEntry = new AssetLocation1();
                    newEntry.AssetId = AssetId;
                    newEntry.LocationId = LocationId;
                    newEntry.LastSeen = DateTime.Now;
                    entities.AssetLocations1.Add(newEntry);
                    entities.SaveChanges();
                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.GetType().Name + ": " + ex.Message;
            }
            finally
            {
                entities.Dispose();
            }
            var result = new { success = success, error = error };
            return Json(result);
        }

        
        public ActionResult List()
        {
            List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

            AssetEntities entities = new AssetEntities();
            try
            {
                List<AssetLocation1> assets = entities.AssetLocations1.ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta
                CultureInfo fiFi = new CultureInfo("fi-FI");
                foreach (AssetLocation1 asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Asset.Code;
                    view.AssetName = asset.Asset.Type + ": " + asset.Asset.Model;
                    view.LastSeen = asset.LastSeen.Value.ToString(fiFi);

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);

        } 
    }
}