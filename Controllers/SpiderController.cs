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
        private static JobsMangement JobMng = new();

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult StartCrawling()
        {
            JobMng = new();
            var ListUrl = new WebURL().GetUrlLinks();
            Spider spider = new();

            spider.StartCrowlingAsync(JobMng, ListUrl);

            return PartialView("_StartCrawling", JobMng);
        }

        public ActionResult ShowChart()
        {
            if (JobMng == null)
            {
                JobMng = new JobsMangement();
                var ListUrl = new WebURL().GetUrlLinks();
                Spider spider = new();

                spider.StartCrowlingAsync(JobMng, ListUrl);
            }

            JobsChart numberJobs = new();

            for(int i = 1; i < JobMng.ListJob.Count; i++)
            {
                numberJobs.ListJobChart.Add(int.Parse(JobMng.ListJob[i].TotalJobsByExpFound.Replace(",","")));
            }

            return PartialView("ShowChart", numberJobs);
        }

        public ActionResult ExportToExcel()
        {
            if (JobMng == null)
            {
                JobMng = new JobsMangement();
                var ListUrl = new WebURL().GetUrlLinks();
                Spider spider = new();

                spider.StartCrowlingAsync(JobMng, ListUrl);
            }
            
            DataTable dt = new DataTable("Jobs Data");
            dt.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Experience Advanced Type"),
                new DataColumn("Total Jobs Found")
            });

            var jobs = JobMng.ListJob;
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Jobs Data.xlsx");
                }
            }
        }
    }
}
