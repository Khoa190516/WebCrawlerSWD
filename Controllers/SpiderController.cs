using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.IO;
using WebCrawlerSWD.ConstURL;
using WebCrawlerSWD.Models;
using WebCrawlerSWD.SpiderBot;

namespace WebCrawlerSWD.Controllers
{
    public class SpiderController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult StartCrawling()
        {
            JobsMangement List = new();
            var ListUrl = new WebURL().GetUrlLinks();
            Spider spider = new();

            spider.StartCrowlingAsync(List, ListUrl);

            return PartialView("_StartCrawling", List);
        }

        public ActionResult ShowChart()
        {
            JobsMangement List = new();
            var ListUrl = new WebURL().GetUrlLinks();
            Spider spider = new();

            spider.StartCrowlingAsync(List, ListUrl);

            JobsChart numberJobs = new();

            for(int i = 1; i < List.ListJob.Count; i++)
            {
                numberJobs.ListJobChart.Add(int.Parse(List.ListJob[i].TotalJobsByExpFound.Replace(",","")));
            }

            return View(numberJobs);
        }

        [HttpPost]
        public ActionResult ExportToExcel()
        {
            JobsMangement List = new();
            var ListUrl = new WebURL().GetUrlLinks();
            Spider spider = new();

            spider.StartCrowlingAsync(List, ListUrl);

            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Experience Advanced Type"),
                new DataColumn("Total Jobs Found")
            });

            var jobs = List.ListJob;
            for(int i = 1; i<jobs.Count; i++)
            {
                dt.Rows.Add(jobs[i].ExpCategory, jobs[i].TotalJobsByExpFound);
            }
            using(XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
    }
}
