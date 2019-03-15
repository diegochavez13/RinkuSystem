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
    public class PagosNominaController : Controller
    {
        //
        // GET: /PagosNomina/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult generarVistaPrevia(int iIdTrabajador, DateTime daFechaInicio, DateTime daFechaFin)
        {
            CRespuesta Respuesta = new CRespuesta();
            MdlPagosNomina mdlPagos = new MdlPagosNomina();

            try
            {
                Respuesta = mdlPagos.generarVistaPrevia(iIdTrabajador, daFechaInicio, daFechaFin);
            }
            catch(Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }

        public ActionResult guardarPagoTrabajador(int iIdTrabajador, DateTime daFechaInicio, DateTime daFechaFin, decimal deDespensa, decimal deISR, decimal dePagoNeto, decimal deSueldoBruto)
        {
            CRespuesta Respuesta = new CRespuesta();
            MdlPagosNomina mdlPagos = new MdlPagosNomina();
           
            try
            {
                Respuesta = mdlPagos.guardarPagoTrabajador( iIdTrabajador,  daFechaInicio,  daFechaFin,  deDespensa,  deISR,  dePagoNeto,  deSueldoBruto);
            }
            catch(Exception Ex)
            {
                Respuesta.sDescription = Ex.Message;
            }

            return Json(Respuesta, JsonRequestBehavior.AllowGet);
        }
	}
}