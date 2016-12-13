﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using FollowTheTask.BLL.Services.ActionSource;
using FollowTheTask.BLL.Services.ActionType;
using FollowTheTask.BLL.Services.IssueType;
using FollowTheTask.BLL.Services.Priority;
using FollowTheTask.BLL.Services.Resolution;
using FollowTheTask.BLL.Services.Role;
using FollowTheTask.TransferObjects.Model.Queries;

namespace FollowTheTask.Web.Infrastructure
{

    public sealed class StaticValues
    {
        private static readonly Lazy<StaticValues> lazy =
            new Lazy<StaticValues>(() => new StaticValues());

        public static StaticValues Instance { get { return lazy.Value; } }

        //public Dictionary<int?, string> Roles;
        public Dictionary<int?, string> ActionSources;
        public Dictionary<int?, string> ActionTypes;
        public Dictionary<int?, string> IssueTypes;
        public Dictionary<int?, string> Priorities;
        public Dictionary<int?, string> Resolutions;

        private StaticValues()
        {
            var query =
                DependencyResolver.Current.GetService<IActionSourceService>().GetAllModelDtos(new AllModelsQuery());
            var list = query.Value?.ToArray();
            if (list != null)
                for (var i = 0; i < list.Length; i++)
                    ActionSources[i + 1] = list[i].Name;
            ActionSources.Add(null, "None");

            var query1 =
                DependencyResolver.Current.GetService<IActionTypeService>().GetAllModelDtos(new AllModelsQuery());
            var list1 = query1.Value?.ToArray();
            if (list1 != null)
                for (var i = 0; i < list1.Length; i++)
                    ActionTypes[i + 1] = list1[i].Name;
            ActionTypes.Add(null, "None");

            var query2 =
                DependencyResolver.Current.GetService<IIssueTypeService>().GetAllModelDtos(new AllModelsQuery());
            var list2 = query2.Value?.ToArray();
            if (list2 != null)
                for (var i = 0; i < list2.Length; i++)
                    IssueTypes[i + 1] = list2[i].Name;
            IssueTypes.Add(null, "None");

            var query3 =
                DependencyResolver.Current.GetService<IPriorityService>().GetAllModelDtos(new AllModelsQuery());
            var list3 = query3.Value?.ToArray();
            if (list3 != null)
                for (var i = 0; i < list3.Length; i++)
                    Priorities[i + 1] = list3[i].Name;
            Priorities.Add(null, "None");

            var query4 =
                DependencyResolver.Current.GetService<IResolutionService>().GetAllModelDtos(new AllModelsQuery());
            var list4 = query4.Value?.ToArray();
            if (list4 != null)
                for (var i = 0; i < list4.Length; i++)
                    Resolutions[i + 1] = list4[i].Name;
            Resolutions.Add(null, "None");

        }

        //private void ApplyDictionary<T>(ListQueryResult<T> query, Dictionary<int?, string> dict)
        //{
        //    var list = query.Value?.ToArray();
        //    dict[null] = "None";
        //    if (list != null)
        //    {
        //        for (int i = 0; i < list.Length)
        //            {
        //                dict[i+1] = list[i].???
        //            }
        //    }
            
        //}
    }
}