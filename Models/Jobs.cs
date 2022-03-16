using System;
using System.Collections.Generic;

namespace WebCrawlerSWD.Models
{
    public class Job
    {
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string Salary { get; set; }
        public string Address { get; set; }
        public string LogoURL { get; set; }

        public Job(String CompanyName, String JobTitle, String Salary, String Address, String LogoURL)
        {
            this.CompanyName = CompanyName;
            this.JobTitle = JobTitle;
            this.Salary = Salary;
            this.Address = Address;
            this.LogoURL = LogoURL;
        }

    }

    public class JobsByExp
    {
        public List<Job> Jobs = new();
        public string TotalJobsByExpFound { get; set; }
        public string ExpCategory { get; set; }

        public JobsByExp()
        {
            Jobs = new List<Job>();
            TotalJobsByExpFound = "0";
            ExpCategory = "All";
        }
    }

    public class JobsMangement
    {
        public List<JobsByExp> ListJob = new();
        public string TotalJobs { get; set; }
        public string JobType { get; set; }

        public JobsMangement()
        {
            if (ListJob == null)
            {
                ListJob = new List<JobsByExp>();
            }      
            TotalJobs = "0";
            JobType = "";
        }
    }

    public class JobsChart
    {
        public List<int> ListJobChart { get; set; }

        public JobsChart()
        {
            ListJobChart = new();
        }
    }
}


