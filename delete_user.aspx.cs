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
// BBDD
using Oracle.ManagedDataAccess.Client;
using System.Configuration;


namespace OTAformApp
{
	/// <summary>
	/// Summary description for delete_user.
	/// </summary>
	public partial class delete_user : System.Web.UI.Page
	{

        //Inicializar el valor de UICulture de la página
        protected override void InitializeCulture()
        {             
            UICulture = Page.Session["lang"].ToString();
            base.InitializeCulture();
        }
        

        protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Session["userID"] != null)
            {
                LoadCulture();
                nameClient.InnerText = Session["userName"].ToString();
                RemoveSuscription();
            }
            else
            {
                Session.RemoveAll();
                Session.Abandon();
                string strError = (string)GetLocalResourceObject("ExceptionDefault");
                Response.Redirect("errorForm.aspx?Error=" + strError, true);
            }			
		}


        private void RemoveSuscription()
        {
            OTADataBase dataBase = new OTADataBase();                       

            try
            {   
                dataBase = new OTADataBase();
                dataBase.OpenConnection(); 	                
                string sSQL = String.Format("UPDATE mobile_users t SET MU_VALID = 0, MU_DELETED = 1 " +
                                            "WHERE MU_ID = {0}", Session["userID"].ToString());
                //sSQL = String.Format(" delete from mobile_users t where MU_ID = '{0}' ", Session["userID"].ToString() );
                dataBase.Cmd.CommandText = sSQL;
                int delUser = dataBase.Cmd.ExecuteNonQuery();  
                
                if (delUser > 0)
                {
                    Session.RemoveAll();
                    Session.Abandon();
                }
                else
                {
                    throw new Exception();
                }                
            }
            catch (Exception )
            {
                string strError = (string)GetLocalResourceObject("ExceptionDefault");
                Response.Redirect("errorForm.aspx?Error=" + strError);
            }
            finally
            {
                dataBase.CloseConnection();
            }		
        }


        private void LoadCulture()
        {
            lnkReturn.InnerText = (string)GetLocalResourceObject("lnkReturn");
            locTitle.Text = (string)GetLocalResourceObject("locTitle");
            locSubTitle.Text = (string)GetLocalResourceObject("locSubTitle");
            locIniUser.Text = (string)GetLocalResourceObject("locIniUser");
            locUser.Text = (string)GetLocalResourceObject("locUser");
            locBay.Text = (string)GetLocalResourceObject("locBay");
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
