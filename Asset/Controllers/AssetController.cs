using Asset.Models;
using Asset.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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

        // GET: Asset/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Asset/Create
        public ActionResult Create()
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

        // POST: Asset/Create 
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asset/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Asset/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asset/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
