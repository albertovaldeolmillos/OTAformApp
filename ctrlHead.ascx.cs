namespace OTAformApp
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
    using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections.Specialized;

	/// <summary>
	///		Summary description for ctrlHead.
	/// </summary>
	public partial class ctrlHead : System.Web.UI.UserControl
	{
		//protected System.Web.UI.HtmlControls.HtmlContainerControl alert;	
        
        protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Page.Request["errorLog"] != null)
            {
                Session.Remove("errorLog");
            }
			
			// Establecer idioma
			string cookieName = "OTAformLang";            
			try
			{
                LoadCulture();
                
                HttpCookie cookie = Request.Cookies.Get(cookieName);
                if (cookie != null)
                    Page.Session["lang"] = cookie.Value;
                
                //Si el usuario ha modificado el idioma se genera una cookie con el nuevo valor
                if (Page.Request["ctrlHead:lang"] != null)
                {
                    Page.Session["lang"] = Page.Request["ctrlHead:lang"].ToString();
                    int iDaysToExpire = 30;
                    HttpCookie objCookie = new HttpCookie(cookieName);
                    objCookie.Value = Page.Session["lang"].ToString();
                    objCookie.Expires = DateTime.Now.AddDays(iDaysToExpire);
                    Response.Cookies.Clear();
                    Response.Cookies.Add(objCookie);
                } 
			}
			catch(Exception eGet)
			{
				string errGetCookie = eGet.ToString();
			}                        
            
            lang.Value = Page.Session["lang"].ToString();            
		}


        private void LoadCulture()
        {        
            btnSubmit.InnerText = (string)GetLocalResourceObject("btnSubmit");
            btnSubmitAccess.InnerText = (string)GetLocalResourceObject("btnSubmitAccess");
            lblConfEmail.Text = (string)GetLocalResourceObject("lblConfEmail");
            lblInpPswd.Text = (string)GetLocalResourceObject("lblInpPswd");
            lblInpUser.Text = (string)GetLocalResourceObject("lblInpUser");
            lblMatricula.Text = (string)GetLocalResourceObject("lblMatricula");
            lnkAltaOnline.InnerText = (string)GetLocalResourceObject("lnkAltaOnline");
            lnkDeleteUser.InnerText = (string)GetLocalResourceObject("lnkDeleteUser");
            lnkFacturation.InnerText = (string)GetLocalResourceObject("lnkFacturation");
            //lnkFormPrinter.InnerText = (string)GetLocalResourceObject("lnkFormPrinter");
            lnkInicio.InnerText = (string)GetLocalResourceObject("lnkInicio");
            lnkModifyData.InnerText = (string)GetLocalResourceObject("lnkModifyData");
            lnkRecuperarPassword.InnerText = (string)GetLocalResourceObject("lnkRecuperarPassword");
            lnkSalir.InnerText = (string)GetLocalResourceObject("lnkSalir");
            locRecuperarPassword.Text = (string)GetLocalResourceObject("locRecuperarPassword");
            //optLang.Text = (string)GetLocalResourceObject("optLang");            
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
