<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="OTAformApp.Summary" %>

<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Formulario de suscripción - Mugipark</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
		<!--meta http-equiv="Content-Type" content="text/html; charset=UTF-8"-->
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
		<meta name="CODE_LANGUAGE" content="C#" />
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<script language="javascript" type="text/javascript" src="formVal.js">
/* SCRIPT TO FORM VALIDATE 	*/
		</script>
		<link rel="stylesheet" type="text/css" href="print.css" media="print" />
	    <link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen" />
	    <link href="formOTA.css" rel="stylesheet" type="text/css" media="screen" />  
	</head>
	<body>
		<div id="frame">
			<div id="layoutform">
				<div id="top_menu">
					<OTAform:header runat="server" id="ctrlHead"/>
				</div>
			
				<h1>
                    <asp:Localize ID="locTitle" runat="server" Text="Servicio de pago remoto del estacionamiento de Mugipark"></asp:Localize>
                </h1>
				<h2>
                    <asp:Localize ID="locSubTitle" runat="server" Text="Conformidad de Suscripción"></asp:Localize>
                </h2>
				<p>
                    <asp:Localize ID="locDescriptionHead" runat="server" Text="Sr./Sra: "></asp:Localize>
                    <strong><span id="nameClient" runat="server"></span></strong>
                    <asp:Localize ID="locDescription" runat="server" Text=", su información ha sido procesada con éxito para poder acceder al &lt;i&gt;&quot;Servicio de 
						pago remoto del estacionamiento de Mugipark &quot; &lt;/i&gt;."></asp:Localize>					
				</p>
				<div id="emailMsg" runat="server">
					<p>
                        <asp:Localize ID="locDescriptionEmail" runat="server" Text="Los Datos de su suscripción se le enviarán a su cuenta de correo. "></asp:Localize>
					</p>
					<p>
					    <strong>(<span id="mailClient" runat="server"></span>)</strong>
					</p>
				</div>
				<p>
                    <asp:Localize ID="locDescriptionFeet" runat="server" Text="Grácias por su confianza."></asp:Localize>
                </p>
                
                <div id="divPrint" runat="server" style="vertical-align:middle; margin-bottom:5px;margin-top:10px;" >                    
                    <asp:Localize ID="locPrintData" runat="server" Text="Si lo desea puede imprimir sus datos de registro:"></asp:Localize>
                    <img ID="imgPrint" height="24" width="24" src="~/images/printerRED.jpg" 
                        style="margin-left: 300px; vertical-align: middle;" runat="server" onclick="window.print();" alt="print" />    
			    </div>
                <div id="divPrintData" runat="server" >
                    <table id="tableDistintivo" width="100%" summary="Recuerde el distintivo">
                        <caption>
                            <asp:Localize ID="locCaptionTableDistintivo" runat="server" Text="Recuerde:"></asp:Localize>
                        </caption>
                        <tr>
                            <td id="msgDistintivo">
                                <p>Para su <b>identificación en el servicio</b>, es necesario que <b>solicite su distintivo</b> al controlador mas cercano, y lo adhiera en el parabrisas de su vehículo.</p>
                                <p><strong>Esta identificación es necesaria ya que usted no dispone de tique físico.</strong></p>
                            </td>
                            <td id="imgDistintivo">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <table id="tablePersonal" width="100%" border="1" summary="Datos personales.">
					        <caption>
                                <asp:Localize ID="locCaptionTablePersonal" runat="server" Text="Datos Personales:"></asp:Localize>
					        </caption>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblName" CssClass="aspLabel" runat="server" Text="Nombre:"></asp:Label>
						        </th>
						        <td>							    
						            <asp:Label ID="name" CssClass="aspText" runat="server" Text=""></asp:Label>
						        </td>
					        </tr>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblSurname1" CssClass="aspLabel" for="surname1" runat="server" Text="Primer apellido:"></asp:Label>
						        </th>
						        <td>							    							   
						            <asp:Label ID="surname1" CssClass="aspText" runat="server" Text=""></asp:Label>
						        </td>
					        </tr>
					        <tr>
						        <th scope="row">								
                                    <asp:Label ID="lblSurname2" CssClass="aspLabel" runat="server" Text="Segundo apellido:"></asp:Label>
						        </th>
						        <td>								
							        <asp:Label ID="surname2" CssClass="aspText" runat="server" Text=""></asp:Label>
						        </td>
					        </tr>
					        <tr class="inpReq">
						        <th scope="row">							    
							        <asp:Label ID="lblDNINIF" CssClass="aspLabel" runat="server" Text="NIF, NIE ó CIF:"></asp:Label>                            
						        </th>
						        <td>							    
							        <asp:Label ID="DNINIF" CssClass="aspText" runat="server" Text=""></asp:Label>
						        </td>
					        </tr>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblTelephone" CssClass="aspLabel" runat="server" Text="Teléfono Móvil 1:"></asp:Label>
						        </th>
						        <td>								
							        <asp:Label ID="telephone" CssClass="aspText" runat="server" Text=""></asp:Label>
						        </td>
					        </tr>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblTelephone2" CssClass="aspLabel"  runat="server" Text="Teléfono Móvil 2:"></asp:Label>
						        </th>
						        <td>								
							        <asp:Label ID="telephone2" CssClass="aspText" runat="server" Text=""></asp:Label>							
						        </td>
					        </tr>						
				        </table>
                    <hr />    
                    <table id="tableFact" width="100%" border="1" summary="Formulario de razón social.">
					        <caption>
                                <asp:Localize ID="locCaptionTableFact" runat="server" Text="Razón social:"></asp:Localize>
                            </caption>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblAdress" CssClass="aspLabel" runat="server" Text="Nombre y número de Calle:"></asp:Label>
                                </th>
						        <td>							    
						            <asp:Label ID="address" CssClass="aspText" runat="server" Text="" style="WIDTH:65%"></asp:Label>
						            , <asp:Label ID="addressNum" CssClass="aspText" runat="server" Text="" style="WIDTH:20%"></asp:Label>
						        </td>
					        </tr>
					        <tr>
						        <th scope="row">								
                                    <asp:Label ID="lblLevel" CssClass="aspLabel" runat="server" Text="Piso / Puerta:" ></asp:Label>
						        </th>
						        <td>							    
						            <asp:Label ID="level" CssClass="aspText" runat="server" 
                                        style="WIDTH:40%! important;"></asp:Label>
							        / <asp:Label ID="door" CssClass="aspText" runat="server" Text="" style="WIDTH:40%! important;"></asp:Label>
						        </td>
					        </tr>
					        <tr>
						        <th scope="row">								
                                    <asp:Label ID="lblStair" CssClass="aspLabel" runat="server" Text="Escalera / Letra:" ></asp:Label>
						        </th>
						        <td>								
							        <asp:Label ID="stair" CssClass="aspText" runat="server" Text="" style="WIDTH:40%! important;"></asp:Label>
							        / <asp:Label ID="letter" CssClass="aspText" runat="server" Text="" style="WIDTH:40%! important;"></asp:Label>
						        </td>
					        </tr>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblPostCode" CssClass="aspLabel" runat="server" Text="Código Postal - Localidad:" ></asp:Label>
						        </th>
						        <td>							    
						            <asp:Label ID="PostCode" CssClass="aspText" runat="server" Text="" style="WIDTH:30%! important;"></asp:Label>
							        - <asp:Label ID="city" CssClass="aspText" runat="server" Text="" style="WIDTH:50%! important;"></asp:Label>
        							
						        </td>
					        </tr>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblProvince" CssClass="aspLabel" runat="server" Text="Provincia:" ></asp:Label>
						        </th>
						        <td>							    
                                    <asp:Label ID="province" CssClass="aspText" runat="server" Text=""></asp:Label>							    
						        </td>
					        </tr>
					        <tr class="inpReq" style="visibility:hidden">
						        <th scope="row">								
                                    <asp:Label ID="lblCountry" CssClass="aspLabel" runat="server" Text="País:" ></asp:Label>
						        </th>
						        <td>							   
						            <asp:Label ID="country" CssClass="aspText" runat="server" Text="ESPANA"></asp:Label>							    
						        </td>
					        </tr>
				        </table>
			        <hr />			     
			        <table id="tableRegistration" width="100%" border="1" summary="Formulario para datos de suscripción">
					        <caption>
                                <asp:Localize ID="locTableRegistration" runat="server" Text="Datos de Suscripción:"></asp:Localize>
					        </caption>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblPlate" CssClass="aspLabel" runat="server" Text="Matrículas asociadas:<br>(1 línea - 1 Matrícula)" for="plates"></asp:Label>
						        </th>
						        <td>
						            <%-- 
						            <textarea id="plates" name="plates" cols="20" rows="2" title="Matrículas" runat="server" style="WIDTH:90%;" disabled="disabled" ></textarea>
							        --%>	
							        <asp:Label ID="plates" CssClass="aspText" runat="server" Text="" style="WIDTH:90%;"></asp:Label>	
							        						
						        </td>
					        </tr>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblUser" CssClass="aspLabel" runat="server" Text="Usuario:" for="user"></asp:Label>
						        </th>
						        <td>							   	
						            <asp:Label ID="user" CssClass="aspText" runat="server" Text="" ></asp:Label>													    
						        </td>
					        </tr>
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblPassword" CssClass="aspLabel" runat="server" Text="Contraseña:" for="password"></asp:Label>
						        </th>
						        <td>							    
						            <asp:Label ID="password" CssClass="aspText" runat="server" Text="" ></asp:Label>
						        </td>
					        </tr>					        
					        <tr class="inpReq">
						        <th scope="row">								
                                    <asp:Label ID="lblEmail" CssClass="aspLabel" for="email" runat="server" Text="Correo Electrónico:"></asp:Label>
						        </th>
						        <td>							    
						            <asp:Label ID="email" CssClass="aspText" runat="server" Text="" ></asp:Label>						   
						        </td>
					        </tr>
				        </table>    
                    <hr />        
                </div>
			</div>	
			
		    <hr />
			<div id="feet">
					<!-- #Include File="OTAfooter.inc" -->
			</div>
		</div>
	</body>
</html>
