using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC3_Displaying_Image.Models;

namespace MVC3_Displaying_Image.Controllers
{
    public class ImageDataController : Controller
    {

        DataAcceess objContext;

        public ImageDataController()
        {
            objContext = new DataAcceess(); 
        }

        //
        // GET: /ImageData/

        public ActionResult Index()
        {
            var Images = objContext.GetImages();
            return View(Images);
        }

    }
}
