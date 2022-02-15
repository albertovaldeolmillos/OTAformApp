<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponseOK.aspx.cs" Inherits="OTAformApp.ResponseOK" %>
<%@ Register TagPrefix="OTAform" TagName="header" Src="ctrlHead.ascx"  %>
<script type="text/javascript">
    function dispatchResponseEvent() {

        parent.postMessage("OK", '*');

    }
    </script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Facturación - <%= ConfigurationSettings.AppSettings["titPages"] %></title>
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
  <body MS_POSITIONING="GridLayout" onload="dispatchResponseEvent()">
	
		<div id="frame">
			<div id="layoutform">
			    <hr />
				<h2 id="tsubtitle">Recarga</h2>	
				
				    <p>La recarga se ha realizado correctamente.</p>					
                    <br />
                    <br />           
		<hr />
		</div>
	
  </body>
</html>
