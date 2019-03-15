using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RinkuSystem.Clases
{
    public class CPagos
    {
        public int iIdTrabajador { get; set; }
        public string daFechaInicio { get; set; }
        public string daFechaFin { get; set; }
        public decimal deDespensa { get; set; }
        public decimal deISR { get; set; }
        public decimal dePagoNeto { get; set; }
        public decimal deSueldoBruto { get; set; }
    }
}