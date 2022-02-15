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

namespace OTAformApp
{
	/// <summary>
	/// Summary description for index.
	/// </summary>
	public partial class index : System.Web.UI.Page
	{

        protected override void InitializeCulture()
        {
            //En el global.axax se añade el valor inicial de la variable Session a "es-ES".            
            if (Page.Request["ctrlHead:lang"] != null)
                UICulture = Page.Request["ctrlHead:lang"].ToString();
            else
            {
                //Comprobar si el usuario alberga una cookie con el idioma escogido
                //en una visita anterior
                string cookieName = "OTAformLang";
                HttpCookie cookie = Request.Cookies.Get(cookieName);
                if (cookie != null)
                    Page.Session["lang"] = cookie.Value;

                UICulture = Page.Session["lang"].ToString();
            }

            base.InitializeCulture();
        }
        
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
			if(Page.IsPostBack)
			{
				string[] formVals = Page.Request.Form.GetValues(5);
				Page.Response.Write(formVals.ToString());	
			}

            LoadCulture();
          

            if (Request.QueryString["Salir"] != null)
            {
                if (Request.QueryString["Salir"].ToString() == "Yes")
                {
                    Session.RemoveAll();
                    Session.Abandon();
                    Page.Response.Redirect("index.aspx", true);
                }
            }
		}



        private void LoadCulture()
        {
            InfoService.InnerHtml = (string)GetLocalResourceObject("InfoService");
            Ampliation.InnerHtml = (string)GetLocalResourceObject("Ampliation");
            Devolution.InnerHtml = (string)GetLocalResourceObject("Devolution");
            Santions.InnerHtml = (string)GetLocalResourceObject("Santions");
            Conditions.InnerHtml = (string)GetLocalResourceObject("Conditions");
            Ventajas.InnerHtml = (string)GetLocalResourceObject("Ventajas");
            Cost.InnerHtml = (string)GetLocalResourceObject("Cost");

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
