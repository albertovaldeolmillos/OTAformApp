<%@ Page language="c#" Codebehind="fact_oper.aspx.cs" AutoEventWireup="True" Inherits="OTAformApp.fact_oper" ResponseEncoding="utf-8" %>
<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Facturación - <%= ConfigurationSettings.AppSettings["titPages"] %></title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="C#"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
	    <link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen" />
	    <style type="text/css" media="screen">	       
	        .userData {font: bold small Arial; color:#663300;}
	        .rowTotals {font: x-small Verdana, Arial, sans-serif; text-align:right;}
	        .rowTotalsHead {font: x-small Verdana, Arial, sans-serif; text-align:right; background-color:#ADC1EE;}   
			.tableItem {font: x-small Verdana, Arial, sans-serif;}
			.tableHeader {font: bold x-small Arial; color:#663300; background-color:#ADC1EE;}
			.alternatingItem {font: x-small Verdana, Arial, sans-serif; background-color:#E7EAEF;}	
			.pageLinks {text-align: center;}  
	    </style>	    
	</head>
  <body MS_POSITIONING="GridLayout">
	
		<div id="frame">
			<div id="layoutform">
				<div id="top_menu">
					<OTAform:header runat="server" id="ctrlHead"/>
				</div>
			    <hr />
				<h1 id="title">Servicio de pago remoto del estacionamiento</h1>
				<h2 id="tsubtitle">Facturación</h2>			
				<div id="UserData" class="userData" runat="server"></div>	
				<p>Extracto de operaciones realizadas a través de pago remoto por m&oacute;vil del estacionamiento de Mugipark en el mes de <strong><%= _period %></strong></p>
				<br />
				<form id="form1" name="form1" enctype="" method="post" runat="server">				    
				    <table id="TableFactura" cellpadding="10px">
				        <tr style="width:100%">
				            <td style="text-align:left">
				                Periodo de Facturación: 
				                <asp:DropDownList id="ddlPeriod" AutoPostBack="True" OnSelectedIndexChanged="Selection_PeriodChange" runat="server">
						                <asp:ListItem Value="Todo"> Todo </asp:ListItem>
						                <asp:ListItem Value="Actual" Selected="True"> Mes Actual </asp:ListItem>
						                <asp:ListItem Value="Anterior"> Mes Anterior </asp:ListItem>
						                <asp:ListItem Value="Ultimos3"> Ultimos 3 meses </asp:ListItem>
			                    </asp:DropDownList>	
			                </td>
			                <td style="text-align:right">	
				                Filas por página: 
					            <asp:DropDownList id="PageSizeList" AutoPostBack="True" OnSelectedIndexChanged="Selection_Change" runat="server">
						            <asp:ListItem> 5 </asp:ListItem>
						            <asp:ListItem Selected="True"> 10 </asp:ListItem>
						            <asp:ListItem> 15 </asp:ListItem>
						            <asp:ListItem> 20 </asp:ListItem>
					            </asp:DropDownList>
                            </td>
                        </tr>
                    </table>             
                                              
					<div style="overflow:auto;width:100%; padding-bottom:16px;">
						<asp:DataGrid runat="server" id="MyDataGrid" 						    
							Width="100%" CellPadding="4"
							Gridlines="Horizontal" HorizontalAlign="Center"								
							AllowPaging="True"
							PagerStyle-Visible="False" AutoGenerateColumns="false"  >
							<PagerStyle Mode="NumericPages" HorizontalAlign="Right"/>
                            <AlternatingItemStyle BackColor="#E7EAEF" Wrap="false" ></AlternatingItemStyle>
                            <ItemStyle CssClass="tableItem" BackColor="White" Wrap="false" ></ItemStyle>                            
							<HeaderStyle CssClass="tableHeader" BackColor="#ADC1EE" Wrap="false" />	
							<Columns>							    
							    <asp:BoundColumn DataField="ID" HeaderText="Nº Operacion" HeaderStyle-Width="80px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="TipoOperacion" HeaderText="Tipo Operacion" HeaderStyle-Width="80px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="Zona" HeaderText="Zona" HeaderStyle-Width="100px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="TipoPago" HeaderText="Tipo Pago" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="FechaPagoSancion" HeaderText="Fecha Pago Sancion" HeaderStyle-Width="110px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="InicioEstacionamiento" HeaderText="Inicio Estacionamiento" HeaderStyle-Width="120px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" ></asp:BoundColumn>
							    <asp:BoundColumn DataField="FinEstacionamiento" HeaderText="Fin Estacionamiento" HeaderStyle-Width="100px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="Importe" HeaderText="Importe" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ></asp:BoundColumn>
							    <asp:BoundColumn DataField="Matricula" HeaderText="Matricula" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="Articulo" HeaderText="Articulo" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="PostPago" HeaderText="PostPago" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="NºExpediente" HeaderText="Nº Expediente" HeaderStyle-Width="80px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="Sancion" HeaderText="Sancion" HeaderStyle-Width="200px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>	
							</Columns>
						</asp:DataGrid>						
                        
					</div>
					<div style="text-align:right;" class="tableHeader" >
                            <span title="Exportar a Excel" style="margin-right:1px; margin-top:1px" >
                                <asp:ImageButton ID="imgBtnExcel" runat="server" OnClick="ExportToExcel_Click" ImageUrl="~/images/Excel.png" />
                            </span>
                     </div>
                     <br />
					<div class="pageLinks">
						<asp:LinkButton runat="server" CssClass="pageLinks" id="FirstPage" Text="[Inicio]" OnCommand="NavigationLink_Click" CommandName="First" />
						<asp:LinkButton runat="server" CssClass="pageLinks" id="PreviousPage2" Text="[Anterior]" OnCommand="NavigationLink_Click" CommandName="Prev" />
						&nbsp;|&nbsp;
						<strong>Página <asp:Label id="CurrentPage" CssClass="pageLinks" runat="server" /> de <asp:Label id="TotalPages" CssClass="pageLinks" runat="server" /></strong>
						&nbsp;|&nbsp;
						<asp:LinkButton runat="server" CssClass="pageLinks" id="NextPage" Text="[Siguiente]" OnCommand="NavigationLink_Click" CommandName="Next" />
						<asp:LinkButton runat="server" CssClass="pageLinks" id="LastPage" Text="[Última]" OnCommand="NavigationLink_Click" CommandName="Last" />
					</div>
					<br />
					<br />
					<p>Resumen de operaciones:</p>	
					<table id="tbTotals" runat="server">
					    <tr class="rowTotalsHead"></tr>					    
					    <tr class="rowTotals"></tr>
					    <tr class="rowTotalsHead"></tr>
					    <tr class="rowTotals"></tr>
					</table>					
				</form>
			</div>
		<hr />
			<div id="feet">
					<!-- #Include File="OTAfooter.inc" -->
			</div>
		</div>
	
  </body>
</html>
