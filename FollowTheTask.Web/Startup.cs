﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FollowTheTask.Web.Startup))]
namespace FollowTheTask.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}