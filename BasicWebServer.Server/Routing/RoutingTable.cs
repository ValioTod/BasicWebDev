using BasicWebServer.Server.Common;
using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Routing
{
    public class RoutingTable : IRoutingTable
    {
        //                                              url
        private readonly Dictionary<Method, Dictionary<string, Response>> _routes;

        public RoutingTable()
        {
            this._routes = new Dictionary<Method, Dictionary<string, Response>>()
            {
                [Method.Get] = new Dictionary<string, Response>(),
                [Method.Post] = new Dictionary<string, Response>(),
                [Method.Put] = new Dictionary<string, Response>(),
                [Method.Delete] = new Dictionary<string, Response>()
            };
        }

        public IRoutingTable Map(string url, Method method, Response response)
        {
            switch (method)
            {
                case Method.Get:
                    this.MapGet(url, response);
                    break;
                case Method.Post:
                    this.MapPost(url, response);
                    break;
                default:
                    throw new InvalidOperationException($"Method '{method}' is not supported.");

            }
            return this;
        }

        public IRoutingTable MapGet(string url, Response response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this._routes[Method.Get][url] = response;

            return this;
        }

        public IRoutingTable MapPost(string url, Response response)
        {
            Guard.AgainstNull(url, nameof(url));
            Guard.AgainstNull(response, nameof(response));

            this._routes[Method.Post][url] = response;

            return this;
        }

        public Response MatchRequest(Request request)
        {
            var requestMethod = request.Method;
            var requestUrl = request.Url;

            if (!this._routes.ContainsKey(requestMethod) || !this._routes[requestMethod].ContainsKey(requestUrl))
            {
                return new NotFoundResponse();
            }

            return this._routes[requestMethod][requestUrl];
        }
    }
}
