using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using SchooldbMVCassign.Models;
using System.Collections.Specialized;
using System.Text;

namespace SchooldbMVCassign.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ActionResult Index()
        {
            IEnumerable<stud> studentdata = null;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44320/api/";
                var json = webClient.DownloadString("Students");
                var list = JsonConvert.DeserializeObject<List<stud>>(json);
                studentdata = list.ToList();
                return View(studentdata);
            }
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
            stud studentdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44320/api/";
                var json = webClient.DownloadString("Students/" + id);
                studentdata = JsonConvert.DeserializeObject<stud>(json);
            }
            return View(studentdata);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        public ActionResult Create(stud model)
        {

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44320/api/";
                    var url = "Students/POST";
                    //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(url, data);
                    JsonConvert.DeserializeObject<stud>(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");

        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            stud studentdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44320/api/";
                var json = webClient.DownloadString("Students/" + id);
                //  var list = emp 
                studentdata = JsonConvert.DeserializeObject<stud>(json);
            }
            return View(studentdata);
        }

        // POST: Students/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, stud model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    webClient.BaseAddress = "https://localhost:44320/api/Students/" + id;
                    //var url = "Values/Put/" + id;
                    //webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                    webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                    string data = JsonConvert.SerializeObject(model);
                    var response = webClient.UploadString(webClient.BaseAddress, "PUT", data);
                    stud modeldata = JsonConvert.DeserializeObject<stud>(response);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            stud studentdata;
            using (WebClient webClient = new WebClient())
            {
                webClient.BaseAddress = "https://localhost:44320/api/";
                var json = webClient.DownloadString("Students/" + id);
                studentdata = JsonConvert.DeserializeObject<stud>(json);
            }
            return View(studentdata);
        }

        // POST: Students/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, stud model)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    NameValueCollection nv = new NameValueCollection();
                    string url = "https://localhost:44320/api/Students/" + id;
                    var response = Encoding.ASCII.GetString(webClient.UploadValues(url, "DELETE", nv));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
    }
}

