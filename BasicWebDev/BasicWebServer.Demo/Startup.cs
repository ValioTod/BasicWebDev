﻿using BasicWebServer.Server;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Responses;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using BasicWebServer.Server.Controllers;
using BasicWebServer.Demo.Controllers;
using System.Threading.Tasks;

namespace BasicWebServer.Demo
{
    internal class Startup
    {
        static async Task Main()
        {
            var server = new HttpServer(routes => routes
                .MapGet<HomeController>("/", c => c.Index())
                .MapGet<HomeController>("/Redirect", c => c.Redirect())
                .MapGet<HomeController>("/HTML", c => c.Html())
                .MapPost<HomeController>("/HTML", c => c.HtmlFormPost())
                .MapGet<HomeController>("/Content", c => c.Content())
                .MapPost<HomeController>("/Content", c => c.DownloadContent())
                .MapGet<HomeController>("/Cookies", c => c.Cookies())
                .MapGet<HomeController>("/Session", c => c.Session())
                .MapGet<UsersController>("/Login", c => c.Login())
                .MapPost<UsersController>("/Login", c => c.LogInUser())
                .MapGet<UsersController>("/Logout", c => c.Logout())
                .MapGet<UsersController>("/UserProfile", c => c.GetUserData()));

            await server.Start();
        }
    }
}
