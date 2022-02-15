<%@ Page language="c#" Codebehind="delete_user.aspx.cs" AutoEventWireup="True" Inherits="OTAformApp.delete_user" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML>
	<HEAD>
		<title>Formulario de suscripci�n - Mugipark</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<!--meta http-equiv="Content-Type" content="text/html; charset=UTF-8"-->
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<script language="javascript" type="text/javascript" src="formVal.js">
/* SCRIPT TO FORM VALIDATE 	*/
		</script>
		<link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen" />
	</HEAD>
	<body>
		<div id="frame">
			<div id="layoutform">
				<div id="top_menu">
					<a href="~/index.aspx" id="lnkReturn" runat="server">Volver</a>
				</div>
				<h1>
                    <asp:Localize ID="locTitle" runat="server" Text="Servicio de pago remoto del estacionamiento - OTA"></asp:Localize>
                </h1>
				<h2>
                    <asp:Localize ID="locSubTitle" runat="server" Text="Baja de Suscripci�n"></asp:Localize>
                </h2>
				<p>
                    <asp:Localize ID="locIniUser" runat="server" Text="Sr./Sra: "></asp:Localize>
				    <strong><span id="nameClient" runat="server"></span> </strong>, 
                    <asp:Localize ID="locUser" runat="server" Text="su informaci�n a sido procesada con �xito."></asp:Localize> 
				</p>
				<p>
                    <asp:Localize ID="locBay" runat="server" 
                        Text="Gracias por su confianza en utilizar nuestro &lt;i&gt;&amp;quot;Servicio de pago remoto del estacionamiento &quot;&lt;/i&gt;"></asp:Localize>
                </p>
			</div>
			<hr />
			<div id="feet">
				<!-- #Include File="OTAfooter.inc" -->
			</div>
		</div>
	</body>
</HTML>
