using System.Web.Mvc;

namespace TestApp_MVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 404 Not Found
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404() 
        {
            Response.StatusCode = 404;
            return View();
        }

        /// <summary>
        /// 500 Internal Server Error
        /// </summary>
        /// <returns></returns>
        public ActionResult Error500() 
        {
            Response.StatusCode = 500;
            return View();
        }
    }
}