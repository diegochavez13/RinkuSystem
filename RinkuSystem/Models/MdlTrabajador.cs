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
        public CRespuesta obtenerRoles()
        {
            CRespuesta oRes = new CRespuesta();

            if (ejecutarStoreProcedure(ref datos, "ObtenerRoles", new List<SqlArgumentos>(), ref sMensaje))
            {
                if (datos.Tables[0].Rows.Count > 0)
                {
                    var Result = (from row in datos.Tables[0].AsEnumerable()
                                     select new
                                     {
                                         iID = row.Field<int>("iID"),
                                         nvRol = row.Field<string>("nvRol")
                                     }).ToList();

                    oRes.data = Result;
                    oRes.shStatus = (short)Definitions.OK_;
                }
                else
                {
                    oRes.shStatus = (short)Definitions.NO_DATOS;
                }
            }
            else
            {
                oRes.sDescription = this.sMensaje;
            }
            return oRes;
        }

        public CRespuesta obtenerTiposTrabajador()
        {
            CRespuesta oRes = new CRespuesta();

            if (ejecutarStoreProcedure(ref datos, "ObtenerTiposTrabajador", new List<SqlArgumentos>(), ref sMensaje))
            {
                if (datos.Tables[0].Rows.Count > 0)
                {
                    var Result = (from row in datos.Tables[0].AsEnumerable()
                                  select new
                                  {
                                      iID = row.Field<int>("iID"),
                                      nvTipo = row.Field<string>("nvTipo")
                                  }).ToList();

                    oRes.data = Result;
                    oRes.shStatus = (short)Definitions.OK_;
                }
                else
                {
                    oRes.shStatus = (short)Definitions.NO_DATOS;
                }
            }
            else
            {
                oRes.sDescription = this.sMensaje;
            }
            return oRes;
        }

        public CRespuesta obtenerTrabajadores()
        {
            CRespuesta oRes = new CRespuesta();

            if (ejecutarStoreProcedure(ref datos, "obtenerTrabajadores", new List<SqlArgumentos>(), ref sMensaje))
            {
                if (datos.Tables[0].Rows.Count > 0)
                {
                    var Result = (from row in datos.Tables[0].AsEnumerable()
                                  select new
                                  {
                                      iID = row.Field<int>("iID"),
                                      nvNombre = row.Field<string>("nvNombre"),
                                      iRol = row.Field<int>("iRol"),
                                      nvRol = row.Field<string>("nvRol"),
                                      iTipo = row.Field<int>("iTipo"),
                                      nvTipo = row.Field<string>("nvTipo"),
                                      iEstatus = row.Field<bool>("iEstatus"),
                                      nvEstatus = row.Field<string>("nvEstatus")
                                  }).ToList();

                    oRes.data = Result;
                    oRes.shStatus = (short)Definitions.OK_;
                }
                else
                {
                    oRes.shStatus = (short)Definitions.NO_DATOS;
                }
            }
            else
            {
                oRes.sDescription = this.sMensaje;
            }
            return oRes;
        }

        public CRespuesta guardarTrabajador(string datos)
        {
            CRespuesta oRes = new CRespuesta();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<CTrabajador> lstTrabajador = new List<CTrabajador>();

            lstTrabajador = serializer.Deserialize<List<CTrabajador>>(datos);

            xml.Add(new XElement("Datos",
                from item in lstTrabajador
                    select new XElement( "Trabajador",
                           new XElement("iID", item.iID),
                           new XElement("nvNombre", item.nvNombre),
                           new XElement("iRol", item.iRol),
                           new XElement("iTipo", item.iTipo),
                           new XElement("iEstatus", item.iEstatus)
                    )));


            if (ejecutarStoreProcedure("guardarTrabajador", xml.ToString(), ref sMensaje))
            {
                 oRes.shStatus = (short)Definitions.OK_;
            }
            else
            {
                oRes.sDescription = this.sMensaje;
            }
            return oRes;
        }
    }
}