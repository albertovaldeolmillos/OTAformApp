<%@ Page language="c#" Codebehind="errorForm.aspx.cs" AutoEventWireup="True" Inherits="OTAformApp.errorForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>Formulario de suscripción - Mugipark</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!--meta http-equiv="Content-Type" content="text/html; charset=UTF-8"-->
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<script language="javascript" type="text/javascript" src="formVal.js">
/* SCRIPT TO FORM VALIDATE 	*/
		</script>
		<link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen">
	</HEAD>
	<body>		
		<div id="frame">
			<div id="layoutform">
			    <div id="top_menu"></div>
				<h1>
                    <asp:Localize ID="locTitle" runat="server" Text="Servicio de pago remoto del estacionamiento de Mugipark"></asp:Localize>
                </h1>
				<h2 id="msgError" runat="server"></h2>
				<div id="bodyError" runat="server">
                    <p>Sentimos comunicarle que ha occurrido un error al procesar su información.</p>
                    <p>Rogamos que vuelva a intentarlo y si el error persiste, póngase en contacto con nosotros mediante <a href="http://www.mugipark.com/" target="_blank">Nuestra web</a></p>
                    <p>Disculpen las molestias.</p>				
				</div>
			</div>
		<hr />
			<div id="feet">
				<!-- #Include File="OTAfooter.inc" -->
			</div>
		</div>
	</body>
</HTML>
