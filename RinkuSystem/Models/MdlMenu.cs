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
    public class MdlMenu : CCompartido
    {
        public CRespuesta CargarMenu()
        {
            CRespuesta Respuesta = new CRespuesta();
            List<CMenu> ListaMenu = new List<CMenu>();

            if (ejecutarStoreProcedure(ref datos, "ObtenerPantallas", new List<SqlArgumentos>(), ref sMensaje))
            {
                var query_distinct = (from c in datos.Tables[0].AsEnumerable()
                                      select new
                                      {
                                          sNombreModulo = c.Field<string>("nvNombreModulo"),
                                          iIdModulo = c.Field<int>("iIdModulo")
                                      }).Distinct().OrderBy(x => x.sNombreModulo);

                var query = (from c in datos.Tables[0].AsEnumerable()
                             select new
                             {
                                 sNombreFuncionalidad = c.Field<string>("nvNombreFuncionalidad"),
                                 iIdModulo = c.Field<int>("iIdModulo"),
                                 sAccion = c.Field<string>("nvAccion"),
                                 iIdFuncionalidad = c.Field<int>("iIdFuncionalidad")
                             }).ToList();

                foreach (var x in query_distinct)
                {
                    CMenu menu = new CMenu();
                    menu.sNombreModulo = x.sNombreModulo.ToString();
                    List<CFuncionalidades> ListaFuncionalidades = new List<CFuncionalidades>();
                    foreach (var y in query)
                    {
                        CFuncionalidades Funcionalidades = new CFuncionalidades();

                        if (y.iIdModulo.ToString() == x.iIdModulo.ToString())
                        {
                            Funcionalidades.sNombreFuncionalidad = y.sNombreFuncionalidad.ToString();
                            Funcionalidades.sAccion = y.sAccion;
                            Funcionalidades.iIdFuncionalidad = y.iIdFuncionalidad;
                            ListaFuncionalidades.Add(Funcionalidades);
                        }

                    }
                    menu.ListaFuncionalidades = ListaFuncionalidades;
                    ListaMenu.Add(menu);
                }

                if (ListaMenu.Count > 0)
                {
                    Respuesta.data = ListaMenu;
                    Respuesta.shStatus = (short)Definitions.OK_;
                }
                else
                {
                    Respuesta.shStatus = (short)Definitions.ERR_;
                }
            }
            else
            {
                Respuesta.sDescription = this.sMensaje;
            }

            return Respuesta;
        }
    }
}