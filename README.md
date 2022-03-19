# WebCrawlerSWD
ASP.Net MVC C#
Spider to crawling topcv website

Library: 
- Quartz for scheduling task (jobs)
- Chart.js for showing data in chart (Pie Chart in this project)
- HtmlAgilityPack for crawling website
- ClosedXML for export data to excel file
- Bootstrap for decorating web UI
- JQuery for validating js code in .cshtml file
- 
Data Structure:
    class Job {
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string Salary { get; set; }
        public string Address { get; set; }
        public string LogoURL { get; set; }
    }
    
    class JobByExp {
        public List<Job> Jobs = new();
        public string TotalJobsByExpFound { get; set; }
        public string ExpCategory { get; set; }
    }
    
    class JobsMangement
    {
        public List<JobsByExp> ListJob = new();
        public string TotalJobs { get; set; }
        public string JobType { get; set; }
    } 
    
