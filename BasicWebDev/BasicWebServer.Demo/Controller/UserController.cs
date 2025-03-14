using BasicWebServer.Server.Controllers;
using BasicWebServer.Server.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

//using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BasicWebServer.Demo.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(Request request) : base(request)
        {

        }

        private const string Username = "user";
        private const string Password = "user123";

        public Response Login() => View();

        public Response LogInUser()
        {
            this.Request.Session.Clear();

            var usernameMatches = this.Request.Form["Username"] == UsersController.Username;
            var passwordMatches = this.Request.Form["Password"] == UsersController.Password;

            if (usernameMatches && passwordMatches)
            {
                if (!this.Request.Session.ContainsKey(Session.SessionUserKey))
                {
                    this.Request.Session[Session.SessionUserKey] = "MyUserId";

                    var cookies = new CookieCollection();
                    cookies.Add(Session.SessionCookieName, this.Request.Session.Id);

                    return Html("<h3>Logged successfully!</h3>", cookies);
                }
                return Html("<h3>Logged successfully!</h3>");
            }
            return Redirect("/Login");
        }

        public Response Logout()
        {
            this.Request.Session.Clear();
            return Html("<h3>Logged out successfully!</h3>");
        }

        public Response GetUserData()
        {
            if (this.Request.Session.ContainsKey(Session.SessionUserKey))
            {
                return Html($"<h3>Currently logged-in user is " + $"with username '{UsersController.Username}'</h3>");
            }
            return Redirect("/Login");
        }
    }
}
