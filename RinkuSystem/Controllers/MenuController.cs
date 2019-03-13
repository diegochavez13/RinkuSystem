using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using RinkuSystem.Models;
using RinkuSystem.Clases;

namespace RinkuSystem.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult cargarFecha()
        {
            CRespuesta Respuesta = new CRespuesta();

            try
            {
                Respuesta.data = DateTime.Now.Day + "/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                Respuesta.shStatus = (short)Definitions.OK_;
            }
            catch (Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CargarMenu()
        {
            CRespuesta Respuesta = new CRespuesta();

            try
            {
                MdlMenu mdlMenu = new MdlMenu();
                Respuesta = mdlMenu.CargarMenu();
            }
            catch (Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }
	}
}