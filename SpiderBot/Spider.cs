using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCrawlerSWD.Models;

namespace WebCrawlerSWD.SpiderBot
{
    public class Spider
    {

        public void StartCrowlingAsync(JobsMangement List, List<String> ListUrl)
        {
            string mainPageUrl = ListUrl.First().ToString();
            CrawlingMainPage(List, mainPageUrl).Wait();

            for (int i = 1; i < ListUrl.Count; i++)
            {
                CrawlingSubPage(List, ListUrl[i]).Wait();
            }
        }

        public async Task CrawlingMainPage(JobsMangement List, string Url)
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(Url);

            //get html 
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            List.JobType = CrawlJobType(htmlDocument);
            List.TotalJobs = CrawTotalJob(htmlDocument);

            JobsByExp ListJobs = CrawlingJobs(htmlDocument);
            List.ListJob.Add(ListJobs);

        }

        public async Task CrawlingSubPage(JobsMangement List, string Url)
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(Url);

            //get html 
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var JobExpCategory = CrawExpAdvanced(htmlDocument);
            var TotalJobByExp = CrawTotalJob(htmlDocument);

            JobsByExp ListJobs = CrawlingJobs(htmlDocument);
            ListJobs.TotalJobsByExpFound = TotalJobByExp;
            ListJobs.ExpCategory = JobExpCategory;

            List.ListJob.Add(ListJobs);

        }

        public string CrawlJobType(HtmlDocument htmlDocument)
        {
            //get job type
            var type = htmlDocument.DocumentNode.Descendants("h1").Where(node => node.GetAttributeValue("class", "").
                Equals("breadcrumb-heading")).First().InnerHtml;

            var JobType = type.Split(',').ToList().First().Substring(13).ToString();

            return JobType;
        }

        public string CrawTotalJob(HtmlDocument htmlDocument)
        {
            var header = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").
              Equals("job-header")).First();

            var TotalJobs = header.Descendants("b").Where(node => node.GetAttributeValue("class", "").
                Equals("text-highlight")).First().InnerHtml;

            return TotalJobs;
        }

        public string CrawExpAdvanced(HtmlDocument htmlDocument)
        {
            var options = htmlDocument.DocumentNode.Descendants("select").Where(node => node.GetAttributeValue("id", "").
                Contains("exp-advanced")).First().OuterHtml.ToString().Split("<option").ToList();

            foreach (var option in options)
            {
                if (option.Contains("selected"))
                {
                    var selectedOption = option.Split("&nbsp;").ToList().Last().
                        Split("</option").ToList().First().ToString();

                    return selectedOption;
                }
            }
            
            return "";
        }

        public JobsByExp CrawlingJobs(HtmlDocument htmlDocument)
        {
            var JobList = new JobsByExp();
            //get list of JobDivs
            var jobDivs = htmlDocument.DocumentNode.Descendants("div").Where(node => node.GetAttributeValue("class", "").
                Equals("job-item  bg-highlight  job-ta result-job-hover")).ToList();

            //get list jobs
            foreach (var jobDiv in jobDivs)
            {
                var companyDiv = jobDiv.Descendants("p").Where(node => node.GetAttributeValue("class", "").
                Equals("company underline-box-job")).First();

                var companyName = companyDiv.Descendants("a").First().InnerHtml;

                var jobTitle = jobDiv.Descendants("span").Where(node => node.GetAttributeValue("class", "").
                Equals("bold transform-job-title")).First().InnerHtml;

                var salary = jobDiv.Descendants("label").Where(node => node.GetAttributeValue("class", "").
                Equals("salary")).First().InnerHtml;

                var address = jobDiv.Descendants("label").Where(node => node.GetAttributeValue("class", "").
                Equals("address")).First().InnerHtml;

                var logo = jobDiv.Descendants("img").Last().ChildAttributes("src").First().Value;
                //add job to list
                JobList.Jobs.Add(new Job(companyName, jobTitle, salary, address, logo));
            }

            return JobList;
        }
    }
}

