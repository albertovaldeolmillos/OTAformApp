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



namespace OTAformApp
{
	/// <summary>
	/// Summary description for WebForm2.
	/// </summary>
	public partial class errorForm : System.Web.UI.Page
	{

        protected override void InitializeCulture()
        {            
            UICulture = Page.Session["lang"].ToString();
            base.InitializeCulture();
        }

        protected void Page_Load(object sender, System.EventArgs e)
		{
            locTitle.Text = (string)GetLocalResourceObject("locTitle");
            //El msgError ya viene traducido desde la página donde se origina el error.            
            msgError.InnerText = Request.QueryString["Error"].ToString();
            //Si existe parametro "ErrorType" el usuario no ha activado todavia su cuenta            
            if (Request.QueryString["ErrorType"]!=null)
                bodyError.InnerHtml = (string)GetLocalResourceObject("bodyErrorActivate"); 
            else
                bodyError.InnerHtml = (string)GetLocalResourceObject("bodyError"); 
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


