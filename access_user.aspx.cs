using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using OTSDecrypt;

// BBDD
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace OTAformApp
{
	/// <summary>
	/// Summary description for access_user.
	/// </summary>
	public partial class access_user : System.Web.UI.Page
	{
        
        protected override void InitializeCulture()
        {
            //En el global.axax se añade el valor inicial de la variable Session a "es-ES".
            if (Page.Request["ctrlHead:lang"] != null)
                UICulture = Page.Request["ctrlHead:lang"].ToString();
            else
                UICulture = Page.Session["lang"].ToString();

            base.InitializeCulture();
        } 


        protected void Page_Load(object sender, System.EventArgs e)
		{
			// PROTEGEMOS SI INTENTAN ACCEDER DIRECTAMENTE
			if(Session.IsNewSession)
			{
				Session.RemoveAll();
				Session.Abandon();
                Page.Response.Redirect("~/index.aspx", true);
			}
			// Reset Error Login
			Session.Remove("errorLog");
			
			// Put user code to initialize the page here
			if(Page.Request.Form.Count>0 && Page.Request.Form["inpUser"]!= "" && Page.Request.Form["inpPswd"]!= "")
			{
                bool loginValid = true;
                string inpUser = Page.Request.Form["inpUser"];
                string inpPswd = Page.Request.Form["inpPswd"];
                
                //Prevenir SQL injection
                if (!System.Text.RegularExpressions.Regex.IsMatch(inpUser, "^[A-Za-z0-9]{4,20}$"))
                {
                    loginValid = false;
                }                

                if (!System.Text.RegularExpressions.Regex.IsMatch(inpPswd, "^[A-Za-z0-9]{6,20}$"))
                {
                    loginValid = false;
                }

                if (loginValid)
                {
                    if (getUserAccount(inpUser, inpPswd))
                    {
                        CUser user = new CUser();

                        user.SetUserData(Session["userID"].ToString());

                        if (Double.Parse(user.Funds) <= 0)
                        {
                            // redirect logged
                            Page.Session.Add("bLogged", "true");
                            Page.Response.Redirect("GetFunds.aspx");
                        }
                        else
                        {
                            // redirect logged
                            Page.Session.Add("bLogged", "true");
                            Page.Response.Redirect("fact_oper.aspx");
                        }
                    }
                    else
                    {
                        //cargar div que para recuperar usuario y contraseña y avisa del error
                        Session.Add("errorLog", (string)GetLocalResourceObject("errorLog"));
                        Page.Response.Redirect("~/index.aspx", true);
                    }
                }
                else
                {
                    //cargar div que para recuperar usuario y contraseña y avisa del error
                    Session.Add("errorLog", (string)GetLocalResourceObject("errorLog"));
                    Page.Response.Redirect("~/index.aspx", true);
                }
			}
			else
			{
				Session.RemoveAll();
				Session.Abandon();
				// redirect init
                Page.Response.Redirect("~/index.aspx", true);
			}
		}

		/// <summary>
		/// getUserAccount
		/// Start Session
		/// </summary>
		/// <param name="user">String User Request</param>
		/// <param name="pass">String Password Request</param>
		/// <returns></returns>
		private bool getUserAccount(string user, string pass)
		{
			bool bResult = false;
            OTADataBase dataBase = null;
            OracleDataReader dr = null;
            string strError = String.Empty;

			try
			{
                dataBase = new OTADataBase();
                dataBase.OpenConnection();  
				string sSQL;
				//sSQL = String.Format("select * from mobile_users t where MU_LOGIN = '{0}' and MU_PASSWORD = '{1}' AND MU_VALID = 1 AND MU_DELETED = 0",user,pass);
                //No puede buscarse por la columna password porque esta está encriptada.
                sSQL = String.Format("select MU_ID, MU_NAME, MU_SURNAME1, MU_PASSWORD, MU_ACTIVATE_ACCOUNT from mobile_users " +
                                     "where MU_LOGIN = '{0}' AND MU_VALID = 1 AND MU_DELETED = 0", user);
				dataBase.Cmd.CommandText = sSQL;    
                dr = dataBase.Cmd.ExecuteReader(CommandBehavior.SingleResult);                

				if(dr.HasRows)
				{
                    //Desencriptar password para compararlo con el del usuario
                    OTSDecrypt.OTSCreditCardDecrypt decrypt = new OTSCreditCardDecrypt();
                    string mu_password;                    
					
                    while (dr.Read())
					{
                        mu_password = decrypt.Decrypt(dr.GetInt32(0).ToString(), dr.GetString(3));
                        if (string.Compare(pass, mu_password) == 0)
                        {
                            /*
                            bResult = dr.HasRows;
                            Session["userName"] = dr["MU_NAME"].ToString() + " " + dr["MU_SURNAME1"].ToString();
                            Session["userID"] = dr["MU_ID"].ToString();
                            //Verificar que el usuario ha activado su cuenta
                            if (dr["MU_ACTIVATE_ACCOUNT"].ToString() != "1")
                            {
                                //strError = "No ha activado su cuenta de usuario.";
                                strError = (string)GetLocalResourceObject("Exception1");
                                throw new Exception();
                            }                            

                            bResult = dr.HasRows;
                            */
                            
                            //Verificar que el usuario ha activado su cuenta
                            if (dr["MU_ACTIVATE_ACCOUNT"].ToString() == "1")
                            {
                                bResult = dr.HasRows;
                                Session["userName"] = dr["MU_NAME"].ToString() + " " + dr["MU_SURNAME1"].ToString();
                                Session["userID"] = dr["MU_ID"].ToString();                                
                            }
                            else
                            {                                
                                Session.RemoveAll();
                                Session.Abandon();
                                
                                //strError = "No ha activado su cuenta de usuario.";
                                strError = (string)GetLocalResourceObject("Exception1");
                                throw new Exception();
                            }
                        }
					}                    
				}    

                //dr.Close();
                //dr.Dispose();
                //dr = null;
			}
			catch(Exception ex)
			{
                if (strError == String.Empty)
                    //strError = "Error de acceso a su cuenta de usuario.";
                    strError = (string)GetLocalResourceObject("ExceptionDefault");

                Response.Redirect("errorForm.aspx?Error=" + strError + "&ErrorType=1");                
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
			
			return bResult;
		}


        

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
