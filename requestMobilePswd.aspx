<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="requestMobilePswd.aspx.cs" Inherits="OTAformApp.requestMobilePswd" %>
<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
    <title>Inicio - <%=ConfigurationSettings.AppSettings["titPages"]%></title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name=vs_defaultClientScript content="JavaScript">
    <meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="description" name="description" content="<%=ConfigurationSettings.AppSettings["webDesc"]%>">
	<link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen" />
  </head>
  <body MS_POSITIONING="GridLayout">
		<div id="frame">
			<div id="layoutform">
				<div id="top_menu">					
				</div>
				<hr />						
					<h1>
                        <asp:Localize ID="locTitle" runat="server" Text="Servicio de pago remoto del estacionamiento"></asp:Localize>
                    </h1>
					<h2>
					    <asp:Localize ID="locSubTitle" runat="server" Text="Recuperación de Datos"></asp:Localize>
					</h2>
					<p>
                        <asp:Localize ID="locBody" runat="server" Text="Usted ha solicitado la recuperación de los datos vinculados a la cuenta de correo"></asp:Localize>  
					    <strong>"<%= email%>"</strong>
					</p>
					<div id="divResult" runat="server"></div>
			</div>
			<hr />
			<div id="feet">
					<div align="center" style="margin-bottom:10px; color:White;">	                   
                    </div>
                    <address style="font-size:.9em; color:White;">
	                    <strong>Mugipark</strong> 
	                     · 
	                    <a href="http://www.mugipark.com/" target="_blank">Mugipark Web</a>
                    </address>
			</div>
		</div>
	</body>
</html>
