using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace OTAformApp
{
    public partial class GetFunds : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //Evitar que al darle "anterior" al navegador no cargue la pagina
            Response.Cache.SetNoStore();

            // PROTEGEMOS SI INTENTAN ACCEDER DIRECTAMENTE
            if (Session.IsNewSession || Session["userID"] == null)
            {
                Session.RemoveAll();
                Session.Abandon();
                Page.Response.Redirect("~/index.aspx", true);
            }

            CUser user = new CUser();

            user.SetUserData(Session["userID"].ToString());

            if (user.Message == "1")
            {
                message_initial.Visible = false;
            }

            //Informamos del saldo actual

            UserData.InnerHtml = String.Format("Sr./Sra. {0} {1} su saldo actual es: <h2>{2} €</h2> ", user.Name, user.Surname1, String.Format("{0:0.##}", Double.Parse(user.Funds) / 100));

            if (Double.Parse(user.Funds) <= 0)
            {
                message_tbl.Visible = true;

                if (user.Recharge == "0")
                {
                    message_txt.InnerHtml = "<span style='font-size: medium; color: red;'>ATENCIÓN:</span> En su caso se han cargado los pagos pendientes y por esta razón su saldo es insuficiente. Recuerde que con saldo insuficiente no puede realizar aparcamientos con ninguna de las matrículas asociadas a este usuario o a otro en el caso de compartirlas.";
                }
                else
                {
                    message_txt.InnerHtml = "<span style='font-size: medium; color: red; line-height: 150%;'>ATENCIÓN:</span> Recuerde que con saldo insuficiente no puede realizar aparcamientos con ninguna de las matrículas asociadas a este usuario o a otro en el caso de compartirlas.";
                }
               

                message_txt.InnerHtml += "<br><br>Nota: Puede consultar las operaciones cargadas a su cuenta mediante el enlace facturación.";
            }
            else
            {
                message_tbl.Visible = false;
            }
        }

        protected void bttnHideMessage_OnClick(object sender, EventArgs e)
        {
            CUser user = new CUser();
            user.SetUserData(Session["userID"].ToString());
            user.CancelInitialMessage();

            message_initial.Visible = false;
        }

      
    }
}
