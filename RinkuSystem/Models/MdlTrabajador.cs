using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json; 
using System.Configuration;
using RinkuSystem.Clases;
using System.Globalization;

namespace RinkuSystem.Models
{
    public class MdlTrabajador:CCompartido
    {
        public CRespuesta ObtenerRolesTrabajador()
        {
            CRespuesta oRes = new CRespuesta();

            return new CRespuesta();
        }

        public CRespuesta ObtenerTiposTrabajador()
        {
            CRespuesta oRes = new CRespuesta();

            return new CRespuesta();
        }
    }
}