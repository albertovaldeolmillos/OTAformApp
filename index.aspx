<%@ Page language="c#" Codebehind="index.aspx.cs" AutoEventWireup="True" Inherits="OTAformApp.index" %>
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
	<%--<script language="javascript" type="text/javascript" src="formVal.js"></script>--%>
  </head>
  <body MS_POSITIONING="GridLayout">
		<div id="frame">
		  <div id="layoutform">
				<div id="top_menu">
					<OTAform:header runat="server" id="ctrlHead"/>
				</div>
				<hr />
				<div id="InfoService" runat="server">
                    <h1>Servicio de pago remoto por móvil y web del estacionamiento de Mugipark</h1>
                    <h2>Información del servicio</h2>
                    <p>Bienvenidos al <strong>&quot;Servicio de pago remoto por móvil y web del estacionamiento de Mugipark&quot;</strong> de <strong>
                     <abbr title="Servicio del Estacionamiento Regulado">Mugipark</abbr></strong>, donde podrá abonar y prolongar los estacionamientos 
                     de su/s vehículo/s, desde cualquier punto de nuestra red de servicios, 
                     fácilmente con su tarjeta de crédito.</p>
                     <p>Facilitando los datos requeridos para la suscripción, podrá darse de alta y tener acceso a dicho servicio.</p>
				</div>
				
				<div id="Ventajas" runat="server">
                    <h3>Ventajas del servicio:</h3>
                    <p>La principal ventaja de este servicio es que podrá realizar operaciones del estacionamiento regulado,
                    sin necesidad de realizarlas presencialmente en un parquímetro.
                    El sistema reconocerá sus operaciones sin necesidad de dejar el tiquet en el vehículo, ahorrándose así su tiempo.</p>
                    <p>Operaciones que podrá realizar remotamente:</p>
                    <ul>
                        <li>Pagos de Tique.</li>
                        <li>Devoluciones.</li>
                        <li>Pagos de Denuncia.</li>
                    </ul>				
				</div>
								
				<div id="Ampliation" runat="server">
				    <h3>Ampliación del estacionamiento:</h3>
				    <p>El sistema permite ampliar el tiempo de estacionamiento, siempre y cuando se respete el límite horario del color de la zona.
				    Este servicio le permitira realizar la ampliación desde cualquier lugar sin necesidad de volver al vehículo a colocar el nuevo ticket.</p> 
				</div>
				
				<div id="Devolution" runat="server">
				    <h3>Devoluciones:</h3>
				    <p>El sistema permite la devolución correspondiente al tiempo de estacionamiento abonado y no consumido. Seleccionando la opción correspondiente el sistema permitirá “desaparcar” su vehículo de forma que únicamente se abone el tiempo realmente estacionado.</p>
				</div>
				
				<div id="Santions" runat="server">
				    <h3>Pago de sanciones:</h3>
				    <p>Mientras el expediente de sanción esté dentro del rango administrativo, usted podrá introducir el número de sanción en nuestro sistema y automaticamente se anulará dicha sanción,<span style="font-size: 12pt; color: #000000; font-family: 'Times New Roman','serif';
                        mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: ES; mso-fareast-language: ES;
                        mso-bidi-language: AR-SA"> sin necesidad de acercarse a un parquímetro para realizar
                        la operación</span>.</p>
                </div>               
                <div id="Alta" runat="server">
				    <h3>Cómo darse de alta:</h3>
				    <p>Los Datos de suscripción le permitirán modificar sus datos o darse de baja cuando usted lo desée.
				    Toda la información que usted nos facilita, estará sujeta a la <a href="POL_PD.html" target="_blank">&quot;Política de Protección de Datos
				    Personales&quot;</a> del presente sitio web.</p>		
				    <p>Para darse de alta hay dos modalidades:</p>
				    <dl class="box">
					    <dt><strong>Online:</strong></dt>
					    <dd>Mediante el <a href="SuscriptionForm.aspx">formulario de suscripción</a> expuesto en nuestra red, podrá dejar los datos requeridos para la suscripción, y automáticamente se le dara de alta a nuestro servicio.</dd>
					    <dd><center>
					        <button accesskey="O" tabindex="4" type="button" onclick="document.location = './SuscriptionForm.aspx'">Darse de alta &quot;Online&quot;</button>
					        </center>
					    </dd>					    
				    </dl>
				</div>
			    <hr />
			    
			    <div id="Cost" runat="server">			    
				    <h3>Coste del servicio:</h3>
					<p>El servicio de pago por remoto (móvil/web) es sin coste de mantenimiento.</p>
				</div>
				<div id="Pago" runat="server">
				    <h3>Pago de las estancias</h3>
				    <p>El sistema funciona como una tarjeta de recarga virtual (prepago), en la que usted gestiona su saldo recargándola con su tarjeta de crédito. Las estancias le serán descontadas conforme las realice. Si su saldo es insuficiente, no podrá efectuar ninguna operación.</p>
                </div>
 			    <div id="Conditions" runat="server">			    
				    <h3>Condiciones del servicio:</h3>
					<p>Este servicio está sujeto a unas <a href="POL_PD.html#tInfo" target="_blank">condiciones generales</a> para su correcto uso. Lea con detenimiento dichas condiciones de su interés para entender el funcionamiento del sistema y comprobar que toda la información que usted nos facilite, está protegída por nuetra <a href="POL_PD.html#tLOPD" target="_blank">política de protección de datos</a>.</p>
					<p>Le agradecemos de antemano su confianza </p>
					<p>Gracias</p>
				</div>		
			</div>
		<hr />
			<div id="feet">
					<!-- #Include File="OTAfooter.inc" -->
			</div>
		</div>
  </body>
</html>
