using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
// BBDD
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace OTAformApp
{

    /// <summary>
    /// Message status enumeration type
    /// </summary>
    public enum StateMessage
    {
        Pendiente = 0,
        Procesado = 10,
        Enviado = 20,
        Fallo = 30,
        Cancelado = 40,
        Errores = 55
    };    
    
    
    
    public partial class configuration : System.Web.UI.Page
    {
        private int pageMode = (int)ConfigurationMode.NotMode;
        private string strMsgEmail = String.Empty;        
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Expires = -1;
            Response.Cache.SetNoStore();

            // PROTEGEMOS SI INTENTAN ACCEDER DIRECTAMENTE
            if (Session.IsNewSession)
            {
                Session.RemoveAll();
                Session.Abandon();
                Page.Response.Redirect("~/Intro_Data.aspx", true);
            }

            if (Session["CfgUserID"] == null)
            {           
                Session.RemoveAll();
                Session.Abandon();
                Page.Response.Redirect("~/Intro_Data.aspx", true);
            }

            if (!Page.IsPostBack)
            {     
                GetAction();                
            }
        }

                

        protected void LinkButton_Command(Object sender, CommandEventArgs e) 
        {
            switch (Convert.ToInt32(e.CommandArgument.ToString()))
            {
                case (int)ConfigurationMode.EmailAutomtization:
                    pageMode = (int)ConfigurationMode.EmailAutomtization;
                    break;
                case (int)ConfigurationMode.NewUser:
                    pageMode = (int)ConfigurationMode.NewUser;
                    break;
                case (int)ConfigurationMode.Suscription:
                    pageMode = (int)ConfigurationMode.Suscription;
                    break;
                case (int)ConfigurationMode.EmailStatus:
                    pageMode = (int)ConfigurationMode.EmailStatus;
                    break;
                case (int)ConfigurationMode.ModifyUser:
                    pageMode = (int)ConfigurationMode.ModifyUser;
                    break;
                case (int)ConfigurationMode.Exit:
                    pageMode = (int)ConfigurationMode.Exit;
                    break;
                default:
                    pageMode = (int)ConfigurationMode.NotMode;
                    break;
            }

            GetAction();
        }

        

        private void GetAction()
        {
            try
            {
                //Limpiar errores   
                ulerrors.InnerHtml = String.Empty;
                formError.Attributes.Add("style", "display:hidden;"); 

                switch (pageMode)
                {
                    case (int)ConfigurationMode.EmailAutomtization:
                        divEnvioEmail.Visible = true;
                        divAltaUsuario.Visible = false;
                        divSuccess.Visible = false;
                        divEstadoEmail.Visible = false;
                        divModificarUsuario.Visible = false;
                        divSuscription.Visible = false;

                        DeleteControls(divEnvioEmail.ID);
                        if (ddlUsuarios.Items.Count == 0)
                            LoadUsers(ddlUsuarios);
                        break;
                    case (int)ConfigurationMode.NewUser:
                        divEnvioEmail.Visible = false;
                        divAltaUsuario.Visible = true;
                        divSuccess.Visible = false;
                        divEstadoEmail.Visible = false;
                        divModificarUsuario.Visible = false;
                        divSuscription.Visible = false;

                        DeleteControls(divAltaUsuario.ID);
                        break;
                    case (int)ConfigurationMode.Suscription:
                        //Response.Redirect("~/SuscriptionData.aspx");
                        divSuscription.Visible = true;
                        divEnvioEmail.Visible = false;
                        divAltaUsuario.Visible = false;
                        divSuccess.Visible = false;
                        divEstadoEmail.Visible = false;
                        divModificarUsuario.Visible = false;

                        DeleteControls(divSuscription.ID);
                        // Carga control con tramos de caducidad para la tarjeta de credito.
                        LoadCardExpireYear();
                        // Carga control con nombres de compañías de móviles
                        LoadMobileCompanies();
                        
                        //Valor provisional para el campo LOGIN. Después de validar los datos entrados por el usuario
                        //este campo coindidirá con el NIF del usuario.
                        txtUsuario.Text = "Automatico";                        
                        break;
                    case (int)ConfigurationMode.Success:
                        divEnvioEmail.Visible = false;
                        divAltaUsuario.Visible = false;
                        divSuccess.Visible = true;
                        divEstadoEmail.Visible = false;
                        divModificarUsuario.Visible = false;
                        divSuscription.Visible = false;
                        break;
                    case (int)ConfigurationMode.EmailStatus:
                        divEnvioEmail.Visible = false;
                        divAltaUsuario.Visible = false;
                        divSuccess.Visible = false;
                        divEstadoEmail.Visible = true;
                        divModificarUsuario.Visible = false;
                        divSuscription.Visible = false;

                        DeleteControls(divEstadoEmail.ID);
                        if(ddlUserStatusEmail.Items.Count != 0)
                            ddlUserStatusEmail.Items.Clear();
                        LoadUsers(ddlUserStatusEmail);                    
                        break;
                    case (int)ConfigurationMode.ModifyUser:
                        divEnvioEmail.Visible = false;
                        divAltaUsuario.Visible = false;
                        divSuccess.Visible = false;
                        divEstadoEmail.Visible = false;
                        divModificarUsuario.Visible = true;
                        divSuscription.Visible = false;

                        DeleteControls(divModificarUsuario.ID);
                        if (ddlModifySelectName.Items.Count != 0)
                            ddlModifySelectName.Items.Clear();
                            LoadUsersAdministrators(ddlModifySelectName);
                        break;
                    case (int)ConfigurationMode.Exit:  
                        Session.RemoveAll();
                        Session.Abandon();
                        Page.Response.Redirect("~/Intro_Data.aspx", true);
                        break;
                    default:
                        divEnvioEmail.Visible = false;
                        divAltaUsuario.Visible = false;
                        divSuccess.Visible = false;
                        divEstadoEmail.Visible = false;
                        divModificarUsuario.Visible = false;
                        divSuscription.Visible = false;
                        break;
                }
            }
            catch (Exception)
            {
                MessageResponse(String.Empty);
            }
        }



        protected void Button_Command(Object sender, CommandEventArgs e)
        {
            string strSQL = String.Empty;
            bool success = false;
            string strErrValidation = String.Empty;
            
            try
            {               
                switch (Convert.ToInt32(e.CommandArgument.ToString()))
                {
                    case (int)CommandMode.SaveDataEmail:
                        //Guardar datos de Automatizacion de Email
                        strSQL = GetCommand((int)CommandMode.SaveDataEmail);
                        success = SaveData(strSQL);
                        MessageResponse(success);
                        break;
                    //case (int)CommandMode.DeleteEmailControls:
                    //    //Borrar valores de controles de Automatizacion de Email
                    //    DeleteControls(divEnvioEmail.ID);
                    //    break;
                    case (int)CommandMode.SaveDataNewUser:
                        //Guardar datos de Alta de Usuario
                        strSQL = GetCommand((int)CommandMode.SaveDataNewUser);
                        success = SaveData(strSQL);
                        MessageResponse(success);
                        break;
                    //case (int)CommandMode.DeleteNewUserControls:
                    //    //Borrar valores de controles de Alta de Usuario
                    //    DeleteControls(divAltaUsuario.ID);
                    //    break;  
                    case (int)CommandMode.FilterStatusEmail:                        
                        strSQL = GetCommand((int)CommandMode.FilterStatusEmail);
                        //Mostrar Resultado en GridView
                        LoadGridView(strSQL);
                        break;
                    case (int)CommandMode.ModifyDataUser:
                        //Guardar datos de Usuario
                        strSQL = GetCommand((int)CommandMode.ModifyDataUser);
                        success = SaveData(strSQL);
                        MessageResponse(success);
                        break;
                    case (int)CommandMode.NewSuscription:
                        //Guardar datos de la Suscripcion
                        //strSQL = GetCommand((int)CommandMode.NewSuscription);
                        //success = SaveData(strSQL);
                        //MessageResponse(success);

                        bool validateData = true;
                        string errorValidation = String.Empty;
                        string sUsuario = String.Empty;

                        //Si el usuario contiene el valor "Automatico", se sustituye por su NIF                        
                        if (txtUsuario.Text.Equals("Automatico"))
                        {
                            sUsuario = txtNIF.Text;
                        }
                        else
                        {
                            sUsuario = txtUsuario.Text;
                        }

                        if (LoginValidation(sUsuario))
                        {
                            validateData = false;
                            errorValidation = "<li><a href=\"#email\">El nombre de usuario ya existe.</a></li>";
                        }
                        if (EmailValidation())
                        {
                            validateData = false;
                            errorValidation += "<li><a href=\"#email\">El correo electr&oacute;nico ya existe.</a></li>";
                            
                        }

                        if (validateData)
                        {
                            //Limpiar errores   
                            ulerrors.InnerHtml = errorValidation;
                            formError.Attributes.Add("style", "display:hidden;"); 

                            int newID = GetNewID((int)CommandMode.NewSuscription);
                            string sPswd = GeneratePassword();
                            
                            if (InsertUser(newID, sUsuario, sPswd) >= 0)
                            {
                                string ActivationID = InsertActivationAccount(newID);
                                InsertPlates(newID);
                                //Enviar email de activación de la suscripción
                                MailSend(ActivationID, sPswd);
                                //Cargar Idioma
                                //LoadCulture(); 
                                //Informar del resultado de la transacción
                                MessageResponse("Los datos se han guardado correctamente." + System.Environment.NewLine + strMsgEmail);
                            }
                            else
                            {
                                MessageResponse(false);
                            }
                        }
                        else
                        {
                            //Informar que el email ya existe eb la BBDD   
                            ulerrors.InnerHtml = errorValidation;
                            formError.Attributes.Add("style", "display:block !important;");  
                                                        
                            //Valor provisional para el campo LOGIN. Después de validar los datos entrados por el usuario
                            //este campo coindidirá con el NIF del usuario.
                            txtUsuario.Text = "Automatico";
                        }
                    break;
                }
            }
            catch (Exception )
            {   
                MessageResponse(String.Empty);                
            }
        }


        private string GeneratePassword()
        {
            //Este nuevo atributo servirá para indicarle desde javascript
            //que muestre el contenido del campo contraseña, generado automaticamente.
            string autoLogPassw = Guid.NewGuid().ToString();
            char[] separator = { '-' };
            string[] logPassw = autoLogPassw.Split(separator);

            return logPassw[0].ToString() + logPassw[1].ToString();            
        }


        //private void ViewPassword(string strPassword)
        //{
        //    //Este nuevo atributo servirá para indicarle desde javascript
        //    //que el usuario ha iniciado session y está editando sus datos,
        //    //es decir, no se trata de un nuevo usuario.  

        //    //string valor = Page.Request.Form["password"].ToString();
        //    //string valor = strPassword;
        //    txtPsw.Attributes.Add("password", strPassword);
        //    ClientScriptManager cScript = Page.ClientScript;
        //    cScript.RegisterStartupScript(this.GetType(), "LoadPasswordField", "<script>loadPasswordCfg();</script>");
        //}


        private int InsertUser(int MU_ID, string MU_LOGIN, string MU_PASSWORD)
        {
            int insUser = 0;

            string MU_NAME = Page.Request.Form["txtNameS"].ToString().Replace("'", "''");
            string MU_SURNAME1 = Page.Request.Form["txtSurname1S"].ToString().Replace("'", "''");
            string MU_SURNAME2 = Page.Request.Form["txtSurname2S"].ToString().Replace("'", "''");
            string MU_DNI = Page.Request.Form["txtNIF"].ToString().Replace(" ", "").Replace("-", "").Replace(".", "");
            string MU_EMAIL = Page.Request.Form["txtEmail"].ToString().Replace("'", "''");
            string MU_ADDR_STREET = Page.Request.Form["txtStreet"].ToString().Replace("'", "''");

            string MU_ADDR_NUMBER = Page.Request.Form["txtStreetNum"].ToString().Replace("'", "''");
            string MU_ADDR_LEVEL = Page.Request.Form["txtPiso"].ToString().Replace("'", "''");
            string MU_ADDR_STAIR = Page.Request.Form["txtEscalera"].ToString().Replace("'", "''");
            string MU_DOOR_NUMBER = Page.Request.Form["txtPuerta"].ToString().Replace("'", "''");

            string MU_ADDR_LETTER = Page.Request.Form["txtLetra"].ToString().Replace("'", "''");
            string MU_ADDR_POSTAL_CODE = Page.Request.Form["txtCP"].ToString().Replace("'", "''");
            string MU_ADDR_CITY = Page.Request.Form["txtLocalidad"].ToString().Replace("'", "''");
            string MU_ADDR_PROVINCE = Page.Request.Form["txtProvincia"].ToString().Replace("'", "''");
            string MU_ADDR_COUNTRY = Page.Request.Form["txtCountry"].ToString().Replace("'", "''");
            string MU_NUM_CREDIT_CARD = Page.Request.Form["txtNumTarjeta"].ToString().ToString().Replace(" ", "").Replace("-", "");
            string MU_NAME_CARD = Page.Request.Form["ddlTipoTarjeta"].ToString().Replace("'", "''");
            DateTime MU_NUM_CC_EXPIRATION_DATE = new DateTime(Convert.ToInt32(Page.Request.Form["ddlAnyoCad"]), Convert.ToInt32(Page.Request.Form["ddlMesCad"]), 1);
            string MU_MOBILE_TELEPHONE = Page.Request.Form["txtMobile"].ToString().Replace(" ", "").Replace("-", "");
            int MU_MOBILE_COMPANY = Convert.ToInt32(Page.Request.Form["ddlMobileCompany"].ToString());
            int MU_PAY_PROFILE = 0; // ?
            int MU_PENDING_PAYMENTS = 0; // ? 
            
            //string MU_PASSWORD = Page.Request.Form["txtPsw"].ToString().Replace("'", "''");            
            int MU_CAN_MODIFY_PLATES = 1; //?

            string[] MU_PLATES = new string[] { };
            char[] separator = new Char[] { };
            MU_PLATES = Page.Request.Form["txtMatriculas"].ToString().Split(separator);                                    
            
            OTADataBase dataBase = null;

            try
            {
                string tipeDate = MU_NUM_CC_EXPIRATION_DATE.ToString();

                string sSQL = " insert into MOBILE_USERS t (mu_id, mu_name, mu_surname1, mu_surname2, mu_dni, mu_email, mu_addr_street, mu_addr_number, mu_addr_level, mu_addr_stair, mu_addr_letter, mu_addr_postal_code, mu_addr_city, mu_addr_province, mu_addr_country, mu_num_credit_card, mu_name_card, mu_num_cc_expiration_date, mu_mobile_telephone, mu_mobile_company, mu_pay_profile, mu_pending_payments, mu_login, mu_password, mu_version, mu_valid, mu_deleted, mu_can_modify_plates, mu_num_credit_card_mask, mu_activate_account, mu_door_number)";
                sSQL += " VALUES( " + MU_ID + ", INITCAP('" + MU_NAME + "'),INITCAP('" + MU_SURNAME1 + "'),INITCAP('" + MU_SURNAME2 + "'),UPPER('" + MU_DNI + "'),'" + MU_EMAIL + "','";
                sSQL += MU_ADDR_STREET + "','" + MU_ADDR_NUMBER + "','" + MU_ADDR_LEVEL + "','" + MU_ADDR_STAIR + "','";
                sSQL += MU_ADDR_LETTER + "','" + MU_ADDR_POSTAL_CODE + "','" + MU_ADDR_CITY + "','" + MU_ADDR_PROVINCE + "','";
                sSQL += MU_ADDR_COUNTRY + "','" + MU_NUM_CREDIT_CARD + "',UPPER('" + MU_NAME_CARD + "'),TO_DATE('" + MU_NUM_CC_EXPIRATION_DATE.ToString("dd/MM/yyyy") + "','dd/mm/yyyy'),";
                sSQL += "'" + MU_MOBILE_TELEPHONE + "'," + MU_MOBILE_COMPANY + "," + MU_PAY_PROFILE + "," + MU_PENDING_PAYMENTS + ",'";
                sSQL += MU_LOGIN + "','" + MU_PASSWORD + "', 1 , 1, 0 ," + MU_CAN_MODIFY_PLATES + ", null, 0, '" + MU_DOOR_NUMBER + "') ";

                dataBase = new OTADataBase();
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = sSQL;
                insUser = dataBase.Cmd.ExecuteNonQuery();
                if (insUser <= 0)
                    throw new Exception();
            }
            catch (Exception)
            {                
                throw new Exception();
            }
            finally
            {
                dataBase.CloseConnection();
            }

            return insUser;
        }


        private void InsertPlates(int userID)
        {
            OTADataBase dataBase = null;

            string[] MU_PLATES = new string[] { };
            char[] separator = new Char[] { };
            MU_PLATES = Page.Request.Form["txtMatriculas"].ToString().Split(separator);

            try
            {
                for (int n = 0; n < MU_PLATES.Length; n++)
                {
                    string strSQL;
                    string plateNum = MU_PLATES[n].ToString().Replace(" ", "").Replace("-", "");
                    int MUP_NUM_OPERATIONS = 0;

                    if (plateNum != "" && plateNum.Length >= 6)
                    {
                        strSQL = " insert into MOBILE_USERS_PLATES ";
                        strSQL += "(mup_mu_id, mup_plate, mup_num_operations, mup_version, mup_valid, mup_deleted, mup_id) ";
                        strSQL += "VALUES ( " + userID + ",'" + MU_PLATES[n].ToString() + "'," + MUP_NUM_OPERATIONS + ",1,1,0," + (userID+n) + " ) ";
                        
                        dataBase = new OTADataBase();
                        dataBase.OpenConnection();
                        dataBase.Cmd.CommandText = strSQL;
                        int confIns = dataBase.Cmd.ExecuteNonQuery();                        
                        if (confIns <= 0)
                            throw new Exception();
                    }
                }
            }
            catch (Exception)
            {                
                throw new Exception();
            }
            finally
            {
                dataBase.CloseConnection();
            }
        }


        private string InsertActivationAccount(int UserId)
        {
            //Insertar registro temporal para activación de cuenta por email
            OTADataBase dataBase = null;
            string ActivationID = String.Empty;
            string strCmd = String.Empty;
            try
            {
                ActivationID = Guid.NewGuid().ToString();
                strCmd = " insert into MOBILE_USERS_ACTIVATION (mu_activation_key, mu_id, mu_email_date)";
                strCmd += " VALUES( '" + ActivationID + "', " + UserId + ", TO_DATE('" + DateTime.Now.ToString("dd/MM/yyyy") + "','dd/mm/yyyy'))";

                dataBase = new OTADataBase();
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strCmd;

                int activationReg = dataBase.Cmd.ExecuteNonQuery();
                if (activationReg <= 0)
                {                    
                    throw new Exception();
                }
            }
            catch (Exception)
            {                
                throw new Exception();
            }
            finally
            {
                dataBase.CloseConnection();
            }

            return ActivationID;
        }


        private void MailSend(string ActivationID, string MU_PASSWORD)
        {
            string MU_EMAIL = Page.Request.Form["txtEmail"].ToString().Replace("'", "''");
            string MU_NAME = Page.Request.Form["txtNameS"].ToString().Replace("'", "''");
            string MU_SURNAME1 = Page.Request.Form["txtSurname1S"].ToString().Replace("'", "''");            

            try
            {
                string urlActivation = System.Configuration.ConfigurationSettings.AppSettings["APP_URL"];
                string linkActivation = "<a href=\"" + urlActivation + "ActivationAccount.aspx?AccountID=" + ActivationID + "\" target=\"_blank\">";
                string bodyMessage = (string)GetLocalResourceObject("EmailHead") + MU_NAME + " " + MU_SURNAME1 +
                                     (string)GetLocalResourceObject("EmailBody") + linkActivation +
                                     (string)GetLocalResourceObject("EmailAccountID");

                


                //En caso que la suscripción fuera mediante formulario impreso se envía un email que contiene
                //la contraseña generada automaticamente, además de su Login, que coincide con su número de NIF/CIF
                //pero que no se especificará en el email por motivos de seguridad. 
                if (txtUsuario.Text.Equals("Automatico"))
                {
                    bodyMessage += (string)GetLocalResourceObject("EmailBodyPrinterFormPassword") + MU_PASSWORD +
                                   (string)GetLocalResourceObject("EmailBodyPrinterForm");
                }
                else
                {
                    bodyMessage += (string)GetLocalResourceObject("EmailBodyPrinterFormLogin") + txtUsuario.Text +
                                   (string)GetLocalResourceObject("EmailBodyPrinterFormPsw") + MU_PASSWORD +
                                   (string)GetLocalResourceObject("EmailBodyPrinterForm");
                }

                bodyMessage += (string)GetLocalResourceObject("EmailFeet");

                //Recuperamos del web.config los valores para el servidor SMTP y la ruta donde guardaremos 
                //los ficheros que subiremos al servidor para después adjuntar al correo.
                string smtpServer = System.Configuration.ConfigurationSettings.AppSettings["SMTP_SERVER"];
                //Otros parámetros de configuracion para el envío de email
                string smtpFrom = System.Configuration.ConfigurationSettings.AppSettings["SMTP_FROM"];

                System.Net.Mail.MailMessage MyMail = new System.Net.Mail.MailMessage();
                MyMail.From = new System.Net.Mail.MailAddress(smtpFrom);
                MyMail.To.Add(MU_EMAIL.ToString());
                MyMail.Subject = (string)GetLocalResourceObject("EmailSubject");
                MyMail.IsBodyHtml = true;
                MyMail.Body = bodyMessage;
                MyMail.IsBodyHtml = true;
                MyMail.Priority = System.Net.Mail.MailPriority.High;

                //Una vez tenemos todos los datos del mail creamos un objeto SMTP para enviar el mail
                System.Net.Mail.SmtpClient smtpMail = new System.Net.Mail.SmtpClient(smtpServer);

                //Si el servidor de correo necesita autenticación, podemos hacerlo mediante un objeto del tipo NetworkCredential, 
                //en el que indicaremos el nombre del usuario y la clave, ese objeto lo asignaremos a la propiedad Credentials 
                //del objeto SmtpClient que acabamos de crear.
                string smtpUser = System.Configuration.ConfigurationSettings.AppSettings["SMTP_USER"];
                string smtpPassword = System.Configuration.ConfigurationSettings.AppSettings["SMTP_PASSWORD"];
                smtpMail.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);

                //Enviamos el correo, si hay algun error, éste seraá devuelto en SmtpException.
                try
                {
                    smtpMail.Send(MyMail);
                }
                catch (System.Net.Mail.SmtpException eSmtp)
                {
                    //emailMsg.InnerText = ex.Message;
                    //emailMsg.Style.Add("display", "none");                    
                    strMsgEmail = "Ocurrió el siguiente error en el envío del email: " + eSmtp.Message;
                }
            }
            catch (System.Web.HttpException ehttp)
            {
                //emailMsg.InnerText = ehttp.Message;
                //emailMsg.Style.Add("display", "none");
                strMsgEmail = "Ocurrió el siguiente error en el envío del email: " + ehttp.Message;
            }
            catch (Exception)
            {
                strMsgEmail = "Ocurrió un error en el envío del email de activación.";
            }

            strMsgEmail = "Se envió al solicitante un email de activación.";
        }



        private bool EmailValidation()
        {
            OracleDataReader dr = null;
            OTADataBase dataBase = new OTADataBase();
            HttpResponse httResp = HttpContext.Current.Response;
            bool existEmail = false;
            string MU_EMAIL = Page.Request.Form["txtEmail"].ToString().Replace("'", "''");

            try
            {
                string strFilter = "SELECT MU_ID, MU_EMAIL FROM  mobile_users WHERE MU_EMAIL = '" + MU_EMAIL + "'";
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strFilter;
                dr = dataBase.Cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                if (dr.HasRows)
                {
                    existEmail = true;                    
                }

                //dr.Close();
                //dr.Dispose();
                //dr = null;
            }
            catch (Exception)
            {                               
                throw new Exception();
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

            return existEmail;
        }


        private bool LoginValidation(string sLogin)
        {
            OracleDataReader dr = null;
            OTADataBase dataBase = new OTADataBase();
            HttpResponse httResp = HttpContext.Current.Response;
            bool existLogin = false;

            try
            {
                string strFilter = "SELECT MU_ID, MU_LOGIN FROM  mobile_users WHERE MU_LOGIN = '" + sLogin + "'";
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strFilter;
                dr = dataBase.Cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
                if (dr.HasRows)
                {
                    existLogin = true;                    
                }

                //dr.Close();
                //dr.Dispose();
                //dr = null;
            }
            catch (Exception)
            {
                throw new Exception();
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

            return existLogin;
        }


         private void LoadGridView(string filter)
         {
             OTADataBase dataBase = new OTADataBase();
             OracleDataReader dr = null;
             try
             {
                 dataBase.OpenConnection();
                 dataBase.Cmd.CommandText = filter;
                 dr = dataBase.Cmd.ExecuteReader();
                 
                 gvEmailStatus.DataSource = dr;
                 gvEmailStatus.DataBind();

                 if (!dr.HasRows)
                 {
                     locEmailStatusMessage.Visible = true;
                     locEmailStatusMessage.Text = "No hay datos para mostrar";
                 }
                 else
                 {
                     locEmailStatusMessage.Visible = false;                     

                     //Asignar formato a celdas
                     for (int i = 0; i < gvEmailStatus.Rows.Count; i++)
                     {                         
                         //Formatear mensaje de estado
                         gvEmailStatus.Rows[i].Cells[3].Text = GetStatusMessage(Convert.ToInt32(gvEmailStatus.Rows[i].Cells[3].Text));
                         //formato fecha 
                         gvEmailStatus.Rows[i].Cells[2].Text = Convert.ToDateTime(gvEmailStatus.Rows[i].Cells[2].Text).ToShortDateString();
                         //Establecer ancho de columnas  
                         ClientScriptManager cScript = Page.ClientScript;
                         cScript.RegisterStartupScript(this.GetType(),"CellsFormat","<script>WidthCells();</script>");   
                     }                     
                 }                 

                //dr.Close();
                //dr.Dispose();
                //dr = null;
            }
            catch (Exception)
            {                                
                throw new Exception();
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
         }



         private string GetStatusMessage(int status)
         {
             string message = String.Empty;
             switch (status)
             {
                 case (int)StateMessage.Cancelado:
                     message = "Cancelado";
                     break;
                 case (int)StateMessage.Enviado:
                     message = "Enviado";
                     break;
                 case (int)StateMessage.Errores:
                     message = "Con errores";
                     break;
                 case (int)StateMessage.Fallo:
                     message = "Fallo al enviar";
                     break;
                 case (int)StateMessage.Pendiente:
                     message = "Pendiente";
                     break;
                 case (int)StateMessage.Procesado:
                     message = "En proceso";
                     break;  
             }

             return message;
         }



        
        private void MessageResponse(bool success)
        {
            pageMode = (int)ConfigurationMode.Success;
            GetAction();            
            
            //Mensaje Error
            if(!success)
                lblSuccess.Text = "Error al intentar guardar los datos.";

            //Mensaje Exito
            else
            lblSuccess.Text = "Los datos se han guardado correctamente.";
        }


        private void MessageResponse(string success)
        {
            pageMode = (int)ConfigurationMode.Success;
            GetAction();

            //Mensaje Error
            if (success == String.Empty)
                lblSuccess.Text = "Error durante el proceso. Inténtelo de nuevo.";            
            else
                lblSuccess.Text = success;
        }



        private string GetCommand(int valueArgument)
        {            
            int id = 0;
            int language = 1;
            int rol = 8;
            string strSQL = String.Empty;

            try
            {     
                switch (valueArgument)
                {
                    case (int)CommandMode.SaveDataEmail:
                        if (Convert.ToBoolean(Session["ExistUserEmail"].ToString()))                        
                            strSQL = String.Format("UPDATE REPORT_SEND_SCHEDULER " + 
                                                   "SET RSS_CUS_ID = {0}, " +
                                                   "RSS_DAY = {1}, " +                                           
                                                   "RSS_HOUR = TO_DATE('{2}','HH24:MI:SS'), " +
                                                   "RSS_PERIOD = {3}, " +
                                                   "RSS_REPORT_PERIOD = {4}, " +                                           
                                                   "RSS_ENABLED = {5}, " +                                           
                                                   "RSS_SUBJECT = '{6}', " +
                                                   "RSS_BODY = '{7}', " +
                                                   "RSS_LAN_ID = {8} " +
                                                   "WHERE RSS_CUS_ID = {0}",
                                                   ddlUsuarios.SelectedValue, 
                                                   ddlDiaEnvio.SelectedValue, 
                                                   Convert.ToDateTime(txtHoraEnvio.Text).ToLongTimeString(),
                                                   ddlSenderPeriod.SelectedValue, 
                                                   ddlSenderData.SelectedValue,
                                                   Convert.ToInt32(chkSenderActivation.Checked), 
                                                   txtSubject.Text, 
                                                   txtBody.Text, 
                                                   ddlIdioma.SelectedValue);  
                        else
                            strSQL = String.Format("INSERT INTO REPORT_SEND_SCHEDULER " +
                                               "(RSS_CUS_ID, RSS_DAY, RSS_HOUR, RSS_PERIOD, RSS_REPORT_PERIOD, " +
                                               "RSS_LAST_SEND, RSS_ENABLED, RSS_FORCE_SEND, RSS_SUBJECT, RSS_BODY, " +
                                               "RSS_LAN_ID, RSS_REP_FORMAT)" +
                                               "VALUES ({0}, {1}, TO_DATE('{2}','HH24:MI:SS'), {3}, {4}, TO_DATE('{5}','DD/MM/YYYY'), {6}, {7}, '{8}', '{9}', {10}, {11})",
                                               ddlUsuarios.SelectedValue, 
                                               ddlDiaEnvio.SelectedValue, 
                                               Convert.ToDateTime(txtHoraEnvio.Text).ToLongTimeString(),
                                               ddlSenderPeriod.SelectedValue, 
                                               ddlSenderData.SelectedValue, 
                                               DateTime.Now.ToString("dd/MM/yyyy"),
                                               Convert.ToInt32(chkSenderActivation.Checked), 
                                               0, 
                                               txtSubject.Text, 
                                               txtBody.Text, 
                                               ddlIdioma.SelectedValue, 
                                               1);                
                        break;                
                    case (int)CommandMode.SaveDataNewUser:
                        id = GetNewID((int)CommandMode.SaveDataNewUser);
                        strSQL = String.Format("INSERT INTO mobile_employee_acces " +
                                               "(mea_id, mea_name, mea_surname1, mea_surname2, mea_rol_id, mea_login, mea_password, " +
                                               "mea_lan_id, mea_status, mea_version, mea_valid, mea_deleted) " +
                                               "values ({0}, '{1}', '{2}', '{3}', {4}, '{5}', '{6}', {7}, 1, 1, 1, 0)",
                                                id, txtName.Text, txtSurname1.Text, txtSurname2.Text, rol, txtLogin.Text, txtPassword.Text, language);
                        break;
                    case (int)CommandMode.FilterStatusEmail:
                        id = Convert.ToInt32(ddlUserStatusEmail.SelectedValue);
                        strSQL = String.Format("SELECT mu_email AS Email , " +
                                               "rrw_file AS Archivo, " +
                                               "rrw_date AS Fecha, " + 
                                               "rrw_state AS Estado " + 
                                               "FROM REPORT_SEND_OUTBOXES_WORK " +
                                               "INNER JOIN REPORT_SEND_OUTBOXES " + 
                                               "ON REPORT_SEND_OUTBOXES_WORK.RRW_RSO_ID = REPORT_SEND_OUTBOXES.RSO_ID " +
                                               "INNER JOIN MOBILE_USERS ON MOBILE_USERS.MU_ID = REPORT_SEND_OUTBOXES_WORK.RRW_RCP_ID " +
                                               "WHERE REPORT_SEND_OUTBOXES_WORK.RRW_RCP_ID = {0}", id);

                        if (txtFechaIni.Text != String.Empty)
                            strSQL += String.Format(" AND rrw_date >= TO_DATE('{0} 00:00:00','DD/MM/YYYY HH24:MI:SS')", txtFechaIni.Text);
                        if (txtFechaFin.Text != String.Empty)
                            strSQL += String.Format(" AND rrw_date <= TO_DATE('{0} 23:59:59','DD/MM/YYYY HH24:MI:SS')", txtFechaFin.Text);
                        break;                
                    case (int)CommandMode.ModifyDataUser:
                        id = Convert.ToInt32(ddlModifySelectName.SelectedValue);
                        strSQL = String.Format("UPDATE mobile_employee_acces " +
                                               "SET mea_name = '{0}', " + 
                                               "mea_surname1 = '{1}', " + 
                                               "mea_surname2 = '{2}', " +
                                               "mea_rol_id = {3}, " + 
                                               "mea_login = '{4}', " + 
                                               "mea_password = '{5}', " +
                                               "mea_lan_id = {6}, " +
                                               "mea_status = {7}, " + 
                                               "mea_version = {8}, " + 
                                               "mea_valid = {9}, " + 
                                               "mea_deleted = {10} " +
                                               "WHERE mea_id = {11}",
                                                txtModifyName.Text, 
                                                txtModifySurname1.Text, 
                                                txtModifySurname2.Text, 
                                                rol, 
                                                txtModifyUser.Text, 
                                                txtModifyPassword.Text, 
                                                language,
                                                1, 
                                                1,
                                                Convert.ToInt32(chkModifyActiveUser.Checked),
                                                0,
                                                id);
                        break;
                    case (int)CommandMode.NewSuscription:
                        
                        break;
                }

            }
            catch (Exception)
            {
                throw new Exception();
            }
            
            return strSQL;
        }



        private int GetNewID(int valueArgument)
        {
            int newID = 0;
            string StrCommand = String.Empty;
            OTADataBase dataBase = new OTADataBase();            

            switch (valueArgument)
            {
                case (int)CommandMode.SaveDataEmail:
                    //StrCommand = "SELECT MAX(mea_id) FROM mobile_employee_acces";
                    break;                
                case (int)CommandMode.SaveDataNewUser:
                    StrCommand = "SELECT MAX(mea_id) FROM mobile_employee_acces";
                    break;
                case (int)CommandMode.NewSuscription:
                    StrCommand = "SELECT MAX(MU_ID) AS ID FROM MOBILE_USERS";
                    break;
            }       

            try
            {                
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = StrCommand;
                //Comprobar si hay registros en la tabla
                string result = dataBase.Cmd.ExecuteScalar().ToString();
                if(result != String.Empty)
                    newID = Convert.ToInt32(result);
                
                newID += 1;
            }
            catch (Exception)
            {                
                throw new Exception();
            }
            finally
            {                
                dataBase.CloseConnection();
            } 

            return newID;
        }



        private bool SaveData(string strCommand)
        {            
            bool result = false;
            OTADataBase dataBase = new OTADataBase();

            try
            {                    
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strCommand;
                int saveData = dataBase.Cmd.ExecuteNonQuery();
                if (saveData > 0)
                    result = true;
            }
            catch (Exception )
            {                
                throw new Exception();
            }
            finally
            {
                dataBase.CloseConnection();
            }            

            return result;
        }



        private void DeleteControls(string containerName)
        {            
            HtmlGenericControl container = (HtmlGenericControl)this.FindControl(containerName);
            
            try
            {
                foreach (Control control in container.Controls)
                {        
                    if (control.GetType().ToString() == "System.Web.UI.WebControls.TextBox")                    
                        ((TextBox)control).Text = String.Empty;
                    if (control.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                        ((CheckBox)control).Checked = false;
                    if (control.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                        ((DropDownList)control).SelectedIndex = -1;
                    if (control.GetType().ToString() == "System.Web.UI.WebControls.GridView")
                    {
                        ((GridView)control).DataSource = "";
                        ((GridView)control).DataBind();
                    }
                    if (control.GetType().ToString() == "System.Web.UI.WebControls.Localize")
                    {
                        if (control.ID == "locEmailStatusMessage")
                            ((Localize)control).Visible = false;
                    }
                }
            }
            catch (Exception )
            {                
                throw new Exception();
            }            
        }



        private void LoadUsers(DropDownList ddlist)
        {            
            OTADataBase dataBase = new OTADataBase();
            OracleDataReader dr = null;

            try
            {
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = String.Format("SELECT mu_id, mu_name, mu_surname1, mu_surname2 FROM mobile_users " + 
                                                         "WHERE mu_valid = 1 AND mu_deleted = 0 AND mu_activate_account = 1");
                dr = dataBase.Cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr.HasRows)
                {
                    string completeName = String.Empty;
                    ListItem lstItem = new ListItem();

                    //Se añade el primer item que será informativo
                    lstItem.Text = "";
                    lstItem.Value = "0";
                    //ddlUsuarios.Items.Add(lstItem);
                    ddlist.Items.Add(lstItem);
                    
                    while (dr.Read())
                    {
                        completeName = dr["mu_name"].ToString() + " " + dr["mu_surname1"].ToString() + " " + dr["mu_surname2"].ToString();
                        lstItem = new ListItem();
                        lstItem.Text = completeName;
                        lstItem.Value = dr["mu_id"].ToString();
                        //ddlUsuarios.Items.Add(lstItem);
                        ddlist.Items.Add(lstItem);
                    }
                }

                //dr.Close();
                //dr.Dispose();
                //dr = null;
            }
            catch (Exception )
            {
                throw new Exception();
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
        }



        private void LoadUsersAdministrators(DropDownList ddlist)
        {
            OTADataBase dataBase = new OTADataBase();
            OracleDataReader dr = null;

            try
            {
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = String.Format("SELECT mea_id, mea_name, mea_surname1, mea_surname2 FROM mobile_employee_acces");   

                dr = dataBase.Cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (dr.HasRows)
                {
                    string completeName = String.Empty;
                    ListItem lstItem = new ListItem();

                    //Se añade el primer item que será informativo
                    lstItem.Text = "";
                    lstItem.Value = "0";
                    //ddlUsuarios.Items.Add(lstItem);
                    ddlist.Items.Add(lstItem);

                    while (dr.Read())
                    {
                        completeName = dr["mea_name"].ToString() + " " + dr["mea_surname1"].ToString() + " " + dr["mea_surname2"].ToString();
                        lstItem = new ListItem();
                        lstItem.Text = completeName;
                        lstItem.Value = dr["mea_id"].ToString();
                        //ddlUsuarios.Items.Add(lstItem);
                        ddlist.Items.Add(lstItem);
                    }
                }

                //dr.Close();
                //dr.Dispose();
                //dr = null;
            }
            catch (Exception )
            {
                throw new Exception();
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
        }


        // Carga tramos de caducidad para la tarjeta de credito
        protected void LoadCardExpireYear()
        {
            try
            {            
                if (ddlAnyoCad.Items.Count == 1)
                {
                    DateTime nowDate = DateTime.Now;
                    int roundDate = nowDate.Year % 5;
                    for (int i = 0; i <= 15; i++)
                    {
                        DateTime NextDate = nowDate.AddYears(i - roundDate);
                        ListItem itemMenu = new ListItem(NextDate.Year.ToString(), NextDate.Year.ToString());
                        ddlAnyoCad.Items.Add(itemMenu);
                    }
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }


        //Carga control con nombres de compañías de móviles
        protected void LoadMobileCompanies()
        {
            if (ddlMobileCompany.Items.Count == 1)
            {
                OTADataBase dataBase = null;
                OracleDataReader dr = null;

                try
                {                    
                    dataBase = new OTADataBase();
                    dataBase.OpenConnection();
                    dataBase.Cmd.CommandText = " SELECT MUCD_ID,MUCD_DESCLONG FROM MOBILE_USERS_COMPANY_DEF " +
                                               "where MUCD_VALID = 1 and MUCD_DELETED=0 ";
                    dr = dataBase.Cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ListItem CompanyCard = new ListItem(dr["MUCD_DESCLONG"].ToString(), dr["MUCD_ID"].ToString());
                        ddlMobileCompany.Items.Add(CompanyCard);
                    }
                    dr.Close();
                    dr.Dispose();
                    dr = null;
                }
                catch (Exception)
                {                                       
                    throw new Exception();
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
            }
        }



        //Al seleccionar el usuario se cargan los datos en los controles
        protected void ddlUsuarios_IndexChanged(Object sender, EventArgs e)
        {
            OTADataBase dataBase = new OTADataBase();
            OracleDataReader dr = null;
            string strFilter = String.Empty;

            try
            {
                strFilter = String.Format("SELECT * FROM REPORT_SEND_SCHEDULER WHERE RSS_CUS_ID = {0}", ddlUsuarios.SelectedValue);
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strFilter;

                dr = dataBase.Cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.HasRows)
                {
                    //Usuario existente para modificar datos de envio automatico de email                    
                    dr.Read();
                    //BindData(dr);
                }

                BindData(dr);

                SetExistUserEmail(dr.HasRows);

                //dr.Close();
                //dr.Dispose();
                //dr = null;
            }
            catch (Exception )
            {
                //throw new Exception();
                MessageResponse(String.Empty);
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

        }




         //Al seleccionar el usuario se cargan los datos en los controles
        protected void ddlModifySelectName_IndexChanged(Object sender, EventArgs e)
        {
            OTADataBase dataBase = new OTADataBase();
            OracleDataReader dr = null;
            string strFilter = String.Empty;

            try
            {                
                strFilter = String.Format("SELECT * FROM mobile_employee_acces WHERE mea_id = {0}", ddlModifySelectName.SelectedValue);
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strFilter;

                dr = dataBase.Cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.HasRows)
                {
                    //Usuario-Administrador existente en la base de datos                    
                    dr.Read();                    
                }

                BindDataModifyUser(dr);

                dr.Close();
                dr.Dispose();
                dr = null;
            }
            catch (Exception )
            {
                //throw new Exception();
                MessageResponse(String.Empty);
            }
            finally
            {
                dataBase.CloseConnection();
            }            

        }




        private void SetExistUserEmail(bool existUser)
        {
            if (Session["ExistUserEmail"] == null)
                Session.Add("ExistUserEmail", existUser);
            else
                Session["ExistUserEmail"] = existUser;  
        }



        private void BindData(OracleDataReader dr)
        {
            if (dr.HasRows)
            {
                ddlDiaEnvio.Text = dr["RSS_DAY"].ToString();
                txtHoraEnvio.Text = Convert.ToDateTime(dr["RSS_HOUR"].ToString()).ToLongTimeString();
                ddlSenderPeriod.Text = dr["RSS_PERIOD"].ToString();
                ddlSenderData.Text = dr["RSS_REPORT_PERIOD"].ToString();
                ddlIdioma.Text = dr["RSS_LAN_ID"].ToString();
                chkSenderActivation.Checked = Convert.ToBoolean(Convert.ToInt32(dr["RSS_ENABLED"].ToString()));
                txtSubject.Text = dr["RSS_SUBJECT"].ToString();
                txtBody.Text = dr["RSS_BODY"].ToString();
            }
            else
            {
                ddlDiaEnvio.Text = "0";
                txtHoraEnvio.Text = String.Empty;
                ddlSenderPeriod.Text = "0";
                ddlSenderData.Text = "0";
                ddlIdioma.Text = "0";
                chkSenderActivation.Checked = false;
                txtSubject.Text = String.Empty;
                txtBody.Text = String.Empty;
            }
        }



        private void BindDataModifyUser(OracleDataReader dr)
        {
            if (dr.HasRows)
            {
                txtModifyName.Text = dr["mea_name"].ToString();                
                txtModifySurname1.Text = dr["mea_surname1"].ToString();
                txtModifySurname2.Text = dr["mea_surname2"].ToString();
                chkModifyActiveUser.Checked = Convert.ToBoolean(Convert.ToInt32(dr["mea_valid"].ToString()));
                txtModifyUser.Text = dr["mea_login"].ToString(); 
                txtModifyPassword.Text = dr["mea_password"].ToString();                
            }
            else
            {

                txtModifyName.Text = String.Empty;
                txtModifySurname1.Text = String.Empty;
                txtModifySurname2.Text = String.Empty;
                chkModifyActiveUser.Checked = false;
                txtModifyUser.Text = String.Empty;
                txtModifyPassword.Text = String.Empty;
            }
        }     


        
    }
}
