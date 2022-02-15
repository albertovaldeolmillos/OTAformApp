using System;
using System.Web;
using System.Web.UI;
using Oracle.ManagedDataAccess.Client;
using OTSDecrypt;
using System.Data;
using System.Net.Mail;


namespace OTAformApp
{
    public partial class SuscripctionForm : System.Web.UI.Page
    {        
        protected ClientScriptManager cScript;

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

            string operation = "";

            try
            {
                //Evitar que al darle "anterior" al navegador no cargue la pagina
                Response.Cache.SetNoStore();

                // PROTEGEMOS SI INTENTAN ACCEDER DIRECTAMENTE
                if (Session.IsNewSession)
                {
                    Session.RemoveAll();
                    Session.Abandon();
                    Page.Response.Redirect("~/index.aspx", true);
                }

                // For Validate Form
                if (Page.IsPostBack)
                {
                    Page.Validate();

                    if (Page.IsValid)
                    {
                        CUser user = new CUser();
                        string strErrValidation = "";


                        if (user.EmailExist(this.email.Value) && Session["userID"] == null)
                        {
                            strErrValidation = "<li><a href=\"#email\">El correo electr&oacute;nico ya existe.</a></li>";
                        }

                        if (user.UserNameExist(this.user.Value) && Session["userID"] == null)
                        {
                            strErrValidation += "<li><a href=\"#email\">El nombre de usuario ya existe.</a></li>";
                        }

                        if (strErrValidation != "")
                        {
                            ulerrors.InnerHtml = strErrValidation;
                            formError.Attributes.Add("style", "display:block !important;");
                            
                        }
                        else
                        {

                            user.SetUserData(Page.Request.Form);

                            if (Session["userID"] != null)
                            {
                                user.Id = Session["userID"].ToString();
                                user.Update();
                                user.RegisterPlates();

                                operation = "update";
                            }
                            else
                            {
                                user.Insert();
                                user.RegisterPlates();

                                MailSend(user.Token, user.Name, user.Surname1, user.Email);

                                Session["userID"] = user.Id;

                                operation = "insert";
                            }
                        }
                    }
                    else
                    {
                        formError.Attributes.Add("style", "display:block !important;");
                    }

                }
                else
                {

                    //Caso en que los datos se precargan en el formulario para poder después modificarlos


                    LoadCulture();
                  
                    //Usuario logado 
                    if (Session["userID"] != null)
                    {
                        CUser user = new CUser();

                        user.SetUserData(Session["userID"].ToString());

                        BindData(user);
                    }
                }
            }
            catch (Exception)
            {
                Response.Redirect("errorForm.aspx?Error=Error al acceder al formulario.");
            }

            if (operation.Length != 0 && Session["userID"] != null)
            {
                Server.Transfer("Summary.aspx?Action=" + operation );

            }



            #region Debug Presetings
#if DEBUG

            if (Session["userID"] == null)
            {
                name.Text = "Name";
                surname1.Value = "Surname";
                surname2.Value = "Surname2";
                DNINIF.Value = "dni";
                email.Value = "email";
                address.Value = "address";
                addressNum.Value = "addressNum";
                level.Value = "level";
                door.Value = "door";
                stair.Value = "stair";
                letter.Value = "letter";
                PostCode.Value = "PostCode";
                city.Value = "city";
                province.Value = "province";
                country.Value = "country";
                telephone.Value = "telephone1";
                telephone2.Value = "telephone2";
                plates.Value = "1222CD";
                user.Value = "user";
                password.Value = "123123";
                passwordChk.Value = "123123";
                email.Value = "user@test.";
            }

#endif

            #endregion

        }

        protected override void Render(HtmlTextWriter output)
        {
            try
            {
                //Se desactiva la comprobacion de estado del botón (Id='submit') entre postback,
                //Puesto que cambia su estado disabled (de true a false) por medio de código cliente
                //y no de servidor. Hay otra opción, no recomendable, mediante la directiva de 
                //página "<%@ Page EnableEventValidation="true" %>" pero ésta, desactiva
                //la comprobación de toda la página restando seguridad a la misma.
                cScript = Page.ClientScript;
                cScript.RegisterForEventValidation("submit");
                base.Render(output);
            }
            catch (Exception)
            {
                Response.Redirect("errorForm.aspx?Error=Error al acceder al formulario.");
            }
        }
        
        protected string DecrypCard(string stringDecrypt)
        {
            //Desencriptar nombre de tarjeta de crédito            
            string strSQL = String.Empty;
            string nameCardDecrypt = String.Empty;
            OTADataBase dataBase = new OTADataBase();

            try
            {
                strSQL = String.Format("select MU_ID from mobile_users where MU_ID = '{0}' AND MU_VALID = 1 " +
                                       "AND MU_DELETED = 0", Session["userID"].ToString());
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strSQL;
                int id = Convert.ToInt32(dataBase.Cmd.ExecuteScalar());
                if (id > 0)
                {
                    OTSDecrypt.OTSCreditCardDecrypt decrypt = new OTSCreditCardDecrypt();
                    nameCardDecrypt = decrypt.Decrypt(id.ToString(), stringDecrypt);
                }
            }
            catch (Exception)
            {
                throw new Exception("Error de acceso a los datos.");
            }
            finally
            {
                dataBase.CloseConnection();
            }

            return nameCardDecrypt;
        }

        protected void BindData(CUser user)
        {

            name.Text = user.Name;
            surname1.Value = user.Surname1;
            surname2.Value = user.Surname2;
            DNINIF.Value = user.DNINIF;
            email.Value = user.Email;
            address.Value = user.Address;
            addressNum.Value = user.AddressNum;
            level.Value = user.Level;
            door.Value = user.Door;
            stair.Value = user.Stair;
            letter.Value = user.Letter;
            PostCode.Value = user.PostCode;
            city.Value = user.City;
            province.Value = user.Province;
            country.Value = user.Country;
            telephone.Value = user.Telephone1;
            telephone2.Value = user.Telephone2;
            this.user.Value = user.UserName;
            //token.Value = row["MU_TOKEN"].ToString();
            //Este nuevo atributo servirá para indicarle desde javascript
            //que el usuario ha iniciado session y está editando sus datos,
            //es decir, no se trata de un nuevo usuario.
             
            passwordChk.Value = user.Password;
            //password.Value =  user.Password;

            password.Attributes.Add("password", user.Password);
            cScript = Page.ClientScript;
            cScript.RegisterStartupScript(this.GetType(), "LoadPasswordField", "<script>loadPassword();</script>");

            plates.Value = String.Empty;
            

            for (int i = 0; i < user.Plates.Length; i++)
            {
                    if (i != 0)
                    {
                        plates.Value += "\n";
                    }
                
                    plates.Value +=  user.Plates[i];


            }


         
        
        }
        
        private void LoadCulture()
        {
            if (Session["userID"] != null)
                //Modificación de literales de encabezado de la página: Se sustituye texto de ALTA por MODIFICACION
                HeadText.InnerHtml = (string)GetLocalResourceObject("HeadTextModify");

            else
                HeadText.InnerHtml = (string)GetLocalResourceObject("HeadText");

            linkDNINIF.InnerHtml = (string)GetLocalResourceObject("linkDNINIF");
            rfvName.Text = (string)GetLocalResourceObject("rfvName");
            rfvSurname1.Text = (string)GetLocalResourceObject("rfvSurname1");

            locLegend.Text = (string)GetLocalResourceObject("locLegend");
            locCaptionTablePersonal.Text = (string)GetLocalResourceObject("locCaptionTablePersonal");
            lblName.Text = (string)GetLocalResourceObject("lblName");
            lblSurname1.Text = (string)GetLocalResourceObject("lblSurname1");
            lblSurname2.Text = (string)GetLocalResourceObject("lblSurname2");
            lblTelephone.Text = (string)GetLocalResourceObject("lblTelephone");
            //optTelCom.Text = (string)GetLocalResourceObject("optTelCom");
            lblTelephone2.Text = (string)GetLocalResourceObject("lblTelephone2");

            //Tabla razón social
            locCaptionTableFact.Text = (string)GetLocalResourceObject("locCaptionTableFact");
            lblAdress.Text = (string)GetLocalResourceObject("lblAdress");
            lblLevel.Text = (string)GetLocalResourceObject("lblLevel");
            lblStair.Text = (string)GetLocalResourceObject("lblStair");
            lblPostCode.Text = (string)GetLocalResourceObject("lblPostCode");
            lblProvince.Text = (string)GetLocalResourceObject("lblProvince");
            lblCountry.Text = (string)GetLocalResourceObject("lblCountry");
            ////locTableCard.Text = (string)GetLocalResourceObject("locTableCard");
            ////lblCardType.Text = (string)GetLocalResourceObject("lblCardType");
            ////optCardType.Text = (string)GetLocalResourceObject("optCardType");
            ////lblcardNum.Text = (string)GetLocalResourceObject("lblcardNum");
            ////lblCardMonthExpire.Text = (string)GetLocalResourceObject("lblCardMonthExpire");
            ////optCardMonthExpire.Text = (string)GetLocalResourceObject("optCardMonthExpire");
            ////optCardYearExpire.Text = (string)GetLocalResourceObject("optCardYearExpire");
            //Tabla Datos de la suscripcion
            locTableRegistration.Text = (string)GetLocalResourceObject("locTableRegistration");
            lblPlate.Text = (string)GetLocalResourceObject("lblPlate");
            lblUser.Text = (string)GetLocalResourceObject("lblUser");
            lblPassword.Text = (string)GetLocalResourceObject("lblPassword");
            lblChkPassword.Text = (string)GetLocalResourceObject("lblChkPassword");
            lblEmail.Text = (string)GetLocalResourceObject("lblEmail");
            //Check Condiciones
            locConditions.Text = (string)GetLocalResourceObject("locConditions");
            submit.InnerText = (string)GetLocalResourceObject("submit");
            reset.InnerText = (string)GetLocalResourceObject("reset");

            spFieldOblig1.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig2.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig3.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig4.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            /*spFieldOblig5.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));*/
            spFieldOblig6.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig7.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig8.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig9.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            ////spFieldOblig10.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            ////spFieldOblig11.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            ////spFieldOblig12.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig13.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig14.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig15.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig16.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig17.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
            spFieldOblig18.Attributes.Add("title", (string)GetLocalResourceObject("spFieldOblig"));
        }

        private void MailSend(string activationID, string name, string surname1, string email)
        {
            try
            {


                //Create Body
                string urlActivation = System.Configuration.ConfigurationSettings.AppSettings["APP_URL"];
                string linkActivation = "<a href=\"" + urlActivation + "ActivationAccount.aspx?TokenID=" + activationID + "\" target=\"_blank\" />";
                string bodyMessage = (string)GetLocalResourceObject("EmailHead") + name + " " + surname1 +
                                     (string)GetLocalResourceObject("EmailBody") + linkActivation +
                                     (string)GetLocalResourceObject("EmailAccountID") +
                                     (string)GetLocalResourceObject("EmailAlternativeURL") + urlActivation + "ActivationAccount.aspx?TokenID=" + activationID + "</p>" +
                                     (string)GetLocalResourceObject("EmailBodyDistinctiveMsg");


                //En caso que la suscripción fuera mediante formulario impreso se envía un email que contiene
                //la contraseña generada automaticamente, además de su Login, que coincide con su número de NIF/CIF
                //pero que no se especificará en el email por motivos de seguridad.
                //if (Session["PrinterForm"] != null)                    
                //    bodyMessage += (string)GetLocalResourceObject("EmailBodyPrinterFormPassword") + MU_PASSWORD +
                //                   (string)GetLocalResourceObject("EmailBodyPrinterForm");

                bodyMessage += (string)GetLocalResourceObject("EmailFeet");

                /*TGA@ INTENTO DE INSERTAR IMAGEN */

                // http://www.fuzzydev.com/blogs/dotnet/archive/2006/07/23/System_Net_Mail_AlternateView_LinkedResource.aspx

                //TGA@ INSERT IMAGE
                /*
                string path = Server.MapPath(@"images/adhesivocristal_90.png"); // my logo is placed in images folder
                LinkedResource logo = new LinkedResource(path,"image/png");
                logo.ContentId = "adhesivocristal";
                */
                //HTML Alternative Email Content
                /*
	            AlternateView objHTLMAltView = AlternateView.CreateAlternateViewFromString(
				bodyMessage, 
				new System.Net.Mime.ContentType("text/html"));
                objHTLMAltView.LinkedResources.Add(logo);
                */
                /*TGA@ INTENTO DE INSERTAR IMAGEN */



                //Recuperamos del web.config los valores para el servidor SMTP y la ruta donde guardaremos 
                //los ficheros que subiremos al servidor para después adjuntar al correo.
                string smtpServer = System.Configuration.ConfigurationSettings.AppSettings["SMTP_SERVER"];
                //Otros parámetros de configuracion para el envío de email
                string smtpFrom = System.Configuration.ConfigurationSettings.AppSettings["SMTP_FROM"];

                System.Net.Mail.MailMessage MyMail = new System.Net.Mail.MailMessage();
                MyMail.From = new System.Net.Mail.MailAddress(smtpFrom);
                MyMail.To.Add(email);
                //MyMail.To.Add("jmv@opentraffic.net");
                MyMail.Subject = (string)GetLocalResourceObject("EmailSubject");
                MyMail.IsBodyHtml = true;
                /*TGA@ INTENTO DE INSERTAR IMAGEN */
                //MyMail.AlternateViews.Add(objHTLMAltView);
                MyMail.Body = bodyMessage;

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
                    //emailMsg.InnerText = ex.Message;
                    //emailMsg.Style.Add("display", "none");
                }
            }
            catch (System.Web.HttpException ehttp)
            {
                //emailMsg.InnerText = ehttp.Message;
                //emailMsg.Style.Add("display", "none");
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
