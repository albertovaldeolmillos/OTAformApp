<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetFunds.aspx.cs" Inherits="OTAformApp.SetFunds" %>
<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Recarga - <%= ConfigurationSettings.AppSettings["titPages"] %></title>
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
	    <script language="javascript" type="text/javascript">

	        function TestAmount() {

	            var bResult = true;
	            var regex = new RegExp("\\d+");

	            var oper = document.getElementById("DS_OPER").value;

	            if (oper == "1") {

	                var txtAmount = document.getElementById("txtAmount");

	                if (txtAmount.value.length != 0 && (!regex.test(txtAmount.value) || isNaN(txtAmount.value))) {
	                    alert("El importe debe ser numérico");
	                    txtAmount.focus();
	                    bResult = false;
	                }

	                if (parseInt(txtAmount.value) < 20) {
	                    alert("El importe debe ser superior a 20€.");
	                    txtAmount.focus();
	                    bResult = false;

	                }
	            }

	            return bResult;

	        }
	    
	    
	    
	    </script>
	        
	</head>
  <body MS_POSITIONING="GridLayout">
	
		<div id="frame">
			<div id="layoutform">
				<div id="top_menu">
					<OTAform:header runat="server" id="ctrlHead"/>
				</div>
			    <hr />
				<h1 id="title">Servicio de pago remoto del estacionamiento</h1>
				<h2 id="tsubtitle">Recarga</h2>	
				<div id="UserData" class="userData" runat="server"></div>	
				<br />
				<br />
				<form runat="server" method="post" id="form1"  target="_self" onsubmit="return TestAmount();">
				<div id="selection" runat="server">
				    <p>Seleccione el importe a realizar en la recarga.</p>					
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            <asp:ListItem Value="500" Text="5 Euros" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1000" Text="10 Euros"></asp:ListItem>
                            <asp:ListItem Value="1500" Text="15 Euros"></asp:ListItem>
                            <asp:ListItem Value="2000" Text="20 Euros"></asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                    <asp:TextBox ID="txtAmount" runat="server" MaxLength="3" Width="40px"></asp:TextBox> Para importes superiores a 20 € rellene la casilla.
                    <br />
                    <br />
                </div>
                <div id="confirm" runat="server">
                    <p id="confirm_message" runat="server">Ha seleccionado un importe de x Euros para realizar la recarga. Si está conforme pulse el botón Recargar. Se le transferirá al servidor de pago externo.</p>
                </div>
                
                <asp:HiddenField ID="DS_SIGNATUREVERSION" runat="server" Value="HMAC_SHA256_V1"  />
                <asp:HiddenField ID="DS_MERCHANTPARAMETERS" runat="server" Value=""  />
                <asp:HiddenField ID="DS_SIGNATURE" runat="server" Value=""  />
                <asp:HiddenField ID="DS_OPER" runat="server"  value="0"/>
               
                <input id="Submit1" type="submit" value="Enviar" runat="server" onclick="document.getElementById('DS_OPER').value='1';" />
                <br />
                <br />
                <p>Extracto de operaciones de recarga en el mes de <strong><%= _period %></strong></p>
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
							    <asp:BoundColumn DataField="Fecha" HeaderText="Fecha" HeaderStyle-Width="80px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>
							    <asp:BoundColumn DataField="Importe" HeaderText="Importe" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" ></asp:BoundColumn>
							    <asp:BoundColumn DataField="Resultado" HeaderText="Resultado" HeaderStyle-Width="200px" ItemStyle-Wrap="false" HeaderStyle-Wrap="false"  ></asp:BoundColumn>	
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
										
				
				</form>
                
				</div>
		<hr />
			<div id="feet">
					<!-- #Include File="OTAfooter.inc" -->
			</div>
		</div>
	
  </body>
</html>