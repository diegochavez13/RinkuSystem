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
    public class MdlPagosNomina:CCompartido
    {
        public CRespuesta generarVistaPrevia(int iIdTrabajador, DateTime daFechaInicio, DateTime daFechaFin)
        {
            CRespuesta oRes = new CRespuesta();

            this.args.Add(new SqlArgumentos("iIdTrabajador", iIdTrabajador, SqlDbType.Int));
            this.args.Add(new SqlArgumentos("daFechaInicio", daFechaInicio, SqlDbType.DateTime));
            this.args.Add(new SqlArgumentos("daFechaFin", daFechaFin, SqlDbType.DateTime));

            if (ejecutarStoreProcedure(ref datos, "CalcularPagoTrabajador", args, ref sMensaje))
            {
                if (datos.Tables[0].Rows.Count > 0)
                {
                    var Result = (from row in datos.Tables[0].AsEnumerable()
                                  select new
                                  {
                                      deSueldoBruto = row.Field<decimal>("SueldoBruto"),
                                      deISR = row.Field<decimal>("ISR"),
                                      deDespensa = row.Field<decimal>("Despensa"),
                                      dePagoNeto = row.Field<decimal>("PagoNeto")
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

        public CRespuesta guardarPagoTrabajador(int iIdTrabajador, DateTime daFechaInicio, DateTime daFechaFin, decimal deDespensa, decimal deISR, decimal dePagoNeto, decimal deSueldoBruto)
        {
            CRespuesta oRes = new CRespuesta();
            this.args.Add(new SqlArgumentos("iIdTrabajador", iIdTrabajador, SqlDbType.Int));
            this.args.Add(new SqlArgumentos("daFechaInicio", daFechaInicio, SqlDbType.DateTime));
            this.args.Add(new SqlArgumentos("daFechaFin", daFechaFin, SqlDbType.DateTime));
            this.args.Add(new SqlArgumentos("deDespensa", deDespensa, SqlDbType.Decimal));
            this.args.Add(new SqlArgumentos("deISR", deISR, SqlDbType.Decimal));
            this.args.Add(new SqlArgumentos("dePagoNeto", dePagoNeto, SqlDbType.Decimal));
            this.args.Add(new SqlArgumentos("deSueldoBruto", deSueldoBruto, SqlDbType.Decimal));

            if (ejecutarStoreProcedure(ref datos, "guardarPagoTrabajador", args, ref sMensaje))
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