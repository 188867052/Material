﻿namespace MaterialUI.Entity
{
    using System;
    using MaterialUI.Enums;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
    using Quartz;

    public partial class TaskScheduleModel
    {
        public TaskScheduleModel()
        {
        }

        public TaskScheduleModel(QuartzTriggers item)
        {
            string expression = string.Empty;
            if (item.QuartzCronTriggers != null)
            {
                expression = item.QuartzCronTriggers.CronExpression;
            }

            if (item.QuartzSimpleTriggers != null)
            {
                expression = (item.QuartzSimpleTriggers.RepeatInterval / 1000) + "秒";
            }

            bool isNotPaused = item.TriggerState.ToLower() != "paused";
            this.Name = item.TriggerName;
            this.Group = item.JobGroup;
            this.TriggerState = item.TriggerState;
            this.NextFireTime = item.NextFireTime;
            this.PrevFireTime = item.PrevFireTime;
            this.StartTime = item.StartTime;
            this.EndTime = item.EndTime;
            this.CronExpression = expression;
            this.IsPaused = !isNotPaused;
            this.IconClass = !isNotPaused ? "pause" : "play_circle_filled";
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public TriggerState Status { get; set; }

        public string TriggerState { get; set; }

        public string Url { get; set; }

        public string ExceptionMessage { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? NextFireTime { get; set; }

        public DateTime? PrevFireTime { get; set; }

        public string ExcutePlan { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public string CronExpression { get; set; }

        public bool IsPaused { get; }

        public int? IntervalTime { get; set; }

        public TimeSpanParseRule IntervalType { get; set; }

        public TriggerTypeEnum TriggerType { get; set; }

        public string Group { get; set; }

        public string Parameters { get; set; }

        public string Description { get; set; }

        public int? RunTimes { get; set; }

        public string IconClass { get; set; }

        [BindNever]
        public JobKey JobKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.Group))
                {
                    return new JobKey(this.Name);
                }

                return new JobKey(this.Name, this.Group);
            }
        }

        [BindNever]
        public TriggerKey TriggerKey
        {
            get
            {
                if (string.IsNullOrEmpty(this.Group))
                {
                    return new TriggerKey(this.Name);
                }

                return new TriggerKey(this.Name, this.Group);
            }
        }
    }
}
