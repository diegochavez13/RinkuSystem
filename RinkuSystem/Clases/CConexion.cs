using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace RinkuSystem.Clases
{
    public class CConexionDatos
    {
        public string sNombreBD { get; set; }
        public string sNombreServidor { get; set; }
        public string sUsuario { get; set; }
        public string sContraseña { get; set; }
    }

    public class SqlArgumentos
    {
        public string sParametro { get; set; }
        public Object oValores { get; set; }
        public SqlDbType Tipo { get; set; }

        public SqlArgumentos(){ }

        public SqlArgumentos(string _sParametro, Object _oValores, SqlDbType _Tipo)
        {
            sParametro = _sParametro;
            oValores = _oValores;
            Tipo = _Tipo;
        }
    }

    public enum DataBase
    {
        RinkuDB = 1
    }

    public class CConexion
    {
        private SqlTransaction Transaccion;
        private SqlConnection Conexion;
        private SqlCommand Comando;
        private SqlDataAdapter Adaptador;

        public CConexion()
        {
            try
            {
                string sNombreServidor = ConfigurationManager.AppSettings["ConexionBD"].ToString();
                Conexion = new SqlConnection(sNombreServidor);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }
        }

        public CConexion(DataBase DataBase)
        {
            string sNombreServidor = string.Empty;

            try 
            { 
                sNombreServidor = ConfigurationManager.AppSettings["ConexionBD"].ToString();
                Conexion = new SqlConnection(sNombreServidor);
            }
            catch (Exception ex) {
                throw new ArgumentException(ex.Message, ex);
            }
           
        }

        public bool BeginTran(ref string sMensaje)
        {
            bool bRet = false;

            try 
            {
                if (this.Conexion.State != ConnectionState.Open)
                {
                    this.Conexion.Open();
                    
                }

                this.Transaccion = this.Conexion.BeginTransaction();

                bRet = true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message, ex);
            }

            return bRet;
        }

        public void Commit()
        {
            if (this.Transaccion != null)
            {
                this.Transaccion.Commit();
                this.Conexion.Close();
            }
        }

        public void RollBack()
        {
            if (this.Transaccion != null)
            {
                this.Transaccion.Rollback();
                this.Conexion.Close();
            }
        }

        public bool ejecutarStoreProcedure(ref DataSet datos, string sStoreProcedure, string sXml, ref string sMensaje, int iTimeOut = 30)
        {
            bool bRet = false;

            bool bExisteTransaccion = Transaccion != null;

            try 
            {
                if (this.Conexion.State != ConnectionState.Open)
                {
                    this.Conexion.Open();
                }

                if (bExisteTransaccion)
                {
                    Comando = new SqlCommand(sStoreProcedure, this.Conexion, this.Transaccion);
                }
                else
                {
                    Comando = new SqlCommand(sStoreProcedure, this.Conexion);
                }

                Comando.CommandType = CommandType.StoredProcedure;

                Comando.Parameters.AddWithValue("@Xml", sXml);

                Comando.CommandTimeout = iTimeOut;

                Adaptador = new SqlDataAdapter(Comando);

                Adaptador.Fill(datos);

                if (!bExisteTransaccion)
                {
                    this.Conexion.Close();
                }

                bRet = true;

            }catch(Exception ex)
            {
                sMensaje = ex.Message;

                if (bExisteTransaccion)
                {
                    this.Transaccion.Rollback();
                }
            }

            return bRet;
        }

        public bool ejecutarStoreProcedure(ref DataSet datos, string sStoreProcedure, List<SqlArgumentos> argumentos, ref string sMensaje, int iTimeout = 30)
        {
            bool bRet = false;

            bool bExisteTransaccion = Transaccion != null;

            try
            {
                if (this.Conexion.State != ConnectionState.Open)
                {
                    this.Conexion.Open();
                }

                if (bExisteTransaccion)
                {
                    Comando = new SqlCommand(sStoreProcedure, this.Conexion, this.Transaccion);
                }
                else
                {
                    Comando = new SqlCommand(sStoreProcedure, this.Conexion);
                }

                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandTimeout = iTimeout;

                foreach (SqlArgumentos arg in argumentos)
                {
                    Comando.Parameters.AddWithValue("@" + arg.sParametro, arg.Tipo);
                    Comando.Parameters["@" + arg.sParametro].Value = arg.oValores;
                }

                Adaptador = new SqlDataAdapter(Comando);

                Adaptador.Fill(datos);

                if (!bExisteTransaccion)
                {
                    this.Conexion.Close();
                }

                bRet = true;
            }
            catch (Exception ex)
            {
                sMensaje = ex.Message;

                if (bExisteTransaccion)
                {
                    this.Transaccion.Rollback();
                }

                this.Conexion.Close();
            }

            return bRet;
        }

        public bool ejecutarStoreProcedure(string sStoreProcedure, string sXml, ref string sMensaje, int iTimeout = 30)
        {
            bool bRet = false;

            bool bExisteTransaccion = Transaccion != null;

            try
            {
                if (this.Conexion.State != ConnectionState.Open)
                {
                    this.Conexion.Open();
                }

                if (bExisteTransaccion)
                {
                    Comando = new SqlCommand(sStoreProcedure, this.Conexion, this.Transaccion);
                }
                else
                {
                    Comando = new SqlCommand(sStoreProcedure, this.Conexion);
                }

                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandTimeout = iTimeout;

                Comando.Parameters.AddWithValue("@Xml", sXml);

                Comando.ExecuteNonQuery();

                if (!bExisteTransaccion)
                {
                    this.Conexion.Close();
                }

                bRet = true;
            }
            catch (Exception ex)
            {
                sMensaje = ex.Message;

                if (bExisteTransaccion)
                {
                    this.Transaccion.Rollback();
                }

                this.Conexion.Close();
            }
            return bRet;
        }

        public bool ejecutarStoreProcedure(string sStoreProcedure, List<SqlArgumentos> argumentos, ref string sMensaje, int iTimeout = 30)
        {
            bool bRet = false;

            bool bExisteTransaccion = Transaccion != null;

            try
            {
                if (this.Conexion.State != ConnectionState.Open)
                {
                    this.Conexion.Open();
                }

                if (bExisteTransaccion)
                {
                    Comando = new SqlCommand(sStoreProcedure, this.Conexion, this.Transaccion);
                }
                else
                {
                    Comando = new SqlCommand(sStoreProcedure, this.Conexion);
                }

                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandTimeout = iTimeout;

                foreach (SqlArgumentos arg in argumentos)
                {
                    Comando.Parameters.AddWithValue("@" + arg.sParametro, arg.Tipo);
                    Comando.Parameters["@" + arg.sParametro].Value = arg.oValores;
                }

                Comando.ExecuteNonQuery();

                if (!bExisteTransaccion)
                {
                    this.Conexion.Close();
                }

                bRet = true;
            }
            catch (Exception ex)
            {
                sMensaje = ex.Message;

                if (bExisteTransaccion)
                {
                    this.Transaccion.Rollback();
                }

                this.Conexion.Close();
            }


            return bRet;
        }
    }
}