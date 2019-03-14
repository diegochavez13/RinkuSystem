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
    public class MdlCapturaMovimientos : CCompartido
    {
        public CRespuesta guardarMovimientoTrabajador(int iIdTrabajador, int iEntregas, DateTime daFecha, bool bCubrioTurno)
        {
            CRespuesta oRes = new CRespuesta();

            this.args.Add(new SqlArgumentos("iIdTrabajador", iIdTrabajador, SqlDbType.Int));
            this.args.Add(new SqlArgumentos("iEntregas", iEntregas, SqlDbType.Int));
            this.args.Add(new SqlArgumentos("daFecha", daFecha, SqlDbType.DateTime));
            this.args.Add(new SqlArgumentos("bCubrioTurno", bCubrioTurno, SqlDbType.Bit));

            if (ejecutarStoreProcedure(ref datos, "guardarMovimientoTrabajador", args, ref sMensaje))
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