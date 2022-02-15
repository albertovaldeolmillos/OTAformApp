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
// Decrypt
using OTSDecrypt;
// BBDD
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
// EMAIL
using System.Web.Mail;
using System.Web.Util;
using System.Net.Mail;
using System.Text;

namespace OTAformApp
{
	/// <summary>
	/// Summary description for requestPsw.
	/// </summary>
	public partial class reqPsw : System.Web.UI.Page
	{
		protected string email = String.Empty;
        protected string nameUser = String.Empty;        
        protected string login = String.Empty;
        protected string password = String.Empty;    
        protected OTADataBase dataBase;


        protected override void InitializeCulture()
        {
            //En el global.axax se añade el valor inicial de la variable Session a "es-ES".
            if (Page.Request["ctrlHead:lang"] != null)            
                UICulture = Page.Request["ctrlHead:lang"].ToString();     
            else
            {
                //No se ha seleccionado ningun idioma con el combo
                UICulture = Page.Session["lang"].ToString();

                //Si existe, borro variables de sesion que indica que se ha seleccionado un idioma
                if (Session["requestPsw"] != null)
                    Session.Remove("requestPsw");
                if (Session["requestPswUser"] != null)
                    Session.Remove("requestPswUser");
                if (Session["requestPswMail"] != null)
                    Session.Remove("requestPswMail");
            }

            base.InitializeCulture();
        } 
        
        

        protected void Page_Load(object sender, System.EventArgs e)
		{
			// PROTEGEMOS SI INTENTAN ACCEDER DIRECTAMENTE
			if(Session.IsNewSession)
			{
				Session.Abandon();
                Page.Response.Redirect("~/index.aspx", true);
			}

            LoadCulture();

            //Traduce contenido de la página si se ha seleccionado un idioma.
            //Al seleccionar el idioma NO se produce PostBack
            if (Session["requestPsw"] != null)
            {                
                switch (Session["requestPsw"].ToString())
                {
                    case "OK":
                        //Si el usuario modifica el idioma, muestro variables de session; ya que en este caso
                        //no se realiza consulta a la BBDD para averiguar email y nombre de usuario
                        nameUser = Session["requestPswUser"].ToString();
                        email = Session["requestPswMail"].ToString();

                        divResult.InnerHtml = (string)GetLocalResourceObject("divResultHead") + nameUser + (string)GetLocalResourceObject("divResult");
                        break;
                    case "ERROR":
                        divResult.InnerHtml = (string)GetLocalResourceObject("divResultError");
                        break;
                    default:
                        break;
                }                
            }
            else
            {         
                //Comprobar datos del usuario
                if (Page.Request.Form.Count > 0 && Page.Request.Form["confEmail"] != "")
                {
                    string plate = Page.Request.Form["control"].ToString();
                    email = Page.Request.Form["confEmail"];

                    //Prevenir SQL injection
                    bool loginValid = true;   
                    if (!System.Text.RegularExpressions.Regex.IsMatch(plate, "^[A-Za-z0-9|\\040|-]{6,20}$"))
                    {
                        loginValid = false;
                    }                

                    if (!System.Text.RegularExpressions.Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                    {
                        loginValid = false;
                    }

                    if (loginValid)
                    {
                        if (getUserAccountByVal(email, plate))
                        {
                            MailSend();
                            divResult.InnerHtml = (string)GetLocalResourceObject("divResultHead") + nameUser + (string)GetLocalResourceObject("divResult");
                            //Si se quiere cambiar el idioma al contenido del <div> hay que guardar un valor que indique que tipo de respuesta
                            //guardaba el <div> ya que este se genera dinamicamente y no guarda su contenido en el viewstate. 
                            Session.Add("requestPsw", "OK");
                            //Guarda nombre y email de usuario para mostrar al seleccionar un idioma diferente. Ya que en ese caso
                            //no se produce la consulta a la BBDD
                            Session.Add("requestPswUser", nameUser);
                            Session.Add("requestPswMail", email);
                            Session.Remove("errorLog");
                        }
                        else
                        {
                            divResult.InnerHtml = (string)GetLocalResourceObject("divResultError");
                            //Si se quiere cambiar el idioma al contenido del <div> hay que guardar un valor que indique que tipo de respuesta
                            //guardaba el <div> ya que este se genera dinamicamente y no guarda su contenido en el viewstate. 
                            Session.Add("requestPsw", "ERROR");
                        }
                    }
                    else
                    {
                        //cargar div que para recuperar usuario y contraseña y avisa del error
                        Session.Add("errorLog", (string)GetLocalResourceObject("errorRecoverPassw"));
                        Page.Response.Redirect("~/index.aspx", true);
                    }
                } 
                else
                {
                    Response.Redirect("~/index.aspx", true);
                }
            }
		}



        private void LoadCulture()
        {
            locTitle.Text = (string)GetLocalResourceObject("locTitle");
            locSubTitle.Text = (string)GetLocalResourceObject("locSubTitle");
            locBody.Text = (string)GetLocalResourceObject("locBody");
        }



		private bool getUserAccountByVal(string _email, string _plate)
		{
			bool bCheck =false;
            OracleDataReader dr = null;
            OracleDataReader drPlate = null;

			try
			{
                dataBase = new OTADataBase();
                dataBase.OpenConnection();   
                string sSQL = "select MU_ID, MU_NAME, MU_SURNAME1, MU_LOGIN, MU_PASSWORD from mobile_users " +
                              "where MU_EMAIL = '" + _email + "'";
				dataBase.Cmd.CommandText = sSQL;				
				dr = dataBase.Cmd.ExecuteReader(CommandBehavior.SingleRow);
                //Validar email
				if(dr.HasRows)
				{
                    //while (dr.Read())
                    //{
                    //Posicionar cursor en primer (y único) registro
                    dr.Read();
                    //Validar matrícula
                    sSQL = String.Format("select mup_mu_id, mup_plate from mobile_users_plates " +
                                         "where mup_mu_id = {0} AND mup_plate = '{1}'", dr["MU_ID"].ToString(), _plate);
                    dataBase.Cmd.CommandText = sSQL;
                    drPlate = dataBase.Cmd.ExecuteReader(CommandBehavior.SingleResult);
                    if (drPlate.HasRows)
                    {
                        bCheck = drPlate.HasRows;
                        //Si la matricula pertenece al usuario se actualizan los valores
                        nameUser = dr["MU_NAME"].ToString() + " " + dr["MU_SURNAME1"].ToString();
                        login = dr["MU_LOGIN"].ToString();
                        //Desencriptar clave antes de enviar  
                        OTSDecrypt.OTSCreditCardDecrypt decrypt = new OTSCreditCardDecrypt();
                        password = decrypt.Decrypt(dr["MU_ID"].ToString(), dr["MU_PASSWORD"].ToString());
                    }
                    //drPlate.Close();
                    //drPlate.Dispose();
                    //drPlate = null;                           
                                            
				}
                //dr.Close();
                //dr.Dispose();
                //dr = null; 
			}
			catch(Exception )
			{
                string strError = (string)GetLocalResourceObject("ExceptionDefault");
                Response.Redirect("errorForm.aspx?Error=" + strError);				
			}
			finally
			{
                if (drPlate != null)
                {
                    drPlate.Close();
                    drPlate.Dispose();
                    drPlate = null;
                }

                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                    dr = null;
                }

                dataBase.CloseConnection();
			}
			
			return(bCheck);
		}



        private void MailSend()
        {
            try
            {      
                string bodyMessage = (string)GetLocalResourceObject("MailHead") + nameUser +
                                     (string)GetLocalResourceObject("MailBody") +
                                     (string)GetLocalResourceObject("MailUser") + login +
                                     (string)GetLocalResourceObject("MailPassword") + password +
                                     (string)GetLocalResourceObject("MailFeet");

                //Recuperamos del web.config los valores para el servidor SMTP y la ruta donde guardaremos 
                //los ficheros que subiremos al servidor para después adjuntar al correo.
                string smtpServer = System.Configuration.ConfigurationSettings.AppSettings["SMTP_SERVER"];
                //Otros parámetros de configuracion para el envío de email
                string smtpFrom = System.Configuration.ConfigurationSettings.AppSettings["SMTP_FROM"]; 
                
                System.Net.Mail.MailMessage MyMail = new System.Net.Mail.MailMessage();
                MyMail.From = new System.Net.Mail.MailAddress(smtpFrom);
                MyMail.To.Add(email);
                MyMail.Subject = (string)GetLocalResourceObject("MailSubject");
                MyMail.IsBodyHtml = true;
                MyMail.Body = bodyMessage;
                MyMail.IsBodyHtml = true;
                MyMail.Priority = System.Net.Mail.MailPriority.High;                

                //Una vez tenemos todos los datos del mail creamos un objeto SMTP para enviar el mail
                SmtpClient smtpMail = new SmtpClient(smtpServer);
                //Si el servidor de correo necesita autenticación, podemos hacerlo mediante un objeto del tipo NetworkCredential, 
                //en el que indicaremos el nombre del usuario y la clave, ese objeto lo asignaremos a la propiedad Credentials 
                //del objeto SmtpClient que acabamos de crear.
                string smtpUser = System.Configuration.ConfigurationSettings.AppSettings["SMTP_USER"];
                string smtpPassword = System.Configuration.ConfigurationSettings.AppSettings["SMTP_PASSWORD"];
                smtpMail.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);

                //Enviamos el correo, si hay algun error , éste seraá devuelto en SmtpException.
                try
                {
                    smtpMail.Send(MyMail);
                }
                catch (SmtpException ex)
                {
                    divResult.InnerText = ex.Message;
                    divResult.Style.Add("display", "none");
                }
            }
            catch (System.Web.HttpException ehttp)
            {
                divResult.InnerText = ehttp.Message;
                divResult.Style.Add("display", "none");
            }
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
