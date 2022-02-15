using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
// BBDD
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace OTAformApp
{
    public partial class Intro_Data : System.Web.UI.Page
    {

        private string strError = String.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {                        
            if (Page.IsPostBack)
            {
                Page.Validate();
                if (Page.IsValid)
                {
                    if (UserValidation())    
                        //si el usuario está validado enviar a la pagina de suscripción
                        //Response.Redirect("~/SuscriptionData.aspx");
                        Response.Redirect("~/configuration.aspx");
                    else
                    {
                        //Mensaje de error
                        formError.Attributes.Add("style", "display:block;");
                        ulerrors.InnerHtml = "<li>Nombre de Usuario y/o Contraseña no son correctos.</li>";
                    }
                }
            }
           
        }


        protected bool UserValidation()
        {
            OTADataBase dataBase = new OTADataBase();
            OracleDataReader dr = null;
            bool existUser = false;

            try
            {
                //Comprobar la existencia del usuario en BBDD   
                string strFilter = String.Format("select mea_id from mobile_employee_acces where mea_login = '{0}' AND mea_password = '{1}' " + 
                                                 "AND MEA_VALID = 1 AND MEA_DELETED = 0", txtLogin.Value, txtPassword.Value);
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strFilter;
                dr = dataBase.Cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr.HasRows)
                {
                    existUser = true;                    
                    //Session["PrinterForm"] = txtLogin.Value;
                    //Usuario validado
                    Session.Add("CfgUserID", txtLogin.Value);
                }

                //dr.Close();
                //dr = null;                
            }
            catch (Exception)
            {
                if (strError == String.Empty)
                    strError = "Error en la solicitud de acceso.";

                Response.Redirect("errorForm.aspx?Error=" + strError);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                    dr = null;
                }

                dataBase.CloseConnection();
            }

            return existUser;
        }
    }
}
