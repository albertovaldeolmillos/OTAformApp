using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OTAformApp
{
    public partial class ResponseKO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Evitar que al darle "anterior" al navegador no cargue la pagina
            Response.Cache.SetNoStore();

            // PROTEGEMOS SI INTENTAN ACCEDER DIRECTAMENTE
            // Eliminado para la aplicación web
            //if (Session.IsNewSession || Session["userID"] == null)
            //{
            //    Session.RemoveAll();
            //    Session.Abandon();
            //    Page.Response.Redirect("~/index.aspx", true);
            //}

            // Trigger event for web app
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "responseEvent", "dispatchResponseEvent();", true);
        }
    }
}
