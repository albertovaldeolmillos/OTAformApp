<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ctrlHead.ascx.cs" Inherits="OTAformApp.ctrlHead" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script language="javascript" type="text/javascript" src="formVal.js"></script>
<%-- ERROR LOG	--%>
<%	if (Session["errorLog"] != null) { %>
<div id="alert" >
	<%--<span style="COLOR:#cc0000"><%=Session["errorLog"].ToString()%></span>&nbsp;|&nbsp;--%> 
	<span style="COLOR:#cc0000"><%=Session["errorLog"].ToString() %></span>&nbsp;|&nbsp;
	<a href="#" id="lnkRecuperarPassword" onclick="javascript:document.getElementById('conf_form').style.display='block'" runat="server">¿Recuperar contraseña?</a>
	<form id="conf_form" name="conf_form" action="requestPsw.aspx" method="post" enctype="application/x-www-form-urlencoded" 
	    style="DISPLAY:none">
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
<%	}	%>
<hr/>
<%--	vvvv	HOME	vvvv	--%>
<div class="item_fmenu">
	<a href="~/index.aspx" id="lnkInicio" runat="server" >inicio</a>
</div>
<%-- --%>
<% if(Session["userName"]!=null && Session["errorLog"] == null ){%>
<div id="usrlogin" class="item_fmenu">
	<span title="Usuario">
		<%=Session["userName"]%>
	</span>- <a href="index.aspx?Salir=Yes" id="lnkSalir" runat="server" title="Salir del servicio">salir</a>
</div>
<hr>
<%} else {%>
<div class="item_fmenu">
	<form id="form_menu" enctype="application/x-www-form-urlencoded" xml:lang="es-ES" method="post"
		name="topmenu" action="access_user.aspx" onsubmit="javascript:return ControlsVal();" style="DISPLAY:inline">
		<%--<label for="inpUser">Usuario:</label>--%>
		<%--<span id="logInvalid" style="COLOR:#cc0000;DISPLAY:none" ></span>--%>
        <asp:Label ID="lblInpUser" runat="server" for="inpUser" Text="Usuario:"></asp:Label>
		<input id="inpUser" name="inpUser" type="text" tabindex="1" size="10">&nbsp; 
		<%--<label for="inpPswd">Contraseña:</label>--%> 
        <asp:Label ID="lblInpPswd" runat="server" for="inpPswd" Text="Contraseña:"></asp:Label>
		<input id="inpPswd" name="inpPswd" type="password" tabindex="2" size="10">&nbsp; 
		<button type="submit" id="btnSubmitAccess" runat="server">Acceder</button>
	</form>
</div>
<hr>
<%}%>
<%--	vvvv	LANGUAGE	vvvv	--%>
<div style="DISPLAY:inline">
	<form id="formlang" name="formlang" method="get" enctype="application/x-www-form-urlencoded" style="DISPLAY:inline">
		<select id="lang" name="lang" tabindex="3" onchange="javascript:document.forms['formlang'].submit()"
			runat="server" title="Seleccione Idioma">
			<%--<option value="" id="optLang">Elija un idioma:</option>--%>
			<%--<option value="eu">Euskera</option>--%>
			<option value="es-ES" selected="selected">Castellano</option>
			<%--<option value="ca">Català</option>--%>
			<%--<option value="en-US">English</option>--%>
		</select>
		<noscript>
			<button id="langBtn" type="submit">»</button>
		</noscript>
	</form>
</div>
<hr>
<%--	vvvv	MENU	VVVV	--%>
<div id="menu_pral" style="MARGIN-TOP:10px;TEXT-ALIGN:left">
	<% if (Session["userName"] != null ) { %>
	<ul>
		<li>
			<a href="fact_oper.aspx" id="lnkFacturation" runat="server" accesskey="1">Facturación</a>
		</li>
		<li>
			<a href="SuscriptionForm.aspx" id="lnkModifyData" runat="server" accesskey="2">Modificar datos</a>
		</li>
		<li>
		    <a href="SetFunds.aspx" id ="lnkSetFunds" runat="server" accesskey="3">Recarga</a>
		</li>
		<li>
		    <a href="GetFunds.aspx" id ="lnkGetFunds" runat="server" accesskey="4">Saldo</a>
		</li>
		<li>
			&nbsp;<a href="#" id="lnkDeleteUser" runat="server" onclick="javascript:DeleteUser()" accesskey="5">Darse de baja</a>
		</li>
		&nbsp;<li>
            <a href="SuscriptionForm.aspx" id="lnkAltaOnline" runat="server" accesskey="2">Alta online</a>
	</ul>
	<% } %>
</div>
<%--	VVVV	END		VVVV	--%>