using System;
using System.Collections.Generic;
//using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

using Newtonsoft.Json;


namespace OTAformApp
{
    public partial class SetFunds : System.Web.UI.Page
    {
        //string URL_SERMEPA = "https://sis-t.sermepa.es:25443/sis/realizarPago";
        string Merchant_MerchantName = "Mugipark";
        string Merchant_MerchantCode = "123456789";
        string Merchant_Terminal = "1";
        string Merchant_Password = "3926LO490N18T012";
        string Merchant_Currency       	= "978";
        string Merchant_Order          	= "-1";
	    string Merchant_TransactionType   = "0";
        //string Merchant_MerchantURL = "http://ops.ods.org:58100/OPSPayMobileWeb/CheckResponse.aspx";


        string claveComercio = "JMd7/eiuzsEEF+lKxxDq41rADVNt4X5v";
        //string claveComercio = "sq7HjrUOBfKmC576ILgskD5srU870gJ7"; // Test


        protected string _period = String.Empty;
        protected enum NameMonth
        {
            Enero = 1,
            Febrero = 2,
            Marzo = 3,
            Abril = 4,
            Mayo = 5,
            Junio = 6,
            Julio = 7,
            Agosto = 8,
            Septiembre = 9,
            Octubre = 10,
            Noviembre = 11,
            Diciembre = 12
        }

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

            //Informamos del saldo actual

            UserData.InnerHtml = String.Format("Sr./Sra. {0} {1} su saldo actual es de {2} euros.", user.Name, user.Surname1, String.Format("{0:0.##}", Double.Parse(user.Funds)/100));


            if (Page.IsPostBack)
            {
                if (DS_OPER.Value == "1")
                {

                    //DS_Merchant_MerchantName.Value = Merchant_MerchantName;
                    //DS_Merchant_MerchantCode.Value = Merchant_MerchantCode;
                    //DS_Merchant_Terminal.Value = Merchant_Terminal;
                    //DS_Merchant_Currency.Value = Merchant_Currency;
                    //DS_Merchant_UrlKO.Value = System.Configuration.ConfigurationSettings.AppSettings["Merchant_UrlKO"];
                    //DS_Merchant_UrlOK.Value = System.Configuration.ConfigurationSettings.AppSettings["Merchant_UrlOK"];

                    //if (txtAmount.Text.Length != 0)
                    //{
                    //    int s_amount = int.Parse(txtAmount.Text) * 100;
                    //    DS_Merchant_Amount.Value = s_amount.ToString();
                    //}
                    //else
                    //{
                    //    DS_Merchant_Amount.Value = RadioButtonList1.SelectedValue;
                    //}
                    //Merchant_Order = user.SetUserOrder(DS_Merchant_Amount.Value);

                    //DS_Merchant_Order.Value = Merchant_Order;
                    //DS_Merchant_TransactionType.Value = Merchant_TransactionType;
                    //DS_Merchant_MerchantURL.Value = System.Configuration.ConfigurationSettings.AppSettings["Merchant_MerchantURL"];

                    ////Cálculo de la Firma SHA
                    //string cadenaFirma = DS_Merchant_Amount.Value + DS_Merchant_Order.Value + DS_Merchant_MerchantCode.Value + DS_Merchant_Currency.Value + DS_Merchant_TransactionType.Value + DS_Merchant_MerchantURL.Value + Merchant_Password;

                    //SHA1.SHA1 sha1 = new SHA1.SHA1();
                    //DS_Merchant_MerchantSignature.Value = sha1.SecureHash(cadenaFirma).ToString();




                    string DS_Merchant_Amount;
                    if (txtAmount.Text.Length != 0)
                    {
                        int s_amount = int.Parse(txtAmount.Text) * 100;
                        DS_Merchant_Amount = s_amount.ToString();
                    }
                    else
                    {
                        DS_Merchant_Amount = RadioButtonList1.SelectedValue;
                    }
                    Merchant_Order = user.SetUserOrder(DS_Merchant_Amount);



                    Peticion peticion = new Peticion
                    {
                        DS_MERCHANT_MERCHANTCODE = Merchant_MerchantCode,
                        DS_MERCHANT_TERMINAL = Merchant_Terminal,
                        DS_MERCHANT_CURRENCY = Merchant_Currency,
                        DS_MERCHANT_URLKO = System.Configuration.ConfigurationSettings.AppSettings["Merchant_UrlKO"],
                        DS_MERCHANT_URLOK = System.Configuration.ConfigurationSettings.AppSettings["Merchant_UrlOK"],
                        DS_MERCHANT_AMOUNT = DS_Merchant_Amount,
                        DS_MERCHANT_ORDER = Merchant_Order,
                        DS_MERCHANT_TRANSACTIONTYPE = Merchant_TransactionType,
                        DS_MERCHANT_MERCHANTURL = System.Configuration.ConfigurationSettings.AppSettings["Merchant_MerchantURL"]

                    };

                    string data = JsonConvert.SerializeObject(peticion, Formatting.None);

                    data = EncodeTo64(data);



                    DS_MERCHANTPARAMETERS.Value = data;



                    string clave = claveComercio;

                    byte[] key_des = TripleDESEncrypt(Merchant_Order, Convert.FromBase64String(clave));

                    var hashmessage1 = EncriptarSHA256(data, key_des);

                    DS_SIGNATURE.Value = Convert.ToBase64String(hashmessage1);






                    //Adecuación pantalla a la información a mostrarse
                    form1.Action = System.Configuration.ConfigurationSettings.AppSettings["URL_SERMEPA"];

                    selection.Visible = false;
                    confirm.Visible = true;
                    Submit1.Value = "Recargar";
                    double amount = double.Parse(DS_Merchant_Amount) / 100;
                    confirm_message.InnerHtml = "Ha seleccionado un importe de <b>" + amount.ToString() + "</b> Euros para realizar la recarga. Si está conforme pulse el botón Recargar. Se le transferirá al servidor de pago externo para que realice la recarga.";
                }
            }
            else
            {
              


                selection.Visible = true;
                confirm.Visible = false;
            }

            

            BindData();
            SetPeriod();
        
        }


        //////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////
        ////////////        FUNCIONES PARA LA GENERACIÓN DEL FORMULARIO DE PAGO:          ////////////
        //////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////



        public static byte[] EncriptarSHA256(string texto, byte[] privateKey)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] key = privateKey;
            var myhmacsha256 = new HMACSHA256(key);
            byte[] hashValue = myhmacsha256.ComputeHash(encoding.GetBytes(texto));
            myhmacsha256.Clear();
            return hashValue;
        }


        public static string Encrypt(string textKey, string content)
        {
            byte[] key = Encoding.GetEncoding(1252).GetBytes(textKey);
            byte[] iv = new byte[8];
            byte[] data = Encoding.GetEncoding(1252).GetBytes(content);
            byte[] enc = new byte[0];
            System.Security.Cryptography.TripleDES tdes = System.Security.Cryptography.TripleDES.Create();
            tdes.IV = iv;
            tdes.Key = key;
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.Zeros;
            ICryptoTransform ict = tdes.CreateEncryptor();
            enc = ict.TransformFinalBlock(data, 0, data.Length);
            return Encoding.GetEncoding(1252).GetString(enc);
        }


        public static string HashHMAC(string data, string key)
        {
            key = key ?? "";
            var encoding = Encoding.GetEncoding(1252);
            byte[] keyByte = encoding.GetBytes(key);
            byte[] messageBytes = encoding.GetBytes(data);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        public static byte[] TripleDESEncrypt(string texto, byte[] key)
        {
            using (TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider())
            {
                byte[] iv_0 = { 0, 0, 0, 0, 0, 0, 0, 0 };

                byte[] toEncryptArray = Encoding.ASCII.GetBytes(texto);

                tdes.IV = iv_0;

                //assign the secret key
                tdes.Key = key;

                tdes.Mode = CipherMode.CBC;

                tdes.Padding = PaddingMode.Zeros;

                ICryptoTransform cTransform = tdes.CreateEncryptor();
                //transform the specified region of bytes array to resultArray
                byte[] resultArray =
                  cTransform.TransformFinalBlock(toEncryptArray, 0,
                  toEncryptArray.Length);

                //Clear to Best Practices
                tdes.Clear();

                return resultArray;
            }
        }


        public static string EncodeTo64(string data)
        {
            byte[] toEncodeAsBytes = Encoding.GetEncoding(1252).GetBytes(data);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        public static string DecodeFrom64(string data)
        {
            byte[] binary = Convert.FromBase64String(data);
            return Encoding.GetEncoding(1252).GetString(binary);
        }



        //Gestión del grid de operaciones de recarga

        private void SetPeriod()
        {
            switch (ddlPeriod.SelectedValue)
            {
                case "Todo":
                    _period = "Todos";
                    break;
                case "Actual":
                    _period = Enum.GetName(typeof(NameMonth), DateTime.Today.Month);
                    break;
                case "Anterior":
                    _period = Enum.GetName(typeof(NameMonth), DateTime.Today.AddMonths(-1).Month);
                    break;
                case "Ultimos3":
                    _period = Enum.GetName(typeof(NameMonth), DateTime.Today.AddMonths(-3).Month) + " - " +
                              Enum.GetName(typeof(NameMonth), DateTime.Today.Month);
                    break;
            }
        }

        protected void Selection_PeriodChange(Object sender, EventArgs e)
        {
            //Puesto que se aplica un filtro, el número de páginas resultante del datagrid probablemente será diferente
            //al cargado antes del postback. Y como la propiedad "CurrentPageIndex" se mantiene entre
            //postbacks, puede darse el caso que el "CurrentPageIndex" guarde un valor mayor que la propiedad
            //"PageSize" después de aplicarse el filtro.
            MyDataGrid.CurrentPageIndex = 0;
            
            BindData();
        }

        protected void Selection_Change(Object sender, EventArgs e)
        {
            // Establecer la página sin perder el conjunto de registros que se visualizaba
            // antes del postback
            MyDataGrid.CurrentPageIndex = GetCurrentPageIndex();
            // Establecer la propiedad "pagesize" seleccionada por el usuario
            MyDataGrid.PageSize = Convert.ToInt32(PageSizeList.SelectedItem.Text);
            //Volver a enlazar el datagrid con el origen de datos
            BindData();
        }

        protected int GetCurrentPageIndex()
        {
            // Calcular el indice del primer registro visible en la página actual del datagrid.
            int firstRecordIndex = MyDataGrid.CurrentPageIndex * MyDataGrid.PageSize + 1;
            // Calcular número de paginas que tendrá el datagrid después del postback
            int oldNumberRecords = Convert.ToInt32(Session["dataGridRecords"].ToString());
            int newPageSize = Convert.ToInt32(PageSizeList.SelectedItem.Text);
            // Math.Ceiling() -> Devuelve el número entero más pequeño mayor o igual que el número decimal especificado
            int newPageCount = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(oldNumberRecords / newPageSize)));
            // Buscar la página del datagrid donde se muestre el registro que visualizábamos
            // antes del postback
            int newCurrentPageIndex = 0;
            for (int i = 1; i <= newPageCount; i++)
            {
                if (firstRecordIndex >= (i - 1) * newPageSize + 1 && firstRecordIndex <= i * newPageSize)
                    newCurrentPageIndex = i - 1;
            }

            return newCurrentPageIndex;
        }

        protected void ExportToExcel_Click(object sender, EventArgs e)
        {
            System.IO.StringWriter tw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

            MyDataGrid.AllowPaging = false;
            MyDataGrid.AllowSorting = false;
            //MyDataGrid.DataSource = CreateDataSource(GetFilter());            
            //MyDataGrid.DataBind();
            BindData();
            //preparativos para la hoja excel
            Response.Clear();


            //Especificamos el tipo de salida
            Response.ContentType = "application/vnd.ms-excel";
            //Damos la salida como attachment con el nombre de listado.xls
            Response.AddHeader("Content-Disposition", "attachment; filename=Listado.xls");



            //---Create the File pdf---------
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();

            //Response.ContentType = "application/pdf";
            //Response.AddHeader("content-disposition", "attachment;filename=Listado.pdf");

            //EnableViewState = false;

            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //HtmlTextWriter hw = new HtmlTextWriter(sw);

            ////---Renders the DataGrid and then dumps it into the HtmlTextWriter Control
            //MyDataGrid.RenderControl(hw);

            ////---Utilize the Response Object to write the StringWriter to the page
            //Response.Write(sw.ToString());
            //Response.Flush();
            //Response.Close();
            //Response.End();



            Response.BufferOutput = true;
            //Asociamos la salida con la codificación UTF7 (para poder visualizar los acentos correctamente)            
            Response.Charset = "UTF-8";
            Response.ContentEncoding = System.Text.Encoding.Default;
            this.EnableViewState = false;

            MyDataGrid.RenderControl(hw);
            Response.Write(tw.ToString());

            //Enviar al usuario
            Response.End();

            ///** para intentar darle formato a la columna con los datos**/
            ////for (int fila = 0; fila < MyDataGrid.Items.Count; fila++)
            ////{
            ////    //Cell[4] -->corresponde a la columna que contiene los datos que deben ser numericos.
            ////    MyDataGrid.Items[fila].Cells[4].Style.Add("mso-number-format","Fixed");
            ////    MyDataGrid.Items[fila].Cells[4].Style.Add("mso-displayed-decimal-separator",@"\,");
            ////    MyDataGrid.Items[fila].Cells[4].Style.Add("mso-displayed-thousand-separator",@"\.");
            ////}            
        }

        protected void NavigationLink_Click(Object sender, CommandEventArgs e)
        {
            int _currentPageNumber = MyDataGrid.CurrentPageIndex;

            switch (e.CommandName)
            {
                case "First":
                    _currentPageNumber = 0;
                    break;
                case "Last":
                    _currentPageNumber = MyDataGrid.PageCount - 1;
                    break;
                case "Next":
                    if (MyDataGrid.CurrentPageIndex < (MyDataGrid.PageCount - 1))
                        _currentPageNumber = _currentPageNumber + 1;
                    break;
                case "Prev":
                    if (MyDataGrid.CurrentPageIndex > 0)
                        _currentPageNumber = _currentPageNumber - 1;
                    break;
            }

            MyDataGrid.CurrentPageIndex = _currentPageNumber;
            BindData();
        }


        protected void BindData()
        {

            CUser user = new CUser();

            user.SetUserData(Session["userID"].ToString());



            try
            {

                MyDataGrid.DataSource = user.GetOrderData(ddlPeriod.SelectedValue);
                MyDataGrid.DataBind();


                //Actualizar contadores
                TotalPages.Text = string.Format("{0}", MyDataGrid.PageCount);
                CurrentPage.Text = string.Format("{0}", MyDataGrid.CurrentPageIndex + 1);
                //Actualizar estado de los controles de navegacion del DataGrid
                SetStateNavigationLink();
                //Guardar en variable session para aplicar al modificar dinamicamente el pagesize
                if (Session != null)
                    Session["dataGridRecords"] = ((DataTable)MyDataGrid.DataSource).Rows.Count;
                else
                    throw new Exception("Ha caducado la sesión.");

                
            }
            catch (Exception ex)
            {
                Response.Redirect("errorForm.aspx?Error=Error de acceso a la factura SetFunds " + ex.Message);
                //Response.Redirect("errorForm.aspx?Error=Error en BINDDATA() " + ex.Message);
            }
            finally
            {
                
            }
        }


       

        private void SetStateNavigationLink()
        {
            FirstPage.Enabled = true;
            PreviousPage2.Enabled = true;
            NextPage.Enabled = true;
            LastPage.Enabled = true;

            //First - Previous
            if (MyDataGrid.CurrentPageIndex == 0)
            {
                FirstPage.Enabled = false;
                PreviousPage2.Enabled = false;
            }
            //Last - Next
            if (MyDataGrid.CurrentPageIndex == (MyDataGrid.PageCount - 1))
            {
                NextPage.Enabled = false;
                LastPage.Enabled = false;
            }
        }

       


    }
}
