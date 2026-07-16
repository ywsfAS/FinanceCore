using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace FinanceCore.Infrastructure.BackgroundJobs
{
    public static class QuartzJobExtensions
    {
        public static void AddJobWithTrigger<TJob>(
        this IServiceCollection service,
        string jobKey,
        string JobTrigger,
        int hours
        ) where TJob : IJob
        {
            service.AddQuartz((options) =>
            {

                var key = new JobKey(jobKey);
                var trigger = new TriggerKey(JobTrigger);
                options.AddJob<TJob>(opts => opts.WithIdentity(key));

                options.AddTrigger(options =>
                {
                    options
                    .ForJob(jobKey)
                    .WithIdentity(trigger)
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInHours(hours).RepeatForever());
                });

            });


        }
    }
}
