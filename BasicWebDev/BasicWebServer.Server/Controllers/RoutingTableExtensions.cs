﻿using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Controllers
{
    public static class RoutingTableExtensions
    {
        public static IRoutingTable MapGet<TController>
            (this IRoutingTable routingTable, string path, Func<TController, Response> controllerFunction)
            where TController : Controller
            => routingTable.MapGet(path, request => controllerFunction(CreateController<TController>(request)));

        public static IRoutingTable MapPost<TController>
            (this IRoutingTable routingTable, string path, Func<TController, Response> controllerFunction)
            where TController : Controller
            => routingTable.MapPost(path, request => controllerFunction(CreateController<TController>(request)));

        private static TController CreateController<TController>(Request request)
            => (TController)Activator.CreateInstance(typeof(TController), new[] { request });
    }
}
