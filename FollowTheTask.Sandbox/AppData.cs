﻿using System;
using System.IO;

namespace FollowTheTask.Sandbox
{
    public static class AppData
    {
        public static void Set()
        {
            // Set the |DataDirectory| path used in connection strings to point to the correct directory for console app and migrations
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string relative = @"..\..\..\FollowTheTask.Web\App_Data\";
            string absolute = Path.GetFullPath(Path.Combine(baseDirectory, relative));
            AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
        }
    }
}