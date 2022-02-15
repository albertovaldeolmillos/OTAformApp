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
                    <h1>Servicio de pago remoto por m�vil y web del estacionamiento de Mugipark</h1>
                    <h2>Informaci�n del servicio</h2>
                    <p>Bienvenidos al <strong>&quot;Servicio de pago remoto por m�vil y web del estacionamiento de Mugipark&quot;</strong> de <strong>
                     <abbr title="Servicio del Estacionamiento Regulado">Mugipark</abbr></strong>, donde podr� abonar y prolongar los estacionamientos 
                     de su/s veh�culo/s, desde cualquier punto de nuestra red de servicios, 
                     f�cilmente con su tarjeta de cr�dito.</p>
                     <p>Facilitando los datos requeridos para la suscripci�n, podr� darse de alta y tener acceso a dicho servicio.</p>
				</div>
				
				<div id="Ventajas" runat="server">
                    <h3>Ventajas del servicio:</h3>
                    <p>La principal ventaja de este servicio es que podr� realizar operaciones del estacionamiento regulado,
                    sin necesidad de realizarlas presencialmente en un parqu�metro.
                    El sistema reconocer� sus operaciones sin necesidad de dejar el tiquet en el veh�culo, ahorr�ndose as� su tiempo.</p>
                    <p>Operaciones que podr� realizar remotamente:</p>
                    <ul>
                        <li>Pagos de Tique.</li>
                        <li>Devoluciones.</li>
                        <li>Pagos de Denuncia.</li>
                    </ul>				
				</div>
								
				<div id="Ampliation" runat="server">
				    <h3>Ampliaci�n del estacionamiento:</h3>
				    <p>El sistema permite ampliar el tiempo de estacionamiento, siempre y cuando se respete el l�mite horario del color de la zona.
				    Este servicio le permitira realizar la ampliaci�n desde cualquier lugar sin necesidad de volver al veh�culo a colocar el nuevo ticket.</p> 
				</div>
				
				<div id="Devolution" runat="server">
				    <h3>Devoluciones:</h3>
				    <p>El sistema permite la devoluci�n correspondiente al tiempo de estacionamiento abonado y no consumido. Seleccionando la opci�n correspondiente el sistema permitir� �desaparcar� su veh�culo de forma que �nicamente se abone el tiempo realmente estacionado.</p>
				</div>
				
				<div id="Santions" runat="server">
				    <h3>Pago de sanciones:</h3>
				    <p>Mientras el expediente de sanci�n est� dentro del rango administrativo, usted podr� introducir el n�mero de sanci�n en nuestro sistema y automaticamente se anular� dicha sanci�n,<span style="font-size: 12pt; color: #000000; font-family: 'Times New Roman','serif';
                        mso-fareast-font-family: 'Times New Roman'; mso-ansi-language: ES; mso-fareast-language: ES;
                        mso-bidi-language: AR-SA"> sin necesidad de acercarse a un parqu�metro para realizar
                        la operaci�n</span>.</p>
                </div>               
                <div id="Alta" runat="server">
				    <h3>C�mo darse de alta:</h3>
				    <p>Los Datos de suscripci�n le permitir�n modificar sus datos o darse de baja cuando usted lo des�e.
				    Toda la informaci�n que usted nos facilita, estar� sujeta a la <a href="POL_PD.html" target="_blank">&quot;Pol�tica de Protecci�n de Datos
				    Personales&quot;</a> del presente sitio web.</p>		
				    <p>Para darse de alta hay dos modalidades:</p>
				    <dl class="box">
					    <dt><strong>Online:</strong></dt>
					    <dd>Mediante el <a href="SuscriptionForm.aspx">formulario de suscripci�n</a> expuesto en nuestra red, podr� dejar los datos requeridos para la suscripci�n, y autom�ticamente se le dara de alta a nuestro servicio.</dd>
					    <dd><center>
					        <button accesskey="O" tabindex="4" type="button" onclick="document.location = './SuscriptionForm.aspx'">Darse de alta &quot;Online&quot;</button>
					        </center>
					    </dd>					    
				    </dl>
				</div>
			    <hr />
			    
			    <div id="Cost" runat="server">			    
				    <h3>Coste del servicio:</h3>
					<p>El servicio de pago por remoto (m�vil/web) es sin coste de mantenimiento.</p>
				</div>
				<div id="Pago" runat="server">
				    <h3>Pago de las estancias</h3>
				    <p>El sistema funciona como una tarjeta de recarga virtual (prepago), en la que usted gestiona su saldo recarg�ndola con su tarjeta de cr�dito. Las estancias le ser�n descontadas conforme las realice. Si su saldo es insuficiente, no podr� efectuar ninguna operaci�n.</p>
                </div>
 			    <div id="Conditions" runat="server">			    
				    <h3>Condiciones del servicio:</h3>
					<p>Este servicio est� sujeto a unas <a href="POL_PD.html#tInfo" target="_blank">condiciones generales</a> para su correcto uso. Lea con detenimiento dichas condiciones de su inter�s para entender el funcionamiento del sistema y comprobar que toda la informaci�n que usted nos facilite, est� proteg�da por nuetra <a href="POL_PD.html#tLOPD" target="_blank">pol�tica de protecci�n de datos</a>.</p>
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
