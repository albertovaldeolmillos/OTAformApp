<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="requestMobileIndex.aspx.cs" Inherits="OTAformApp.requestMobileIndex" %>
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
    <script language="javascript" type="text/javascript" src="formVal.js"></script>
  </head>
  <body MS_POSITIONING="GridLayout">
		<div id="frame">
		  <div id="layoutform">
				<div id="top_menu">
					<form id="conf_form" name="conf_form" action="requestMobilePswd.aspx" method="post" enctype="application/x-www-form-urlencoded">
		                <p>
                            <asp:Localize ID="locRecuperarPassword" Text="Introduzca el correo electrónico de su suscripción para enviarle su nombre de 
			                usuario y contraseña recuperada." runat="server"></asp:Localize>
		                </p>			

		                <%--<label for="control">Matrícula:</label>&nbsp;--%> 
                        <asp:Label ID="lblMatricula" for="control" runat="server" Text="Matrícula:"></asp:Label>&nbsp;
		                <input id="control" name="control" type="text" size="30" title="Introduzca su nº control" onblur="javascript:this.value = checkPlates(this.value);"/>&nbsp;
		                <%--<label for="confEmail">E-mail:</label>&nbsp;--%> 
                        <asp:Label ID="lblConfEmail" for="confEmail" runat="server" Text="E-mail:"></asp:Label>&nbsp;
		                <input id="confEmail" name="confEmail" type="text" size="30" title="Introduzca su cuenta de correo"/>&nbsp;
		                <button id="btnSubmit" type="submit" value="validar" runat="server">Enviar</button>
	                </form>
				</div>				
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