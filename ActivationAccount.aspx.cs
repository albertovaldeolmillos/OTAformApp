using System;
using System.Web;
using System.Web.UI;
// BBDD

namespace OTAformApp
{
    public partial class ActivationAccount : System.Web.UI.Page
    {               
     
        protected override void InitializeCulture()
        {
            //En el global.axax se añade el valor inicial de la variable Session a "es-ES". 
            string cookieName = "OTAformLang";
            HttpCookie cookie = Request.Cookies.Get(cookieName);
            if (cookie != null)
                Page.Session["lang"] = cookie.Value;
            
            UICulture = Page.Session["lang"].ToString();
            base.InitializeCulture();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            

            CUser user = null;
            string strError = String.Empty;

            try
			{
                user = new CUser();

                string userId  =  user.GetUserIDActivationPendent(Request.QueryString["TokenID"].ToString());

                if (userId != "-1")
                {
                    user.SetUserData(userId);
                    user.ActivateAccount();

                    locTitle.Text = (string)GetLocalResourceObject("locTitle");
                    locSubTitle.Text = (string)GetLocalResourceObject("locSubTitle");
                    MessageActivation.InnerHtml = (string)GetLocalResourceObject("MessageActivation");
                }
                else
                {
                    throw new Exception();
                }
                    
                                 
            }
			catch(Exception)
			{                
                strError = (string)GetLocalResourceObject("ExceptionDefault");

                Response.Redirect("errorForm.aspx?Error=" + strError);
			}
			
        }

    }
}
