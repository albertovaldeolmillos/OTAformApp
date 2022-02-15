using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Resources;
using System.Reflection;
using System.Globalization;
//using System.Reflection;
using System.Threading;

// DDBB
using Oracle.ManagedDataAccess.Client;


namespace OTAformApp
{

    public enum ConfigurationMode
    {
        NotMode = 0,
        Suscription = 1,
        NewUser = 2,
        EmailAutomtization = 3,
        Success = 4,
        EmailStatus = 5,
        ModifyUser = 6,
        Exit = 7
    }



    public enum CommandMode
    {
        NotMode = 0,
        SaveDataEmail = 1,
        DeleteEmailControls = 2,
        SaveDataNewUser = 3,
        DeleteNewUserControls = 4,
        FilterStatusEmail = 5,
        ModifyDataUser = 6,
        NewSuscription = 7
    }

    
    public class OTADataBase
    {
        public static string ConnexionString = ConfigurationManager.AppSettings["ConnectionString"];   
        private OracleConnection _cnn;
        private OracleCommand _cmd;

        #region Propertys

        public OracleConnection Cnn
        {
            get { return _cnn; }
            set { _cnn = value; }
        }
        
        public OracleCommand Cmd
        {
            get { return _cmd; }
            set { _cmd = value; }
        }

        #endregion

        #region Methods
        public void OpenConnection()
        {
            try
            {
                Cnn = new OracleConnection(ConnexionString);
                Cmd = new OracleCommand();

                if (Cnn == null)
                    throw new Exception("Error en la cadena de conexion a la BBDD.");
                
                Cmd.Connection = Cnn;
                Cmd.Connection.Open();
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception("Error al abrir la conexion a la BBDD. " + ex.Message);
            }            
        }


        public void CloseConnection()
        {
            try
            {       
                if (Cmd != null)
                {                    
                    Cmd.Dispose();
                    Cmd = null;
                }
                if (Cnn != null)
                {
                    Cnn.Close();
                    Cnn.Dispose();
                    Cnn = null;
                }	
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cerrar la conexión a la BBDD. " + ex.Message);
            }
        }


        public IDataReader SelectMethod(string strCommand)
        {
            OracleDataReader dr = null;
            try
            {
                OpenConnection();
                Cmd.CommandText = strCommand;
                dr = Cmd.ExecuteReader();                

                return dr;
            }
            catch (Exception ex)
            {
                CloseConnection();
                throw new Exception("Error al recuperar los datos de la BBDD. " + ex.Message);
            }            
        }
        #endregion
    }      
}
