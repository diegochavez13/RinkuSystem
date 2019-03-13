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
    public class TrabajadorController : Controller
    {
        //
        // GET: /Empleados/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult obtenerRoles()
        {
            CRespuesta Respuesta = new CRespuesta();
            MdlTrabajador mdlTrabajador = new MdlTrabajador();

            try
            {
                Respuesta = mdlTrabajador.obtenerRoles();
                Respuesta.shStatus = (short)Definitions.OK_;
            }
            catch (Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult obtenerTiposTrabajador()
        {
            CRespuesta Respuesta = new CRespuesta();
            MdlTrabajador mdlTrabajador = new MdlTrabajador();

            try
            {
                Respuesta = mdlTrabajador.obtenerTiposTrabajador();
                Respuesta.shStatus = (short)Definitions.OK_;
            }
            catch (Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult obtenerTrabajadores()
        {
            CRespuesta Respuesta = new CRespuesta();
            MdlTrabajador mdlTrabajador = new MdlTrabajador();

            try
            {
                Respuesta = mdlTrabajador.obtenerTrabajadores();
                Respuesta.shStatus = (short)Definitions.OK_;
            }
            catch (Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult guardarTrabajador(string args)
        {
            CRespuesta Respuesta = new CRespuesta();
            MdlTrabajador mdlTrabajador = new MdlTrabajador();

            try
            {
                Respuesta = mdlTrabajador.guardarTrabajador(args);
                Respuesta.shStatus = (int)Definitions.OK_;
            }
            catch(Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }
	}
}