<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Intro_Data.aspx.cs" Inherits="OTAformApp.Intro_Data" %>
<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">  
    <title>´Suscripción - <%= ConfigurationSettings.AppSettings["titPages"] %></title>
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <script language="javascript" type="text/javascript" src="formVal.js"></script>
    <link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen" /> 
    <link href="formOTA.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css" media="screen">
	        .formatRow {font-family:Georgia,"Times New Roman",Times,serif;font-weight:bold;color:#033;font-size:0.9em;}	         
	</style>	   	    
</head>
			
<body MS_POSITIONING="GridLayout">
    <div id="frame">        
	    <div id="layoutform">
		    <%--<div id="top_menu">
			    <OTAform:header runat="server" id="ctrlHead"/>
		    </div>--%>
	        <hr />
		    <h1 id="title">Servicio de pago remoto del estacionamiento</h1>
		    <h2 id="tsubtitle">Punto de Acceso</h2>
		    
		    <form id="form1" onsubmit="javascript:return accesVal();" action="" enctype="application/x-www-form-urlencoded"
		        method="post" target="_self" runat="server">
                <div>
                    <div id="formError" runat="server">
                        <p>ATENCIÓN!, compruebe los siguientes datos:</p>
						<ul id="ulerrors" runat="server">
						    <asp:RequiredFieldValidator id="rqvLogin" ControlToValidate="txtLogin" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li>Campo requerido &quot;Usuario&quot;</li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="RequiredFieldValidator1" ControlToValidate="txtPassword" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li>Campo requerido &quot;Contraseña&quot;</li>
							</asp:RequiredFieldValidator>						    
                        </ul>
						<p align="right">Gracias.</p>
                    </div>                  
                    <table id="tablePersonal">
                        <tr>
                            <td class="formatRow" align="right">Usuario: </td>
                            <td><input id="txtLogin" type="text" runat="server" style="width:200px" /></td>                                                    
                        </tr>
                        <tr>
                            <td class="formatRow" align="right">Contraseña: </td>
                            <td><input id="txtPassword" type="password" runat="server" style="width:200px" /></td>                            
                        </tr>                                               
                    </table>    
                </div>
                <br />
                <div id="formButtons" style="TEXT-ALIGN:center">
				    <button id="submit" name="submit" type="submit" value="submit">Entrar</button>					
				</div>
            </form>
	    </div>
	    <hr />
			<div id="feet">
					<!-- #Include File="OTAfooter.inc" -->
			</div>
	</div>
</body>
</html>
