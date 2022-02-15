<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetFunds.aspx.cs" Inherits="OTAformApp.GetFunds" %>
<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Saldo - <%= ConfigurationSettings.AppSettings["titPages"] %></title>
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
	    <form id="f1" runat="server" method="post">
	         <div id="message_initial" runat="server" style="border: 2px solid black; background-color: white; position:absolute;top:40%;left:50%;overflow: show; padding: 10px; width: 900px; margin-left: -450px;">
				        <table border="0" width="100%">
				            <tr>
				                <td align="center">AVISO<br /><br /></td>
				            </tr>
				            <tr>
				                <td align="left">
				                    Estimado usuario: con el afán de mejorar nuestro servicio y la seguridad del mismo, hemos cambiado el formato  del sistema de pago por web y móvil del que usted es usuario.<br /><br />
                                    El sistema se convierte en un sistema de prepago de manera que usted mismo puede gestionar sus recargas y su saldo. De este saldo se irá descontando tanto la cuota mensual de 1€, 
                                    como las estancias que usted realice. Para paliar los inconvenientes que este cambio le pueda ocasionar, no se harán efectivas las cuotas de mantenimiento mensuales hasta este mes de noviembre de 2011.<br /><br />
                                    Esperamos que estos cambios sean de su agrado.<br /><br /> 
                                    Att. Mugipark	
				                </td>
				            </tr>
				            <tr>
				                <td align="center">
				                    <br /><br /><br />
                                        <asp:Button ID="Button1" runat="server" Text="Aceptar" OnClick="bttnHideMessage_OnClick" />
				                    <br /><br />                                
                                    <br />
				                </td>
				            </tr>                       
                        </table>
				    </div>
            
		        <div id="frame">
			<div id="layoutform">
				    <div id="top_menu">
					    <OTAform:header runat="server" id="ctrlHead"/>
				    </div>
			        <hr />
				    <h1 id="title">Servicio de pago remoto del estacionamiento</h1>
				    <h2 id="tsubtitle">Saldo</h2>	
				    <div id="UserData" class="userData" runat="server"></div>	
				    <table id="message_tbl" runat="server" cellpadding="6" cellspacing="2" style="border: 2px solid red;">
				        <tr >
				            <td >
				                <div id="message_txt" runat="server"></div>
				            </td>
				        </tr>
				        <tr>
				            <td align="center"><button accesskey="0" tabindex="4" type="button" onclick="document.location = './SetFunds.aspx'">Recargar</button></td>
				        </tr>
				    </table>
				    <br />
				    <br />
				    
				   
				
                
				</div>
		<hr />
			<div id="feet">
					<!-- #Include File="OTAfooter.inc" -->
			</div>
		</div>
        </form>
 
  </body>
</html>
