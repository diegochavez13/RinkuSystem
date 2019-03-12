using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Xml.Linq;

namespace RinkuSystem.Clases
{
    public class CCompartido : CConexion
    {
        protected string sMessage;
        protected CRespuesta Respuesta;
        protected DataSet datos;
        protected XDocument xml;
        protected List<SqlArgumentos> args;

        public CCompartido()
        {
            this.sMessage = string.Empty;
            this.Respuesta = new CRespuesta();
            this.datos = new DataSet();
            this.xml = new XDocument();
            this.args = new List<SqlArgumentos>();
        }

        public CCompartido(DataBase database)
            : base(database)
        {
            this.sMessage = string.Empty;
            this.Respuesta = new CRespuesta();
            this.datos = new DataSet();
            this.xml = new XDocument();
            this.args = new List<SqlArgumentos>();
        }
    }
}