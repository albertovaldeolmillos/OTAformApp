<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="configuration.aspx.cs" Inherits="OTAformApp.configuration" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Configuración - <%= ConfigurationSettings.AppSettings["titPages"] %></title>
    <script language="javascript" type="text/javascript" src="formVal.js"></script>
    <link href="OTA_base.css" rel="stylesheet" type="text/css" media="screen" /> 
    <link href="formOTA.css" rel="stylesheet" type="text/css" media="screen" /> 
    <style type="text/css" media="screen">	        
			.tableItem {font: x-small Verdana, Arial, sans-serif; background-color:#F6F9F9; white-space: normal; word-wrap: break-word}
			.tableHeader {font: bold small Arial; color:#663300; background-color:#CCCC66;}
			
			.alternatingItem {font: x-small Verdana, Arial, sans-serif; background-color:#FFFFCC;}	
			.pageLinks {text-align: center;}  
	    </style>      
</head>
<body MS_POSITIONING="GridLayout">
    <div id="frame">        
	    <div id="layoutform">
		    <%--<div id="top_menu">
			    <OTAform:header runat="server" id="ctrlHead"/>
		    </div>--%>
	        <hr />
		    <h1>
                <asp:Localize ID="locTitle" runat="server" Text="Servicio de pago remoto del estacionamiento"></asp:Localize>
            </h1>
		    <h2>
                <asp:Localize ID="locSubtitle" runat="server" Text="Configuración"></asp:Localize>
            </h2>
		    <a name="errorMsg" id="errorMsg" href="#errorMsg"></a>
		    <form id="form1" onsubmit="javascript:return configVal();" action="" enctype="application/x-www-form-urlencoded" method="post" target="_self" runat="server">
                <div>                                    
                    <table>
                        <tr>                            
                            <td style="text-align:left;width:30%; vertical-align:text-top;">
                                <asp:LinkButton ID="lkbtnSuscripcion" runat="server" Text="Suscripción" 
                                CommandName="Suscription" CommandArgument="1" OnCommand="LinkButton_Command" 
                                OnClientClick="javascript:clickLink = 'true';"></asp:LinkButton> 
                                <br />
                                <br />
                                <asp:LinkButton ID="lkbtnEmail" runat="server" Text="Automatización de Email"
                                CommandName="Email" CommandArgument="3" OnCommand="LinkButton_Command" 
                                OnClientClick="javascript:clickLink = 'true';"></asp:LinkButton>
                                <br />
                                <br />
                                <asp:LinkButton ID="lkbtnEstadoEmail" runat="server" Text="Estado de Envío" 
                                CommandName="EmailStatus" CommandArgument="5" OnCommand="LinkButton_Command" 
                                 OnClientClick="javascript:clickLink = 'true';">
                                </asp:LinkButton>
                                <br />
                                <br />                                
                                <asp:LinkButton ID="lkbtnAcceso" runat="server" Text="Alta de Usuario" 
                                CommandName="Access" CommandArgument="2" OnCommand="LinkButton_Command" 
                                 OnClientClick="javascript:clickLink = 'true';">
                                </asp:LinkButton>
                                <br />
                                <br />
                                 <asp:LinkButton ID="lkbtnModifyAcceso" runat="server" Text="Modificar Usuario"
                                CommandName="AccessModify" CommandArgument="6" OnCommand="LinkButton_Command" 
                                OnClientClick="javascript:clickLink = 'true';"></asp:LinkButton>
                                <br />
                                <br />
                                <asp:LinkButton ID="lkbtnSalir" runat="server" Text="Salir"
                                CommandName="Exit" CommandArgument="7" OnCommand="LinkButton_Command" 
                                OnClientClick="javascript:clickLink = 'true';"></asp:LinkButton>       
                            </td>
                            <td style="text-align:left;width:70%">  
                                                                                    
                                <div id="formError" runat="server">
                                    <p>
                                        <asp:Localize ID="locAtencion" Text="ATENCIÓN!, compruebe los siguientes datos:" runat="server"></asp:Localize>
                                    </p>
						            <ul id="ulerrors" runat="server"></ul>
						            <p align="right">
                                        <asp:Localize ID="locGracias" Text="Gracias." runat="server"></asp:Localize>
                                    </p>
                                </div>      
                                                        
                                <div id="divSuccess" runat="server" style="background-color:White;padding:20px 20px 20px 20px">
                                    <asp:Label ID="lblSuccess" runat="server" Text=""></asp:Label>
                                </div>  
                                
                                <div id="divSuscription" style="background-color:White;padding:20px 20px 20px 20px" runat="server">
                                    <h6 style="color:#663300">
                                        <asp:Localize ID="Localize2" runat="server" Text="Suscripción"></asp:Localize>
                                    </h6>
                                    
                                    <table width="100%">
                                        <caption>Datos Personales</caption>                                    
                                    <tr>
                                        <td><asp:Label ID="lblNameS" Font-Size="X-Small" runat="server" Text="Nombre"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:TextBox ID="txtNameS" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblSurname1S" Font-Size="X-Small" runat="server" Text="Primer Apellido"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:TextBox ID="txtSurname1S" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblSurname2S" Font-Size="X-Small" runat="server" Text="Segundo Apellido"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:TextBox ID="txtSurname2S" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblNIF" Font-Size="X-Small" runat="server" Text="NIF o CIF"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:TextBox ID="txtNIF" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblMobile" Font-Size="X-Small" runat="server" Text="Teléfono Móvil"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td><asp:TextBox ID="txtMobile" runat="server"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td><asp:Label ID="lblMobileCompany" Font-Size="X-Small" runat="server" Text="Compañía de Móvil"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlMobileCompany" runat="server" AutoPostBack="false">
                                                <asp:ListItem Selected="True" Value="0" Text="Seleccione Compañía…" ></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>                                     
                                    </tr>    
                                    </table>                               
                                    
                                    <table width="100%">
                                        <caption>Razón Social</caption>
                                        <tr>
                                            <td><asp:Label ID="lblStreet" Font-Size="X-Small" runat="server" Text="Calle"></asp:Label></td>
                                            <td><asp:Label ID="lblNum" Font-Size="X-Small" runat="server" Text="Número Calle"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:TextBox ID="txtStreet" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtStreetNum" runat="server"></asp:TextBox></td>
                                        </tr>
                                        
                                        <tr>
                                            <td><asp:Label ID="lblPiso" Font-Size="X-Small" runat="server" Text="Piso"></asp:Label></td>
                                            <td><asp:Label ID="lblPuerta" Font-Size="X-Small" runat="server" Text="Puerta"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:TextBox ID="txtPiso" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtPuerta" runat="server"></asp:TextBox></td>
                                        </tr>
                                        
                                        <tr>
                                            <td><asp:Label ID="lblEscalera" Font-Size="X-Small" runat="server" Text="Escalera"></asp:Label></td>
                                            <td><asp:Label ID="lblLetra" Font-Size="X-Small" runat="server" Text="Letra"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:TextBox ID="txtEscalera" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtLetra" runat="server"></asp:TextBox></td>
                                        </tr>
                                        
                                        <tr>
                                            <td><asp:Label ID="lblCP" Font-Size="X-Small" runat="server" Text="Código Postal"></asp:Label></td>
                                            <td><asp:Label ID="lblLocalidad" Font-Size="X-Small" runat="server" Text="Localidad"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td><asp:TextBox ID="txtCP" runat="server"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtLocalidad" runat="server"></asp:TextBox></td>
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblProvincia" Font-Size="X-Small" runat="server" Text="Provincia"></asp:Label>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                             <td colspan="2">
                                                <asp:TextBox ID="txtProvincia" runat="server"></asp:TextBox>
                                            </td>                                            
                                        </tr>
                                        <tr style="visibility:hidden">
							                <td>								
                                                <asp:Label ID="lblCountry" Font-Size="X-Small" runat="server" Text="País:"></asp:Label>
							                </td>
							                <td>
							                    <asp:TextBox ID="txtCountry" runat="server" value="ESPANA" Text="ESPANA" ></asp:TextBox>							                    
							                </td>
						                </tr>
                                    </table>
                                    
                                    <table width="100%">
                                        <caption>Datos de la Tarjeta</caption>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblTipoTarjeta" Font-Size="X-Small" runat="server" Text="Tipo Tarjeta"></asp:Label>
                                            </td>                                            
                                        </tr>                                        
                                        <tr>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlTipoTarjeta" runat="server" AutoPostBack="false">
                                                    <asp:ListItem Selected="True" Value="0" Text="Seleccione tipo…" ></asp:ListItem>
                                                    <asp:ListItem Value="VISA" Text="VISA" ></asp:ListItem>
                                                    <asp:ListItem Value="MASTER" Text="MASTERCARD" ></asp:ListItem>
                                                    <asp:ListItem Value="4B" Text="4B" ></asp:ListItem>      
                                                </asp:DropDownList> 
                                            </td>                                            
                                        </tr>
                                        
                                        <tr>
                                            <td colspan="2">
                                                <asp:Label ID="lblNumTarjeta" Font-Size="X-Small" runat="server" Text="Número Tarjeta"></asp:Label>
                                            </td>                                            
                                        </tr>                                        
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="txtNumTarjeta" runat="server"></asp:TextBox> 
                                            </td>                                            
                                        </tr>     
                                        <tr>
                                            <td><asp:Label ID="lblMesCad" Font-Size="X-Small" runat="server" Text="Mes Caducidad"></asp:Label></td>
                                            <td><asp:Label ID="lblAnyoCad" Font-Size="X-Small" runat="server" Text="Año Caducidad"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlMesCad" runat="server" AutoPostBack="false">
                                                    <asp:ListItem Selected="True" Value="0" Text="Mes…" ></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="01" ></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="02" ></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="03" ></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="04" ></asp:ListItem>
                                                    <asp:ListItem Value="5" Text="05" ></asp:ListItem>
                                                    <asp:ListItem Value="6" Text="06" ></asp:ListItem>
                                                    <asp:ListItem Value="7" Text="07" ></asp:ListItem>
                                                    <asp:ListItem Value="8" Text="08" ></asp:ListItem>
                                                    <asp:ListItem Value="9" Text="09" ></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10" ></asp:ListItem>
                                                    <asp:ListItem Value="11" Text="11" ></asp:ListItem>
                                                    <asp:ListItem Value="12" Text="12" ></asp:ListItem>
                                                </asp:DropDownList>
                                            </td> 
                                            <td>
                                                <asp:DropDownList ID="ddlAnyoCad" runat="server" AutoPostBack="false">
                                                    <asp:ListItem Selected="True" Value="0" Text="Año…" ></asp:ListItem>
                                                </asp:DropDownList>
                                            </td> 
                                        </tr>
                                    </table>
                                    
                                    <table width="100%">
                                        <caption>Datos de Suscripción</caption>
                                        <tr>
                                            <td><asp:Label ID="lblMatriculas" Font-Size="X-Small" runat="server" Text="Matrículas Asociadas" ></asp:Label></td>                                                                                        
                                        </tr>
                                        <tr>
                                             <td><asp:TextBox ID="txtMatriculas" runat="server" TextMode="MultiLine" Columns="20" Rows="2" OnChange="javascript:this.value = checkPlates(this.value);"></asp:TextBox></td>                                            
                                        </tr>
                                        <tr>
                                            <td><asp:Label ID="lblUsuario" Font-Size="X-Small" runat="server" Text="Usuario"></asp:Label></td>                                            
                                        </tr>
                                        <tr>
                                             <td><asp:TextBox ID="txtUsuario" runat="server" ></asp:TextBox></td>                                            
                                        </tr>
                                        <tr style="visibility:collapse;">
                                            <td><asp:Label ID="lblPsw" Font-Size="X-Small" runat="server" Text="Contraseña" ></asp:Label></td>                                            
                                        </tr>
                                        <tr style="visibility:collapse;">
                                             <td><asp:TextBox ID="txtPsw" TextMode="Password" runat="server"  Enabled="false"></asp:TextBox></td>                                            
                                        </tr>
                                        <tr style="visibility:collapse;">
                                            <td><asp:Label ID="lblPswRep" Font-Size="X-Small" runat="server" Text="Repetir Contraseña" ></asp:Label></td>                                            
                                        </tr>
                                        <tr style="visibility:collapse;">
                                             <td><asp:TextBox ID="txtPswRep" TextMode="Password" runat="server" Enabled="false"></asp:TextBox></td>                                            
                                        </tr>
                                        <tr>
                                            <td><asp:Label ID="lblEmail" Font-Size="X-Small" runat="server" Text="Correo Electrónico"></asp:Label></td>                                            
                                        </tr>
                                        <tr>
                                             <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>                                            
                                        </tr>
                                    </table>
                                    
                                    <br />    
                                    <br />                                    
                                    <p style="text-align:center">
                                        <asp:Button ID="Button2" runat="server" Text="Guardar" Width="80px" 
                                                    CommandName="SaveSuscription" CommandArgument="7" OnCommand="Button_Command"/>&nbsp;
                                        <%--<asp:Button ID="btnDeleteUser" runat="server" Text="Borrar" Width="80px" 
                                                    CommandName="DeleteUser" CommandArgument="4" OnCommand="Button_Command"/>--%>
                                        <input id="reset2" type="reset" value="Borrar" title="delete" style="width:80px" />
                                    </p>
                                </div>
                                                         
                                <div id="divEnvioEmail" runat="server" style="background-color:White;padding:20px 20px 20px 20px">
                                    <h6 style="color:#663300">
                                        <asp:Localize ID="locTitleEdit" runat="server" Text="Automatización de Email"></asp:Localize>
                                    </h6>                                    
                                    <asp:Label ID="lblNameUser" Font-Size="X-Small" runat="server" Text="Nombre"></asp:Label>                                    
                                    <br />
                                    <asp:DropDownList ID="ddlUsuarios" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlUsuarios_IndexChanged" onchange="javascript:clickLink = 'true';" >
                                     </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblSenderDay" Font-Size="X-Small" runat="server" Text="Día de Envío"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlDiaEnvio" runat="server">
                                        <asp:ListItem Selected="True" Value="0" Text=""></asp:ListItem>
                                        <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>                                        
                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>                                        
                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblSenderHour" Font-Size="X-Small" runat="server" Text="Hora de Envío (hh:mm:ss)"></asp:Label>
                                    <br />
                                    <%--<asp:DropDownList ID="ddlHoraEnvio" runat="server"></asp:DropDownList>--%>
                                    <asp:TextBox ID="txtHoraEnvio" runat="server" Width="100px"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblSenderPeriod" Font-Size="X-Small" runat="server" Text="Periodo de Envío (meses)"></asp:Label>
                                    <br />
                                    <%--<asp:TextBox ID="txtSenderPeriod" runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlSenderPeriod" runat="server">
                                        <asp:ListItem Selected="True" Value="0" Text=""></asp:ListItem>
                                        <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>                                        
                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblSenderData" Font-Size="X-Small" runat="server" Text="Periodo de Datos (meses)"></asp:Label>
                                    <br />
                                    <%--<asp:TextBox ID="txtSenderData" runat="server"></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlSenderData" runat="server">
                                        <asp:ListItem Selected="True" Value="0" Text=""></asp:ListItem>
                                        <asp:ListItem Value="1" Text="01"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="02"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="03"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="04"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="05"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="06"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="07"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="08"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="09"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>                                        
                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                    </asp:DropDownList>                              
                                    <br />
                                    <asp:Label ID="lblLanguage" Font-Size="X-Small" runat="server" Text="Idioma"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlIdioma" runat="server">
                                    <asp:ListItem Selected="True" Value="0" Text=""></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Castellano"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Catalán"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Euskera"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="Inglés"></asp:ListItem>                                        
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblSenderActivation" Font-Size="X-Small" runat="server" Text="Activar Envío"></asp:Label>
                                    <br />
                                    <asp:CheckBox ID="chkSenderActivation" CssClass="CheckBox" runat="server" />
                                    <br />
                                    <asp:Label ID="lblSubject" Font-Size="X-Small" runat="server" Text="Asunto del Mensaje"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtSubject" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblBody" Font-Size="X-Small" runat="server" Text="Mensaje"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtBody" runat="server" BackColor="#FAFAFA" TextMode="MultiLine" Height="150px" Width="90%" 
                                                 BorderColor="#BDBDBD" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox> 
                                    <br />                                    
                                    <p style="text-align:center">
                                        <asp:Button ID="btnSaveEmail" runat="server" Text="Guardar" Width="80px" 
                                                    CommandName="SaveEmail" CommandArgument="1" OnCommand="Button_Command"/>&nbsp;
                                        <%--<asp:Button ID="btnDeleteEmail" runat="server" Text="Borrar" Width="80px" 
                                                    CommandName="DeleteEmail" CommandArgument="2" OnCommand="Button_Command"/>--%>
                                        <input id="resetSaveEmail" type="reset" value="Borrar" style="width:80px" />
                                    </p>                                                                     
                                </div>
                                
                                <div id="divAltaUsuario" style="background-color:White;padding:20px 20px 20px 20px" runat="server">
                                    <h6 style="color:#663300">
                                        <asp:Localize ID="Localize1" runat="server" Text="Alta de Usuario"></asp:Localize>
                                    </h6>
                                    <asp:Label ID="lblNameOfficer" Font-Size="X-Small" runat="server" Text="Nombre"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblSurname1" Font-Size="X-Small" runat="server" Text="Primer Apellido"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtSurname1" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblSurname2" Font-Size="X-Small" runat="server" Text="Segundo Apellido"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtSurname2" runat="server"></asp:TextBox>
                                    <br />
                                    <br />
                                    <asp:Label ID="lblLogin" Font-Size="X-Small" runat="server" Text="Usuario"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtLogin" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblPassword" Font-Size="X-Small" runat="server" Text="Contraseña"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                                    <br />                                    
                                    <p style="text-align:center">
                                        <asp:Button ID="btnSaveUser" runat="server" Text="Guardar" Width="80px" 
                                                    CommandName="SaveUser" CommandArgument="3" OnCommand="Button_Command"/>&nbsp;
                                        <%--<asp:Button ID="btnDeleteUser" runat="server" Text="Borrar" Width="80px" 
                                                    CommandName="DeleteUser" CommandArgument="4" OnCommand="Button_Command"/>--%>
                                        <input id="resetNewUser" type="reset" value="Borrar" title="delete" style="width:80px" />
                                    </p>
                                </div>
                                
                                <div id="divEstadoEmail" style="background-color:White;padding:20px 20px 20px 20px" runat="server">
                                    <h6 style="color:#663300">
                                        <asp:Localize ID="locEstadoEmail" runat="server" Text="Estado de Envío"></asp:Localize>
                                    </h6>
                                    <asp:Label ID="lblUserStatusEmail" Font-Size="X-Small" runat="server" Text="Nombre Usuario"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlUserStatusEmail" runat="server">                                        
                                    </asp:DropDownList>
                                    <br />                                     
                                   
                                    <asp:Label ID="lblFechaIni" Font-Size="X-Small" runat="server" Text="Fecha Inicio"></asp:Label>  
                                    <br />
                                    <asp:TextBox ID="txtFechaIni" Width="100px" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblFechaFin" Font-Size="X-Small" runat="server" Text="Fecha Fin"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtFechaFin" Width="100px" runat="server"></asp:TextBox> 
                                                                                 
                                    <p style="text-align:center">
                                        <asp:Button ID="btnFilter" runat="server" Text="Filtrar" Width="80px" 
                                                CommandName="StatusEmailFilter" CommandArgument="5" OnCommand="Button_Command" />&nbsp;
                                        <input id="resetStatusEmail" type="reset" value="Borrar" title="delete" style="width:80px" />                                        
                                    </p>
                                    <br />
                                    <asp:GridView ID="gvEmailStatus" runat="server" Width="100%" CellPadding="4" CaptionAlign="Left"                                    
						                Gridlines="Both" HorizontalAlign="Left"						            
						                AllowPaging="False" >
                                        <FooterStyle BackColor="#F6F9F9" />
                                        <RowStyle BorderColor="#DEDFDE" Wrap="true" BorderWidth="1px" BorderStyle="Solid" BackColor="#F6F9F9" CssClass="tableItem"></RowStyle>
                                        <AlternatingRowStyle BackColor="White" /> 
                                        <PagerStyle HorizontalAlign="Right" BackColor="#B5D9BD"  />   
                                        <PagerSettings Mode="Numeric" Visible="true" />                                 
                                        <%--<SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />--%>
                                        <HeaderStyle BackColor="#B5D9BD" CssClass="tableItem" />                                           
                                    </asp:GridView> 
                                    
                                    <p style="text-align:center; color:#F20B12; font-size:x-small">
                                        <asp:Localize ID="locEmailStatusMessage" runat="server" 
                                            Text="No hay datos para mostrar" Visible="False"></asp:Localize>
                                    </p>
                                </div>
                                
                                <div id="divModificarUsuario" style="background-color:White;padding:20px 20px 20px 20px" runat="server">
                                    <h6 style="color:#663300">
                                        <asp:Localize ID="locModifyUser" runat="server" Text="Modificar Datos de Usuario"></asp:Localize>
                                    </h6>
                                    <asp:Label ID="lblModifySelectName" Font-Size="X-Small" runat="server" Text="Seleccionar Usuario"></asp:Label>
                                    <br />
                                    <asp:DropDownList ID="ddlModifySelectName" runat="server" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlModifySelectName_IndexChanged" onchange="javascript:clickLink = 'true';" >
                                     </asp:DropDownList>
                                     <br />
                                     <br />
                                     <asp:Label ID="lblModifyName" Font-Size="X-Small" runat="server" Text="Nombre"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtModifyName" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblModifySurname1" Font-Size="X-Small" runat="server" Text="Primer Apellido"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtModifySurname1" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblModifySurname2" Font-Size="X-Small" runat="server" Text="Segundo Apellido"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtModifySurname2" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblModifyActiveUser" Font-Size="X-Small" runat="server" Text="Activar Usuario"></asp:Label>
                                    <br />
                                    <asp:CheckBox ID="chkModifyActiveUser" CssClass="CheckBox" runat="server" />                                    
                                    <br />                                    
                                    <asp:Label ID="lblModifyUser" Font-Size="X-Small" runat="server" Text="Usuario"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtModifyUser" runat="server"></asp:TextBox>
                                    <br />
                                    <asp:Label ID="lblModifyPassword" Font-Size="X-Small" runat="server" Text="Contraseña"></asp:Label>
                                    <br />
                                    <asp:TextBox ID="txtModifyPassword" runat="server"></asp:TextBox>
                                    <br />                                    
                                    <p style="text-align:center">
                                        <asp:Button ID="Button1" runat="server" Text="Guardar" Width="80px" 
                                                    CommandName="ModifyUser" CommandArgument="6" OnCommand="Button_Command"/>&nbsp;                                        
                                        <input id="reset1" type="reset" value="Borrar" style="width:80px" />
                                    </p>
                                </div>
                            </td>                                                    
                        </tr>
                        
                                                                    
                    </table>    
                </div>
                <br />
                <%--<div id="formButtons" style="TEXT-ALIGN:center">
				    <button id="submit" name="submit" type="submit" value="submit">Entrar</button>					
				</div>--%>
            </form>
	    </div>
	    <hr />
			<div id="feet">
					<!-- #Include File="OTAfooter.inc" -->
			</div>
	</div>
</body>
</html>
