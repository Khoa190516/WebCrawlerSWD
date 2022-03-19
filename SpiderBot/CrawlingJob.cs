using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCrawlerSWD.ConstURL;
using WebCrawlerSWD.Models;

namespace WebCrawlerSWD.SpiderBot
{
    public class CrawlingJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Spider spider = new();
            JobsMangement List = new();
            var ListUrl = new WebURL().GetUrlLinks();

            string mainPageUrl = ListUrl.First().ToString();
            spider.CrawlingMainPage(List, mainPageUrl).Wait();

            for (int i = 1; i < ListUrl.Count; i++)
            {
                spider.CrawlingSubPage(List, ListUrl[i]).Wait();
            }

            foreach(var item in List.ListJob)
            {
                System.Diagnostics.Debug.WriteLine(item.ExpCategory+" Total Jobs Founded: "+item.TotalJobsByExpFound);
                foreach(var job in item.Jobs)
                {
                    System.Diagnostics.Debug.WriteLine(job.JobTitle+" || "+job.CompanyName+" || "+job.Address+" || "+job.Salary);
                }
            }

            return Task.CompletedTask;
        }
    }
}
