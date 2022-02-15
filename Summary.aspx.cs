using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
// BBDD
using Oracle.ManagedDataAccess.Client;
using System.Configuration;
// EMAIL
//using System.Web.Mail;
using System.Web.Util;
using System.Net.Mail;
using System.Text;
// Desencriptar
using OTSDecrypt;


namespace OTAformApp
{
    public partial class Summary : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string strError = String.Empty;

            // PROTEGEMOS SI INTENTAN ACCEDER DIRECTAMENTE
            if (Session.IsNewSession || Session["userID"] == null)
            {
                Session.RemoveAll();
                Session.Abandon();
                Page.Response.Redirect("~/index.aspx", true);
            }


            // Variables de control
            string userId = Session["userID"].ToString();
            string operation = Request["Action"].ToString();

            CUser user = new CUser(userId);

            LoadUserData(user);
                      
            // Set Client and Email client in the screen
            nameClient.InnerText = user.Name + " " + user.Surname1;
            mailClient.InnerText = user.Email;


            SetLiterals();

            if (operation == "update")
            {
                divPrint.Attributes.Add("style", "display:none");
                divPrintData.Attributes.Add("style", "display:none");
            }
            else
            {
                // Hasta que no active la cuenta no puede hacer nada.
                Session["userID"] = null;
                

            }

        

        }



        //Sustitución de literales de página para usuario en modo edición de datos
        private void SetLiterals()
        {
            locTitle.Text = (string)GetLocalResourceObject("locTitle");
            locSubTitle.Text = (string)GetLocalResourceObject("locSubTitleModify");
            locDescriptionHead.Text = (string)GetLocalResourceObject("locDescriptionHead");
            locDescription.Text = (string)GetLocalResourceObject("locDescriptionModify");
            emailMsg.Attributes.Add("style", "display:none");
        }

     
        protected void LoadUserData(CUser user)
        {
                     

            this.name.Text = user.Name;
            this.surname1.Text = user.Surname1;
            this.surname2.Text = user.Surname2;
            this.DNINIF.Text = user.DNINIF;
            this.telephone.Text = user.Telephone1;
            this.telephone2.Text = user.Telephone2;
            this.address.Text = user.Address;
            this.addressNum.Text = user.AddressNum;
            this.level.Text = user.Level;
            this.door.Text = user.Door;
            this.stair.Text = user.Stair;
            this.letter.Text = user.Letter;
            this.PostCode.Text = user.PostCode;
            this.city.Text = user.City;
            this.province.Text = user.Province;
            this.country.Text = user.Country;


            this.user.Text = user.UserName;
            //this.password.Text = getPasswordMask(dr["MU_PASSWORD"].ToString());
            string strPasswordMask = string.Empty;
            //this.password.Text = strPasswordMask.PadRight(user.Password.Length, '*');  <-------- ESTA MAL
            this.email.Text = user.Email;

            for (int i = 0; i < user.Plates.Length; i++)
            {
                plates.Text += user.Plates[i] + "<br />"; 

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
