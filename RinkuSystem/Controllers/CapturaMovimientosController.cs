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
    public class CapturaMovimientosController : Controller
    {
        //
        // GET: /CapturaMovimientos/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult guardarMovimientoTrabajador(int iIdTrabajador, int iEntregas, DateTime daFecha, bool bCubrioTurno)
        {
            CRespuesta Respuesta = new CRespuesta();
            MdlCapturaMovimientos mdlCapturaMovimientos = new MdlCapturaMovimientos();
          
            try
            {
                Respuesta = mdlCapturaMovimientos.guardarMovimientoTrabajador(iIdTrabajador, iEntregas, daFecha, bCubrioTurno);
            }
            catch (Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }
	}
}