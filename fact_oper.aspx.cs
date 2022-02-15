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
	/// Summary description for fact_oper.
	/// </summary>
	public partial class fact_oper : System.Web.UI.Page
	{
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
            //Evitar que al darle anterior al navegador no cargue la pagina
            //Response.Expires = 0;
            Response.Cache.SetNoStore(); 

            // PROTEGEMOS SI INTENTAN ACCEDER DIRECTAMENTE
            if (Session.IsNewSession)
            {
                Session.RemoveAll();
                Session.Abandon();
                Page.Response.Redirect("~/index.aspx", true);
            }

            if (!Page.IsPostBack)
            {
                if (Session["userID"] != null)
                {
                    LoadUserData();
                    BindData();
                }
                else
                {
                    Page.Response.Redirect("~/index.aspx", true);
                }
            }

            SetPeriod();
		}      
        

		protected void NavigationLink_Click ( Object sender, CommandEventArgs e )
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
        

        protected void LoadUserData()
        {
            OracleDataReader dr = null;
            OTADataBase dataBase = new OTADataBase();
            try
            {
                string strFilter = "select * from v_mobile_users_fact where mu_id = " + Session["userID"];
                dataBase.OpenConnection();
                dataBase.Cmd.CommandText = strFilter;
                dr = dataBase.Cmd.ExecuteReader();
                
                if (dr.Read())
                {
                    //Asignar columnas a controles del webform
                    UserData.InnerHtml = String.Format("Sr./Sra. {0}<br>{1}<br>({2}) {3}<br>{4}<br>{5}<br>{6}", 
                                                       dr["Nombre"].ToString(), 
                                                       dr["Direccion"].ToString(),
                                                       dr["CPostal"].ToString(), 
                                                       dr["Ciudad"].ToString(), 
                                                       dr["Provincia"].ToString(),
                                                       dr["Pais"].ToString(), 
                                                       dr["NumTarjetaCredito"].ToString());
                }
                
                //dr.Close();
                //dr.Dispose();
                //dr = null;
            }
            catch (Exception )
            {
                Response.Redirect("errorForm.aspx?Error=Error de acceso a la factura LoadUserData");
                //Response.Redirect("errorForm.aspx?Error=Error en LoadUserData() " + ex.Message);
            }
            finally
            {
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                    dr = null;
                }

                dataBase.CloseConnection();
            }
        }


        protected void BindData()
		{
            OTADataBase dataBase = new OTADataBase();
            OracleDataReader dr = null;

            try
			{   
                dataBase.OpenConnection();                
                dataBase.Cmd.CommandText = GetCommandText();                
                dr = dataBase.Cmd.ExecuteReader();

                MyDataGrid.DataSource = ConvertReaderToTable(dr);                
				MyDataGrid.DataBind();
                
                //dr.Close();
                //dr.Dispose();
                //dr = null;

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
                
                SetTotals(((DataTable)MyDataGrid.DataSource));
			}
			catch(Exception )
            {
                Response.Redirect("errorForm.aspx?Error=Error de acceso a la factura BindData");
                //Response.Redirect("errorForm.aspx?Error=Error en BINDDATA() " + ex.Message);
			}
			finally
			{
                if (dr != null)
                {
                    dr.Close();
                    dr.Dispose();
                    dr = null;
                }

                dataBase.CloseConnection();			
			}			          			
		}



        private DataTable ConvertReaderToTable(OracleDataReader reader)
        {
            DataRowCollection rows = reader.GetSchemaTable().Rows;
            DataTable table = new DataTable();

            foreach (DataRow row in rows)
	        {
                //la columna "Tipo" no se muestra
                //if (row["ColumnName"].ToString() != "Tipo")
                //{
                    DataColumn col = new DataColumn();
                    col.ColumnName = (string)row["ColumnName"];
                    col.DataType = (Type)row["DataType"];
                    table.Columns.Add(col);
                //}
	        }

            while (reader.Read())
            {
                DataRow dataRow = table.NewRow();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    //Ignorar valores d ela columna "Tipo"
                    //if(reader.GetName(i) != "Tipo")                   
                        //dataRow[i] = reader.GetValue(i);
                        dataRow[reader.GetName(i)] = reader.GetValue(i);
                }

                table.Rows.Add(dataRow);
            }

            return table;
        }




        protected void SetTotals(DataTable drOperations)
        {
            decimal TotalEstacionamiento = 0;
            decimal TotalAmpliaciones = 0;
            decimal TotalDevoluciones = 0;
            decimal TotalPostPagos = 0;
            decimal TotalPagosSancion = 0;
            decimal TotalRecarga = 0;
            decimal TotalOperaciones = 0;
            DataView dvTotals = new DataView(drOperations);
            //DataRowView[] dvResult = dvTotals.FindRows("idTipoPago = 1");
            if (dvTotals.Count > 0)
            {
                foreach (DataRowView itemView in dvTotals)
                {
                    switch (Convert.ToInt32(itemView.Row["Tipo"]))
	                {
		                case 1: //Estacionamiento
                            TotalEstacionamiento += Convert.ToDecimal(itemView.Row["Importe"]);
                            break;
                        case 2: //Ampliacion
                            TotalAmpliaciones += Convert.ToDecimal(itemView.Row["Importe"]);
                            break;
                        case 3: //Devolucion
                            TotalDevoluciones += Convert.ToDecimal(itemView.Row["Importe"]);
                            break;
                        case 4: //Pago Sancion
                            TotalPagosSancion += Convert.ToDecimal(itemView.Row["Importe"]);
                            break;
                        case 5: //Recarga
                            TotalRecarga += Convert.ToDecimal(itemView.Row["Importe"]);
                            break;
                        case 7: //PostPago
                            TotalPostPagos += Convert.ToDecimal(itemView.Row["Importe"]);
                            break;
                        default:
                            break;
	                }                    
                }

                TotalOperaciones = TotalEstacionamiento + TotalAmpliaciones + TotalPagosSancion + TotalRecarga + TotalPostPagos;
                TotalOperaciones += TotalDevoluciones;
            }
               
            if (TotalEstacionamiento != 0)
                GenerateColumn("Total Estacionamientos", TotalEstacionamiento,false);
            if (TotalAmpliaciones != 0)
                GenerateColumn("Total Ampliaciones", TotalAmpliaciones, false);
            if (TotalDevoluciones != 0)
                GenerateColumn("Total Devoluciones", -TotalDevoluciones, false);
            if (TotalPostPagos != 0)
                GenerateColumn("Total PostPagos", TotalPostPagos, false);
            if (TotalPagosSancion != 0)
                GenerateColumn("Total Pagos de Sanción", TotalPagosSancion, false);
            if (TotalRecarga != 0)
                GenerateColumn("Total Recarga", TotalRecarga, false);
            if (TotalOperaciones != 0)
                GenerateColumn("Total Operaciones", TotalOperaciones, true);
        }



        protected void GenerateColumn(string name, decimal valueColumn, Boolean totalOperations)
        {            
            HtmlTableCell td;            

            //Generar Columna titulo
            td = new HtmlTableCell();
            td.InnerHtml = "<strong>" + name + "<strong>";
            if (totalOperations)
            {                
                tbTotals.Rows[2].Cells.Add(td);
                td.ColSpan = tbTotals.Rows[0].Cells.Count;
            }
            else
                tbTotals.Rows[0].Cells.Add(td);
             
            //Generar Columna importe
            td = new HtmlTableCell();
            //td.InnerHtml = "<strong>" + valueColumn + "<strong>";
            td.InnerHtml = "<strong>" + String.Format("{0} €", valueColumn) + "<strong>";
            if (totalOperations)
            {                
                tbTotals.Rows[3].Cells.Add(td);
                td.ColSpan = tbTotals.Rows[0].Cells.Count;
            }
            else
                tbTotals.Rows[1].Cells.Add(td);                   
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


        protected void Selection_PeriodChange(Object sender, EventArgs e)
        {
            //Puesto que se aplica un filtro, el número de páginas resultante del datagrid probablemente será diferente
            //al cargado antes del postback. Y como la propiedad "CurrentPageIndex" se mantiene entre
            //postbacks, puede darse el caso que el "CurrentPageIndex" guarde un valor mayor que la propiedad
            //"PageSize" después de aplicarse el filtro.
            MyDataGrid.CurrentPageIndex = 0;
            BindData();
        }


        private string GetCommandText()
        {            
            DateTime IniDate;
            DateTime EndDate;            
            string strFilter = "SELECT ID, ope_dope_id as \"Tipo\", " +
                                "Tipo_Operacion AS \"TipoOperacion\", " +
                                "Zona AS \"Zona\", " +
                                "Tipo_Pago AS \"TipoPago\", " +
                                "Fecha_Pago_Sancion AS \"FechaPagoSancion\", " +
                                "Inicio_Estacionamiento AS \"InicioEstacionamiento\", " +
                                "Fin_Estacionamiento AS \"FinEstacionamiento\", " +
                                "Importe AS \"Importe\", " + 
                                "Matricula AS \"Matricula\", " + 
                                "Articulo AS \"Articulo\", " +
                                "ope_post_pay AS \"PostPago\", " +                                
                                "Num_Expediente AS \"NºExpediente\", " +
                                "Sancion AS \"Sancion\" ";
                                
            string strFrom = " FROM v_operations_mobile ";
            string strWhere = " WHERE idTipoPago = 4 AND ope_mobi_user_id = " + Session["userID"];

            switch (ddlPeriod.SelectedValue)
            {
                case "Todo":
                    //strFilter = "SELECT * FROM operations WHERE ope_mobi_user_id = " + Session["userID"];
                    break;
                case "Actual":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    EndDate = IniDate.AddMonths(1).AddDays(-1);
                    strWhere += " AND FIN_ESTACIONAMIENTO >= to_date('" + IniDate.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')" +
                                 " AND FIN_ESTACIONAMIENTO <= to_date('" + EndDate.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')";
                    strFrom = " FROM v_operations_mobile_curr ";
                    break;
                case "Anterior":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
                    EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
                    strWhere += " AND FIN_ESTACIONAMIENTO >= to_date('" + IniDate.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')" +
                                 " AND FIN_ESTACIONAMIENTO <= to_date('" + EndDate.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')";
                    strFrom = " FROM v_operations_mobile_curr ";
                    break;
                case "Ultimos3":
                    IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-3);
                    EndDate = IniDate.AddMonths(3).AddDays(-1);
                    strWhere += " AND FIN_ESTACIONAMIENTO >= to_date('" + IniDate.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')" +
                                 " AND FIN_ESTACIONAMIENTO <= to_date('" + EndDate.ToString("dd/MM/yyyy") + "','dd/mm/yyyy')";
                    break;                   
            }

            strFilter += strFrom;
            strFilter += strWhere;
            strFilter += " order by Inicio_Estacionamiento";
            return strFilter;
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



        private void SetPeriod()
        {
            switch (ddlPeriod.SelectedValue)
            {               
                case "Todo":
                    _period = "Todos" ;
                    break;
                case "Actual":
                    _period = Enum.GetName(typeof(NameMonth), DateTime.Today.Month);
                    break;
                case "Anterior":
                    _period = Enum.GetName(typeof(NameMonth),DateTime.Today.AddMonths(-1).Month);
                    break;
                case "Ultimos3":
                    _period = Enum.GetName(typeof(NameMonth), DateTime.Today.AddMonths(-3).Month) + " - " + 
                              Enum.GetName(typeof(NameMonth), DateTime.Today.Month);
                    break;
            }            
        }



        //Datos de prueba
        //ICollection CreateDataSource(string filter)
        //{
        //    // Create sample data for the DataGrid control.
        //    DataTable dt = new DataTable();
        //    DataRow dr;
        //    DateTime fecha = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day).AddMonths(-4);
 
        //    // Define the columns of the table.
        //    dt.Columns.Add(new DataColumn("DateValue", typeof(DateTime)));
        //    dt.Columns.Add(new DataColumn("idTipoPago", typeof(Int32)));
        //    dt.Columns.Add(new DataColumn("StringValue", typeof(String)));
        //    dt.Columns.Add(new DataColumn("Importe", typeof(Double)));            
 
        //    // Populate the table with sample values.
        //    int x=1;
        //    for (int i=0; i<=150; i++) 
        //    {
        //        dr = dt.NewRow();
        //        dr[0] = fecha.AddDays(i);
        //        if(x > 7) x = 1;                
        //        dr[1] = x;
        //        dr[2] = "Item " + i.ToString();
        //        dr[3] = 1.23 * (i + 1);                
        //        dt.Rows.Add(dr);
        //        x += 1;
        //    }

        //    DataView dv = new DataView(dt, filter, "DateValue ASC", DataViewRowState.CurrentRows);            

        //    return dv;      
        //}


        //pruebas
        //private string GetFilter()
        //{
        //    DateTime IniDate;
        //    DateTime EndDate;
        //    string strFilter = String.Empty;
        //    switch (ddlPeriod.SelectedValue)
        //    {
        //        case "Todo":
        //            strFilter = "";
        //            break;
        //        case "Actual":
        //            IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        //            EndDate = IniDate.AddMonths(1).AddDays(-1);
        //            strFilter += "DateValue >= '" + IniDate.ToShortDateString() +
        //                         "' AND DateValue <= '" + EndDate.ToShortDateString() + "'";
        //            break;
        //        case "Anterior":
        //            IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-1);
        //            EndDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
        //            strFilter += "DateValue >= '" + IniDate.ToShortDateString() +
        //                         "' AND DateValue <= '" + EndDate.ToShortDateString() + "'";
        //            break;
        //        case "Ultimos3":
        //            IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(-3);
        //            EndDate = IniDate.AddMonths(3).AddDays(-1);
        //            strFilter += "DateValue >= '" + IniDate.ToShortDateString() +
        //                         "' AND DateValue <= '" + EndDate.ToShortDateString() + "'";
        //            break;
        //    }

        //    return strFilter;
        //}

        
        
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



        public static void Exportar_a_Excel(System.Web.HttpResponse Response, System.Web.UI.WebControls.DataGrid elGrid)
        {
            //string strSampleFile = "Listado.xls";
            //OWC.Spreadsheet objExcel = new OWC.Spreadsheet(); //Referencing Microsoft Office Web Compenents 9.0

            ////Add the column headers to the excel sheet
            //int ColumnCount = 1;
            //for (int i = 0; i < elGrid.Columns.Count; i++)
            //{
            //    if (elGrid.Columns[i].Visible)
            //    {
            //        objExcel.ActiveSheet.Cells[1, ColumnCount] = elGrid.Columns[i].HeaderText;
            //        ((OWC.Range)objExcel.ActiveSheet.Cells[1, ColumnCount]).Font.set_Bold(true);
            //        ColumnCount++;
            //    }
            //}
            //objExcel.ActiveSheet.Rows.EntireRow.Font.set_Bold(true);

            ////Add the values
            //for (int i = 0; i < elGrid.Items.Count; i++)
            //{
            //    int col = 1;
            //    for (int j = 0; j < elGrid.Columns.Count; j++)
            //    {
            //        if (elGrid.Columns[j].Visible)
            //        {
            //            string val = elGrid.Items[i].Cells[j].Text;
            //            if (val == null || val == "")
            //            {
            //                if (elGrid.Items[i].Cells[j].Controls[0] is System.Web.UI.WebControls.HyperLink)
            //                {
            //                    val = ((System.Web.UI.WebControls.HyperLink)(elGrid.Items[i].Cells[j].Controls[0])).Text;
            //                }
            //                else if (elGrid.Items[i].Cells[j].Controls[1] is System.Web.UI.WebControls.TextBox)
            //                {
            //                    val = ((System.Web.UI.WebControls.TextBox)(elGrid.Items[i].Cells[j].Controls[1])).Text;
            //                }
            //            }
            //            val = val.Replace('\r', ' ');
            //            val = val.Replace('\n', ' ');
            //            val = val.Replace('-', '_');
            //            if (val == " ")
            //                val = "";

            //            if (elGrid.Columns[j].HeaderText.ToUpper().IndexOf("FECHA") >= 0)
            //            {
            //                ((OWC.Range)objExcel.ActiveSheet.Cells[i + 2, col]).set_NumberFormat("dd/mm/yyyy");
            //                objExcel.ActiveSheet.Cells[i + 2, col] = Convert.ToDateTime(val);
            //            }
            //            else
            //            {
            //                objExcel.ActiveSheet.Cells[i + 2, col] = val;
            //            }
            //            col++;
            //        }
            //    }
            //}

            ////Autofit the columns to make them look pretty
            //objExcel.ActiveSheet.Columns.AutoFitColumns();
            //objExcel.ActiveSheet.Rows.AutoFitColumns();

            ////Do any formatting you wish.....
            ////objExcel.ActiveSheet.Rows.EntireRow.Font.set_Bold(true);

            ////This saves the excel file "strSampleFolder" on the server
            //string strSampleFolder = "c:\\" + strSampleFile;
            //objExcel.ActiveSheet.Export(strSampleFolder, OWC.SheetExportActionEnum.ssExportActionNone);

            ////This opens the download dialoge box and allows the user
            ////to download the excel sheet from the server
            //Response.Clear();
            ////Response.ContentEncoding = System.Text.Encoding.Unicode;
            //Response.ContentType = "application.x-msexcel"; //"application/octet-stream";
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + strSampleFile);
            //Response.Flush();
            ////This command actually transfers the file
            //Response.WriteFile(strSampleFolder);
            //Response.End();
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
