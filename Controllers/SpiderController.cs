using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System;
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

            spider.StartCrawlingAsync(JobMng, ListUrl);
            
            return PartialView("_StartCrawling", JobMng);
        }

        public ActionResult ShowJob()
        {
            JobMng = new();
            var ListUrl = new WebURL().GetUrlLinks();
            Spider spider = new();

            spider.StartCrawlingAsync(JobMng, ListUrl);

            JobScheduler.Start();

            return PartialView("_StartCrawling", JobMng);
        }

        public ActionResult StopJob()
        {
            JobScheduler.Stop();
            return PartialView("StopJob", JobMng);
        }

        public ActionResult ShowChart()
        {
            if (JobMng == null)
            {
                JobMng = new JobsMangement();
                var ListUrl = new WebURL().GetUrlLinks();
                Spider spider = new();

                spider.StartCrawlingAsync(JobMng, ListUrl);
            }

            JobsChart numberJobs = new();

            for(int i = 1; i < JobMng.ListJob.Count; i++)
            {
                numberJobs.ListJobChart.Add(int.Parse(JobMng.ListJob[i].TotalJobsByExpFound.Replace(",","")));
            }

            return PartialView("ShowChart", numberJobs);
        }

        public ActionResult ViewDetails(string expCategory)
        {
            foreach(var job in JobMng.ListJob)
            {
                if(job.ExpCategory == expCategory)
                {
                    return View(job);
                }
            }
            return View();
        }

        public ActionResult ExportToExcel()
        {
            XLWorkbook wb = new XLWorkbook();

            if (JobMng == null)
            {
                JobMng = new JobsMangement();
                var ListUrl = new WebURL().GetUrlLinks();
                Spider spider = new();

                spider.StartCrawlingAsync(JobMng, ListUrl);
            }
            // Get total jobs summarize
            DataTable dt = new DataTable("Jobs Data Summarize");
            dt.Columns.AddRange(new DataColumn[2] {
                new DataColumn("Experience Advanced Type"),
                new DataColumn("Total Jobs Found")
            });

            var jobs = JobMng.ListJob;

            for(int i = 1; i<jobs.Count; i++)
            {
                dt.Rows.Add(jobs[i].ExpCategory, jobs[i].TotalJobsByExpFound);
            }
            // Get Detail jobs
            DataTable dt2;
            for (int i = 0; i < JobMng.ListJob.Count; i++)
            {
                dt2 = new DataTable("Exp " + JobMng.ListJob[i].ExpCategory.ToString());
                dt2.Columns.AddRange(new DataColumn[4] {
                    new DataColumn("Job Title"),
                    new DataColumn("Company Name"),
                    new DataColumn("Location"),
                    new DataColumn("Salary")
                });
                //Add job exp type
                dt2.Rows.Add(JobMng.ListJob[i].ExpCategory.ToString());

                var jobsDetail = JobMng.ListJob[i].Jobs;
                for (int j = 0; j < jobsDetail.Count; j++)
                {
                    var job = jobsDetail[j];
                    dt2.Rows.Add(job.JobTitle, job.CompanyName, job.Address, job.Salary);
                }               
                wb.Worksheets.Add(dt2);
            }
                wb.Worksheets.Add(dt);
                
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Jobs Data.xlsx");
                }
        }
    }
}
