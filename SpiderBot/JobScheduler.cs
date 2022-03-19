using Quartz;
using Quartz.Impl;
using System;

namespace WebCrawlerSWD.SpiderBot
{
    public static class JobScheduler
    {
        public static async void Start()
        {
            System.Diagnostics.Debug.WriteLine("Scheduler started.........");
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<CrawlingJob>().Build();

            ITrigger trigger = TriggerBuilder.Create().
                WithIdentity("Trigger 1","group 1").
                StartNow().
                WithSimpleSchedule(x => x
                .WithIntervalInMinutes(5).
                RepeatForever()).       
                    Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public static async void Stop()
        {
            System.Diagnostics.Debug.WriteLine("Scheduler started.........");
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await schedulerFactory.GetScheduler();

            await scheduler.Shutdown();
            System.Diagnostics.Debug.WriteLine("Scheduler stopped.........");
        }
    }
}
