<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivationAccount.aspx.cs" Inherits="OTAformApp.ActivationAccount" %>
<%--<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Formulario de Activación de cuenta - Mugipark</title>
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	<!--meta http-equiv="Content-Type" content="text/html; charset=UTF-8"-->
	<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
	<meta name="CODE_LANGUAGE" Content="C#">
	<meta name="vs_defaultClientScript" content="JavaScript">
    <link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen" />	
</head>
<body>
    
        <div id="frame">
            <div id="layoutform">
                <%--<div id="top_menu">
					<OTAform:header runat="server" id="ctrlHead"/>
				</div>--%>
                <h1>
                    <asp:Localize ID="locTitle" runat="server" Text="Servicio de pago remoto del estacionamiento de Mugipark"></asp:Localize>
                </h1>
		        <h2>
                    <asp:Localize ID="locSubTitle" runat="server" Text="Activación de Cuenta"></asp:Localize>
                </h2>
		        <br />                
                <div id="MessageActivation" runat="server"></div>
            </div>         
            <hr />            
            <div id="feet">
                <!-- #Include File="OTAfooter.inc" -->
            </div>            
        </div>        
        
</body>
</html>
