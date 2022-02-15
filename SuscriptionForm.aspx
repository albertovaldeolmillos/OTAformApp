<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SuscriptionForm.aspx.cs" Inherits="OTAformApp.SuscripctionForm" %>
<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Formulario de suscripción - Mugipark</title>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript" />
		<script language="javascript" type="text/javascript" src="formVal.js">
			/* SCRIPT TO FORM VALIDATE 	*/
		</script>
	    <link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen" />
	    <link href="formOTA.css" rel="stylesheet" type="text/css" media="screen" />
	</head>
	<body>
		<div id="frame">
			<div id="layoutform">
				<div id="top_menu">
					<OTAform:header runat="server" id="ctrlHead"/>
				</div>
				<div id="HeadText" runat="server">
				    <hr />
				    <h1>Formulario de alta al servicio de pago remoto del estacionamiento de Mugipark</h1>
				    <h2>Formulario de Suscripción</h2>
				    <h3>1. Datos del suscriptor</h3>
				    <p>Con el pago remoto podrá efectuar estacionamientos de su vehículo o 
					    prolongarlos, desde cualquier punto de nuestra red de servicios, y podrá abonar 
					    las operaciones facilmente con su tarjeta de credito.</p>
				    <p>Los Datos de suscripcion le permitirán modificar sus datos o darse de baja 
					    cuando usted lo desée. Toda la información que usted nos facilita, estará 
					    sujeta a la <a href="POL_PD.html" id="a1" target="_blank" runat="server">&quot;Política de Protección de Datos Personales&quot;</a>
					     del presente sitio web.</p>
				    <p>Por favor, rellene los detalles para poder beneficiarse del servicio remoto del estacionamiento.</p>	
				</div>
				
				<%-- 
				<form id="form1" class="inpText" action="" enctype="application/x-www-form-urlencoded"
					method="post" name="form1" onsubmit="javascript:return formVal();" target="_self"
					runat="server">	
				--%>
				<%-- Pruebas TRANSAX "onsubmit" se ha eliminado temporalmente para saltarnos la validacion. --%>
				<form id="form1" class="inpText" action="" enctype="application/x-www-form-urlencoded" method="post" name="form1" target="_self" runat="server">
                <asp:HiddenField ID="token" runat="server" />
				<%-- /////////////////////////////////////////////////////////////////////////////          --%>
				
					<a name="errorMsg" id="errorMsg" href="#errorMsg"></a>
					<div id="formError" runat="server">                        
						<p>
                            <asp:Localize ID="locAtention" runat="server" Text="Atención, compruebe los siguientes datos:"></asp:Localize>
                        </p>
						<ul id="ulerrors" runat="server">
							<!-- ERROR LIST -->
							<asp:RequiredFieldValidator id="rfvName" ControlToValidate="name" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#name">Campo requerido &quot;Nombre&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvSurname1" ControlToValidate="surname1" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#surname1">Campo requerido &quot;Primer Apellido&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvDNINIF" ControlToValidate="DNINIF" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#DNINIF" id="linkDNINIF" runat="server">Campo requerido &quot;NIF ó CIF&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvAddress" ControlToValidate="address" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#address">Campo requerido &quot;Nombre de Calle&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvPostCode" ControlToValidate="PostCode" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#PostCode">Campo requerido &quot;Código Postal&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvCity" ControlToValidate="city" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#city">Campo requerido &quot;Localidad&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvProvince" ControlToValidate="province" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#province">Campo requerido &quot;Provincia&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvCountry" ControlToValidate="country" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#country">Campo requerido &quot;País&quot;</a></li>
							</asp:RequiredFieldValidator>							
							<asp:RequiredFieldValidator id="rfvTelephone" ControlToValidate="telephone" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#telephone">Campo requerido &quot;Teléfono Móvil&quot;</a></li>
							</asp:RequiredFieldValidator>							
							<asp:RequiredFieldValidator id="rfvUser" ControlToValidate="user" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#user">Campo requerido &quot;Usuario&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvPassword" ControlToValidate="password" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#password">Campo requerido &quot;Contraseña&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvPasswordChk" ControlToValidate="passwordChk" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#passwordChk">Campo requerido &quot;Repetir contraseña&quot;</a></li>
							</asp:RequiredFieldValidator>
							<asp:RequiredFieldValidator id="rfvEmail" ControlToValidate="email" Display="Static" Width="100%"
								runat="server" ForeColor="">
								<li><a href="#email">Campo requerido &quot;Correo Electrónico&quot;</a></li>
							</asp:RequiredFieldValidator>
						</ul>
						<p align="right">Gracias</p>
					</div>
					<p class="legend">
                        <asp:Localize ID="locLegend" runat="server" Text="Los campos marcados con '&lt;strong&gt;&lt;span 
    title=&quot;Campo obligatorio&quot; style=&quot;COLOR:#a33&quot;&gt;*&lt;/span&gt;&lt;/strong&gt;' 
						    deben ser rellenados obligatoriamente. "></asp:Localize>
					</p>
					<table id="tablePersonal" width="100%" border="1" summary="Formulario de datos personales.">
						<caption>
                            <asp:Localize ID="locCaptionTablePersonal" runat="server" Text="Datos Personales:"></asp:Localize>
						</caption>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblName" for="name" runat="server">Nombre:</label>--%>
                                <asp:Label ID="lblName" CssClass="aspLabel" runat="server" for="name" Text="Nombre:"></asp:Label>
							</th>
							<td><asp:TextBox ID="name" Runat="server"></asp:TextBox><!--input id="name" name="name" type="text"-->
							    <span id="spFieldOblig1" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblSurname1" for="surname1">Primer apellido:</label>--%>
                                <asp:Label ID="lblSurname1" CssClass="aspLabel" for="surname1" runat="server" Text="Primer apellido:"></asp:Label>
							</th>
							<td><input id="surname1" name="surname1" type="text" runat="server">
							    <span id="spFieldOblig2" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr>
							<th scope="row">
								<%--<label id="lblSurname2" for="surname2">Segundo apellido:</label>--%>
                                <asp:Label ID="lblSurname2" CssClass="aspLabel" for="surname2" runat="server" Text="Segundo apellido:"></asp:Label>
							</th>
							<td>
								<input id="surname2" name="surname2" type="text" runat="server">
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
							    <%--
								<label id="lblDNINIF" for="DNINIF">NIF, NIE ó CIF:</label> 
								--%>   
								<asp:Label ID="lblDNINIF" CssClass="aspLabel" for="DNINIF" runat="server" Text="NIF, NIE ó CIF:"></asp:Label>                            
							</th>
							<td>
							    <input id="DNINIF" name="DNINIF" type="text" onchange="javascript:this.value=this.value.toUpperCase();" runat="server">
								<span id="spFieldOblig3" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblTelephone" for="telephone">Teléfono Móvil:</label>--%>
                                <asp:Label ID="lblTelephone" CssClass="aspLabel" for="telephone" runat="server" Text="Teléfono Móvil 1:"></asp:Label>
							</th>
							<td>
								<input id="telephone" name="telephone" type="text" runat="server"> 
								<span id="spFieldOblig4" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr>
							<th scope="row">
								<%--<label id="lblTelephone" for="telephone">Teléfono Móvil:</label>--%>
                                <asp:Label ID="lblTelephone2" CssClass="aspLabel" for="telephone2" runat="server" Text="Teléfono Móvil 2:"></asp:Label>
							</th>
							<td>
								<input id="telephone2" name="telephone2" type="text" runat="server"> 								
							</td>
						</tr>
						<%-- 
						<tr class="inpReq">
							<th scope="row">
								<!--<label id="lblTelCompany" for="telCom">Compañía del Móvil:</label>-->								
                                <asp:Label ID="lblTelCompany" CssClass="aspLabel" for="telCom" runat="server" Text="Compañía del Móvil:"></asp:Label>
							</th>
							<td>
								<select id="telCom" name="telCom" runat="server">
									<option id="optTelCom" value="0" selected="true">Seleccione compañía…</option>
								</select>
								<span id="spFieldOblig5" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						--%>
					</table>
					<hr>
					<table id="tableFact" width="100%" border="1" summary="Formulario de razón social.">
						<caption>
                            <asp:Localize ID="locCaptionTableFact" runat="server" Text="Razón social:"></asp:Localize>
                        </caption>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblAdress" for="address">Nombre y número de Calle:</label>--%>
                                <asp:Label ID="lblAdress" CssClass="aspLabel" runat="server" Text="Nombre y número de Calle:"></asp:Label>
                            </th>
							<td><input id="address" name="address" type="text" style="WIDTH:65%" runat="server">
							    <span id="spFieldOblig18" title="Campo obligatorio" runat="server">,</span>
							    <input id="addressNum" name="addressNum" type="text" style="WIDTH:20%" runat="server">
								<span id="spFieldOblig6" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr>
							<th scope="row">
								<%--<label id="lblLevel" for="level">Piso / Puerta:</label>--%>
                                <asp:Label ID="lblLevel" CssClass="aspLabel" runat="server" Text="Piso / Puerta:" for="level"></asp:Label>
							</th>
							<td><input id="level" name="level" type="text" style="WIDTH:40%! important" runat="server">
								/ <input id="door" name="door" type="text" style="WIDTH:40%! important" runat="server">
							</td>
						</tr>
						<tr>
							<th scope="row">
								<%--<label id="lblStair" for="stair">Escalera / Letra:</label>--%>
                                <asp:Label ID="lblStair" CssClass="aspLabel" runat="server" Text="Escalera / Letra:" for="stair"></asp:Label>
							</th>
							<td>
								<input id="stair" name="stair" type="text" size="10" style="WIDTH:40%! important" runat="server">
								/ <input id="letter" name="letter" type="text" size="10" style="WIDTH:40%! important" runat="server">
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblPostCode" for="PostCode">Código Postal - Localidad:</label>--%>
                                <asp:Label ID="lblPostCode" CssClass="aspLabel" runat="server" Text="Código Postal - Localidad:" for="PostCode"></asp:Label>
							</th>
							<td><input id="PostCode" name="PostCode" type="text" style="WIDTH:30%! important" runat="server">
								- <input id="city" name="city" type="text" style="WIDTH:50%! important" runat="server">
								<span id="spFieldOblig7" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblProvince" for="province">Provincia:</label>--%>
                                <asp:Label ID="lblProvince" CssClass="aspLabel" runat="server" Text="Provincia:" for="province"></asp:Label>
							</th>
							<td>
							    <input id="province" name="province" type="text" runat="server">
                                <%--<select id="province" name="province" runat="server">
									<option value="0" selected="true">Seleccione Provincia…</option>
								</select>--%>
							    <span id="spFieldOblig8" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr class="inpReq" style="visibility:hidden">
							<th scope="row">
								<%--<label id="lblCountry" for="country">País:</label>--%>
                                <asp:Label ID="lblCountry" CssClass="aspLabel" runat="server" Text="País:" for="country"></asp:Label>
							</th>
							<td><input id="country" name="country" type="text" runat="server" value="ESPANA" >
							    <span id="spFieldOblig9" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
					</table>					
					<hr>
					<table id="tableRegistration" width="100%" border="1" summary="Formulario para datos de suscripción">
						<caption>
                            <asp:Localize ID="locTableRegistration" runat="server" Text="Datos de Suscripción:"></asp:Localize>
						</caption>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblPlate" for="plates">Matrículas asociadas:<br>(1 línea - 1 Matrícula)</label>--%>
                                <asp:Label ID="lblPlate" CssClass="aspLabel" runat="server" Text="Matrículas asociadas:<br>(1 línea - 1 Matrícula)" for="plates"></asp:Label>
							</th>
							<td><textarea id="plates" name="plates" cols="20" rows="2" title="Matrículas" runat="server" style="WIDTH:90%"
									onchange="javascript:this.value = checkPlates(this.value);"></textarea>
								<span id="spFieldOblig13" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblUser" for="user">Usuario:</label>--%>
                                <asp:Label ID="lblUser" CssClass="aspLabel" runat="server" Text="Usuario:" for="user"></asp:Label>
							</th>
							<td><input id="user" name="user" type="text" runat="server">
							    <span id="spFieldOblig14" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblPassword" for="password">Contraseña:</label>--%>
                                <asp:Label ID="lblPassword" CssClass="aspLabel" runat="server" Text="Contraseña:" for="password"></asp:Label>
							</th>
							<td><input id="password" name="password" type="password" runat="server">
							    <span id="spFieldOblig15" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblChkPassword" for="passwordChk">Repetir contraseña:</label>--%>
                                <asp:Label ID="lblChkPassword" CssClass="aspLabel" runat="server" Text="Repetir contraseña:" for="passwordChk"></asp:Label>
							</th>
							<td><input id="passwordChk" name="passwordChk" type="password" runat="server">
							    <span id="spFieldOblig16" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
						<tr class="inpReq">
							<th scope="row">
								<%--<label id="lblEmail" for="email">Correo Electrónico:</label>--%>
                                <asp:Label ID="lblEmail" CssClass="aspLabel" for="email" runat="server" Text="Correo Electrónico:"></asp:Label>
							</th>
							<td><input id="email" name="email" type="text" runat="server">
							    <span id="spFieldOblig17" title="Campo obligatorio" runat="server"> * </span>
							</td>
						</tr>
					</table>
					<hr>
					<div id="formConditions" align="left">
						<p>
							<input id="conditions" name="conditions" type="checkbox" onclick="javascript:enableButtons(this);">
                            <asp:Localize ID="locConditions" runat="server" Text="He leido y estoy de acuerdo con la &lt;a href=&quot;POL_PD.html&quot; target=&quot;_blank&quot;&gt;&amp;quot;Política de Protección de 
								Datos Personales&amp;quot;&lt;/a&gt; del presente sitio web."></asp:Localize>
						</p>
					</div>
					<div id="formButtons" style="TEXT-ALIGN:center">                        
                        <button id="submit" name="submit" type="submit" value="submit" disabled="disabled" runat="server">Siguiente</button>  
						<button id="reset" name="reset" type="reset" value="reset" runat="server">Borrar</button>						
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