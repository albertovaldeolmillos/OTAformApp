// JavaScript Document
    
	regexName = new RegExp("^[A-Za-z|\\040|\\u00C4-\\u00FC|\\u00C2-\\u00D9|.]{2,50}$","i");
	regexStreetNumAndLvl = new RegExp("^\\d{0,6}$","i");
	regexCP = new RegExp("^\\d{5,10}$","i");

	//old	regexEmail = new RegExp("^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$", "i");
	regexEmail = new RegExp("^[a-z0-9!$'*+\-_]+(\.[a-z0-9!$'*+\-_]+)*@([a-z0-9]+(-+[a-z0-9]+)*\.)+([a-z]{2}|aero|arpa|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|travel)$", "i");
    regexCard = new RegExp("^((4\\d{3})|(5[1-5]\\d{2}))(-?|\\040?)(\\d{4}(-?|\\040?)){3}|^(3[4,7]\\d{2})(-?|\\040?)\\d{6}(-?|\\040?)\\d{5}","i");
    regexPsw = new RegExp("(?=^.{6,51}$)([A-Za-z]{1})([A-Za-z0-9!@#$%_\^\&amp;\*\-\.\?]{5,49})$", "i");


    //regexNumStreet = new RegExp("^\\w{0,10}$");
    regexNumStreet = new RegExp("(^[A-Za-z0-9/]{1,10}$)");
    //Piso
    //regexLevel = new RegExp("^\\w{0,10}$");
    regexLevel = new RegExp("(^[A-Za-z0-9/|\\u00BA|\\u00AA]{0,10}$)");
	//Puerta
    //regexDoor = new RegExp("^\\w{0,10}$");
    regexDoor = new RegExp("(^[A-Za-z0-9/|\\u00BA|\\u00AA]{0,10}$)");
	
    //OLD	regexDNI = new RegExp("^[ABCDEFGHKLMNPQS][\\d]{2}[\\d]{5}[0-9A-J]$|^[E]?\\d{8,10}[A-Z]?$");
	regexCIF = new RegExp("^[ABCDEFGHKLMNPQS][\\d]{2}[\\d]{5}[0-9A-J]$");
	regexDNI = new RegExp("^[E]?\\d{8,9}[A-Z]?$");
	regexNIE = new RegExp("^[XxTtYyZz]{1}[0-9]{7,9}[a-zA-Z]{1}$");
	// Telephone
	regexTel = new RegExp("^(\\(\\+?\\d{2,3}\\))?[\\d|\\040|-]{6,20}$");
	regexPlate = new RegExp("^[\\w|\\040|-]{6,20}$","gmi");
	regexPlate.multiline = true;
	// Login User = Characters (A-Z 0-9) (min 4, max10) en BBDD hay un maximo de 10 caracteres;
	regexLogin = new RegExp("^[A-Za-z0-9]{4,10}$");
	// PIN Number = number  (min 4, max10);
	//Montse -> 6 caracteres alfanumericos
	//regexPINNum = new RegExp("^\\d{4,10}$");
	regexPINNum = new RegExp("^[A-Za-z0-9]{6,20}$");
	//Contraseña para punto de acceso
	regexPINNumConfig = new RegExp("^[A-Za-z0-9]{5,20}$");
    //Variable a la que se da valor en el Code-Behind
	var culture;
	//Variable para pagina de configuración de datos
	var clickLink;
	//Validación formato hora
	//regexTime = new RegExp("^[0-2][0-3]\\072[0,5]\\d\\072[0,5]\\d$");
	//regexTime = new RegExp("([0-1]?[0-9])|(2?[0-3]):([0-5]?[0-9]):([0-5]?[0-9])");
	
	//regexTime = new RegExp("^(0[1-9]|1\d|2[0-3]):([0-5]\d):([0-5]\d)$");
	regexTime = new RegExp("^(0?[1-9]|(1\\d)|(2?[0-3])):([0-5]\\d):([0-5]\\d)$");
	
	//Validacion Formato fecha corta
	regexDate = new RegExp("^[0-3]\\d\\057[0-1]\\d\\057\\d{4}$");



//función que valida fecha	
// En el objeto text hacemos lo Siguiente
/*
   <input type=\'text\' name=cajaFecha onChange=\'fechas(this.value); this.value=borrar\'>
*/
function IsValidDate(caja)
{
    var isValid = true;
    
   if (caja)
   {  
      borrar = caja;
      if ((caja.substr(2,1) == '/') && (caja.substr(5,1) == '/'))
      {      
         for (i=0; i<10; i++)
	     {	
            if (((caja.substr(i,1) < '0') || (caja.substr(i,1)> '9')) && (i != 2) && (i != 5))
			{
			    //borrar = '';
			    isValid = false;
               break;  
			}  
         }
	     if (borrar)
	     { 
	        a = caja.substr(6,4);
		    m = caja.substr(3,2);
		    d = caja.substr(0,2);
		    if((a < 1900) || (a > 2050) || (m < 1) || (m > 12) || (d < 1) || (d > 31))
		       //borrar = '';
		        isValid = false;
		    else
		    {
		       if((a%4 != 0) && (m == 2) && (d > 28))	   
		          //borrar = ''; // Año no bisiesto y es febrero y el dia es mayor a 28
		           isValid = false;
			   else				   
		          if ((((m == 4) || (m == 6) || (m == 9) || (m==11)) && (d>30)) || ((m==2) && (d>29)))
			         //borrar = '';
		              isValid = false; 
		    } 
         } 
      } 			    			
	  else
	      //borrar = '';
	      isValid = false;    } 

    return isValid;
} 
	



function GetCookie() {
    var nombre = 'OTAformLang';
    a = document.cookie.substring(document.cookie.indexOf(nombre + '=') + nombre.length + 1, document.cookie.length);
    if (a.indexOf(';') != -1) a = a.substring(0, a.indexOf(';'))
    return a;
}


//Seleccion de idioma
function GetLanguage() {
    var language;
    var culture = GetCookie();
    
    switch (culture) {
        case 'ca':
            language = Catalan;
            break;
        default:
            language = Castellano;
    }

    return language;
}
	
	
	
function formVal(){
	divErr = document.getElementById('formError');
	ulErr = document.getElementById('ulerrors');
	formObj = document.getElementById('form1');

	var sendForm = true;	
	ulErr.innerHTML = '';
	//formObj.action = 'validation.aspx';
	//return (sendForm);

	//Seleccion de idioma	
	var Language = GetLanguage();	
	
	//	Name & Surname
	var name = document.getElementById('name').value;
	var firstSurname = document.getElementById('surname1').value;
	var SecondSurname = document.getElementById('surname2').value;
	var SecSurnameParse = true;
	
	if(	SecondSurname != '' && !regexName.test(SecondSurname)){
		SecSurnameParse = false;
	}
	
	if( name == '' || firstSurname == '' || !(regexName.test(name) && regexName.test(firstSurname)) || !SecSurnameParse){
		sendForm = false;
		var newLInode = document.createElement("li");
		//newLInode.innerHTML = '<a href="#name">Nombre y Apellidos no son correctos</a>';
		//newLInode.innerHTML = Castellano.NameSurname;
		newLInode.innerHTML = Language.NameSurname;
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	// NIF CIF
	var numNIF = document.getElementById('DNINIF').value.toUpperCase();
	var isCIF = false;
	var isNIE = false;
	
	if( regexCIF.test(numNIF)){
	    if (ValidaCIF(numNIF)) isCIF = true;
	}

	if (regexNIE.test(numNIF))
	    isNIE = true;
	    
	//alert(isCIF +'_-_'+ regexDNI.test(numNIF) );
	if (!isCIF && !(regexDNI.test(numNIF)) && !isNIE) {
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#DNINIF">El NIF, NIE &oacute; CIF no es correcto</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	/* OLD
	if(!(regexDNI.test(numNIF))){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = "El NIF ó CIF no es correcto";
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	*/


	//	TelephoneCompany
	/*
	var telefCompany = document.getElementById('telCom').value;
	if (telefCompany == '0') {
	    sendForm = false;
	    var newLInode = document.createElement("li");
	    newLInode.innerHTML = '<a href="#telCom">Elija una compa&ntilde;&iacute;a de tel&eacute;fono</a>';
	    ulErr.appendChild(newLInode);
	    divErr.style.display = 'block';
	}
	*/

	// Telephone1
	var telf = document.getElementById('telephone').value;
	if(telf == '' || !( regexTel.test(telf) ) ){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#telephone">El tel&eacute;fono 1 no es v&aacute;lido</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}

	// Telephone2
	var telf2 = document.getElementById('telephone2').value;
	if (telf2 != '' && !(regexTel.test(telf2))) {
	    sendForm = false;
	    var newLInode = document.createElement("li");
	    newLInode.innerHTML = '<a href="#telephone2">El tel&eacute;fono 2 no es v&aacute;lido</a>';
	    ulErr.appendChild(newLInode);
	    divErr.style.display = 'block';
	}
	
	// address
	var address = document.getElementById('address').value;
	if(address == ''){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#address">La direcci&oacute;n no es v&aacute;lida</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	
	var addressNum = document.getElementById('addressNum').value;
	if (addressNum != '' && !(regexNumStreet.test(addressNum))) {
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#addressNum">El n&uacute;mero de calle no es v&aacute;lido<br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9 y /, m&iacute;n. 1 - m&aacute;x. 10 )</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
    }
	
	
	var level = document.getElementById('level').value;
	if (level != '' && !(regexLevel.test(level))) {
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#level">El piso no es v&aacute;lido<br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, &ordf; y &ordm;, m&aacute;x. 10 )</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	//var door = document.getElementById('door').value;


	// Puerta
	var door = document.getElementById('door').value;
	if (door != '' && !(regexDoor.test(door))) {
	    sendForm = false;
	    var newLInode = document.createElement("li");
	    newLInode.innerHTML = '<a href="#door">La puerta no es v&aacute;lida<br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, &ordf; y &ordm;, m&aacute;x. 10 )</a>';
	    ulErr.appendChild(newLInode);
	    divErr.style.display = 'block';
	}


	var postCode = document.getElementById('PostCode').value;
	if(!(regexCP.test(postCode))){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#PostCode">El c&oacute;digo postal no es v&aacute;lido</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	// city
	var city = document.getElementById('city').value;
	if(!(regexName.test(city))){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#city">La localidad no es v&aacute;lida</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	// Province
	var province = document.getElementById('province').value;
	if(!(regexName.test(province))){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#province">La provincia no es v&aacute;lido</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	// country
	var country = document.getElementById('country').value;
	if(!(regexName.test(country))){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#country">El pa&iacute;s no es v&aacute;lido</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	
	//	CreditCard
	var cardType = document.getElementById('cardType').value;
	if(cardType == '0'){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#cardType">Elija un tipo de tarjeta de cr&eacute;dito</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	var cardNum = document.getElementById('cardNum').value;
	var chkCard = regexCard.test(cardNum) && checkCard(cardNum.toString());
	//Indica que el usuario está dentro de la sesion modificando sus datos.
	//(atributo añadido dinamicamente)
	//IExplorer y Firefox	
	//var cardNumSession = document.getElementById('cardNum').cardMask;
	var cardNumSession = document.getElementById('cardNum').getAttribute('cardMask');
    
    //El atributo 'cardMask' guarda la máscara de entrada de la tarjeta de crédito
    //obtenida de la BBDD en el momento de cargar la pagina.
    //Si el usuario ha modificado el número de tarjeta de crédito el atributo "session"
    //y el valor del control no coincidirán.
    if (cardNumSession == 'undefined' || cardNum != cardNumSession)
    {
	    if( !chkCard )
	    {	        
		    sendForm = false;
		    var newLInode = document.createElement("li");
		    newLInode.innerHTML = '<a href="#cardNum">El formato de tarjeta de cr&eacute;dito no es correcto.</a>';
		    ulErr.appendChild(newLInode);
		    divErr.style.display = 'block';
	    }
	} 
	
	
	var cardMonth = document.getElementById('cardMonthExpire').value;
	var cardYear = document.getElementById('cardYearExpire').value;

	if(cardMonth == '0' || cardYear == '0' || !checkCardDate(cardYear,cardMonth) ){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#cardMonthExpire">Caducidad de la tarjeta de cr&eacute;dito no es v&aacute;lida</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}
	
	// Plates
	var platesNum = document.getElementById('plates').value;
	if( platesNum == '' ){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#plates">La/s matr&iacute;cula/s no tienen un formato v&aacute;lido</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}

	
	// UserNAME
	var userName =  document.getElementById('user').value;
	
	if( userName == '' || !(regexLogin.test(userName))){
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#user">El nombre de usuario no es correcto <br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, m&iacute;n. 4 - m&aacute;x. 10 )</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}

	//Al login se le asigna el valor del NIF cuando se trate de una suscripcion mediante formulario impreso.
	if (userName == 'Automatico' && sendForm == true)
	    document.getElementById('user').value = numNIF;	
	
	
	// Password
	var password = document.getElementById('password').value;
	var passwordChk = document.getElementById('passwordChk').value;			
	
	if (password == '' || !(regexPINNum.test(password)) || password != passwordChk)
	{
		sendForm = false;
		var newLInode = document.createElement("li");
		newLInode.innerHTML = '<a href="#password">La contrase&ntilde;a no es correcta.<br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, m&iacute;n. 6 - m&aacute;x. 20 )</a>';
		ulErr.appendChild(newLInode);
		divErr.style.display = 'block';
	}	
	
	
	//	Email
	if (!regexEmail.test(document.getElementById('email').value)) {
	    sendForm = false;
	    var newLInode = document.createElement("li");
	    newLInode.innerHTML = '<a href="#email">El formato de correo electr&oacute;nico no es correcto.</a>';
	    ulErr.appendChild(newLInode);
	    divErr.style.display = 'block';
	}

	if (!sendForm) {
	    window.location = '#errorMsg';
	    alert('Compruebe los datos!');
	    formObj.action = '';
	}
	
	return (sendForm);	
}

function checkCard(val)
{
	// ATECION!!! Return para pruebas!!!
	//return true;

    var char1 = new RegExp('-','g');
	var char2 = new RegExp('\040','g');
	val = val.replace(char1,'');
	val = val.replace(char2,'');
   
   var lReturn=true;
   var n=0, sum = 0;
   var num_digits=0;
   var alternate = false;
   
   for (var i = (val.length)-1; i >= 0; i--) {

		n = parseInt(val.charAt(i));
		if ( n>=0 && n<=9 )
		{
			if (alternate)
			{
				n *= 2;
				if (n > 9){
				   n = (n % 10) + 1;
				}
			}
			sum += n;
			alternate = !alternate;
			num_digits++;
		}
   }

	if (((sum % 10) != 0)||(num_digits<13)||(num_digits>16))
	{
		lReturn=false;
		//alert("Error CheckCard!: " + val);
	}

   return (lReturn);
}



function checkCardDate(year,month){

	var validExpire = false;
	var now = new Date();
	var nowYear = now.getFullYear();
	var nowMonth = now.getMonth() + 1;
	
	
	if(year > nowYear){
		validExpire = true;		
	} else if (year == nowYear && parseInt(month) >= nowMonth){
		validExpire = true;		
	}
	return (validExpire);
}



function enableButtons(obj) {    
	if(obj.checked){
		document.getElementById('submit').disabled = false;
	} else {
		document.getElementById('submit').disabled = true;
    }    
}



function ValidaCIF(nCif){
	var valido = false;
	var v1 = new Array(0,2,4,6,8,1,3,5,7,9);
	var letras =new Array('J','A','B','C','D','E','F','G','H','I','J');
	var control = nCif.substr(nCif.length-1);
	var temp = 0;
	var valor= nCif.toUpperCase();
//	var Tipo, Provincia, Digito;
	for( i = 2; i <= 6; i += 2 )
	{
		temp = temp + v1[ parseInt(valor.substr(i-1,1)) ];
		temp = temp + parseInt(valor.substr(i,1));
	}
	temp = temp + v1[ parseInt(valor.substr(7,1)) ];
	temp = (10 - ( temp % 10));
	
	switch (valor.substr(0,1))
	{
		case 'K':
		case 'P':
		case 'Q':
		case 'S': 
			if(control == letras[temp]) valido =true;
			break;
		case 'A':
		case 'B':
		case 'E':
		case 'H': 
			if(control == temp) valido = true;
			break;
		default: 
			if(control == letras[temp] || control == temp) valido =true;
			break;
	}
	
	return (valido);
} // function ValidaCIF



function validNIF(numNIF){
	var letras = new Array('T','R','W','A','G','M','Y','F','P','D','X','B','N','J','Z','S','Q','V','H','L','C','K','E','T');

	//OLD	cadena="TRWAGMYFPDXBNJZSQVHLCKET"
	posicion = parseInt(numNIF) % 23;
	if(letras[posicion]== numNIF.substr(numNIF.length - 1)){
		alert('valid NIF');
	}
	//letra = cadena.substring(posicion,posicion+1);
	//document.formulario.dni.value=formulario.dni.value+" - "+letra
}



function checkPlates(str){
	regexPlate.multiline = true;
	var plates = new Array();
	var i=0;
	var strResult = "";
	while((plates[i] = regexPlate.exec(str.toUpperCase()))!=null ){
		
		//regexPlate.multiline = true;
		if(i >0) strResult += '\r';
		strResult += plates[i].toString().replace(/\040|-/gi,"");
		i++;
		
	}

	//alert(plates.length);
	return ( strResult );
}



function loadPassword() {
    var userPassword = document.getElementById('password').getAttribute('password');
    if (userPassword != 'undefined' || userPassword != null)
    {
        document.getElementById('password').value = userPassword;
        document.getElementById('passwordChk').value = userPassword;
    }
}


function loadPasswordCfg() {
    var userPassword = document.getElementById('txtPsw').getAttribute('password');
    if (userPassword != 'undefined' || userPassword != null) {
        document.getElementById('txtPsw').value = userPassword;
        document.getElementById('txtPswRep').value = userPassword;
    }
}



function accesVal() 
{
    var sendForm = true;    
    var divErr = document.getElementById('formError');
    var ulErr = document.getElementById('ulerrors');
    var formObj = document.getElementById('form1');    
    var accesLogin = document.getElementById('txtLogin').value;
    var accesPassword = document.getElementById('txtPassword').value;

    ulErr.innerHTML = '';

    if (accesLogin == '' || !(regexLogin.test(accesLogin))) {
        sendForm = false;
        var newLInode = document.createElement("li");
        newLInode.innerHTML = 'El usuario no es correcto';
        ulErr.appendChild(newLInode);
        divErr.style.display = 'block';
    }

    if (accesPassword == '' || !(regexPINNumConfig.test(accesPassword))) {
        sendForm = false;
        var newLInode = document.createElement("li");
        newLInode.innerHTML = 'La Contrase&ntilde;a no es correcta';
        ulErr.appendChild(newLInode);
        divErr.style.display = 'block';
    }
    
//    if (accesLogin == '' || accesPassword == '')
//    {
//        sendForm = false;
//        var newLInode = document.createElement("li");
//        newLInode.innerHTML = 'Nombre de Usuario y/o Contrase&ntilde;a.';
//        ulErr.appendChild(newLInode);
//        divErr.style.display = 'block';
//    }

    return (sendForm);
}



function configVal() {
    var sendForm = true;

    //Si el formulario se envía pulsando en el link no comprueba el estado
    //de los controles, puesto que en este caso estamos cambiando de formulario
    if (clickLink != 'true') {
        var divErr = document.getElementById('formError');
        var ulErr = document.getElementById('ulerrors');
        var formObj = document.getElementById('form1');

        //Comprobar qué DIV está activo
        var divEnvioEmail = document.getElementById('divEnvioEmail');
        var divAltaUsuario = document.getElementById('divAltaUsuario');
        var divModificarUsuario = document.getElementById('divModificarUsuario');
        var divEstadoEmail = document.getElementById('divEstadoEmail');
        var divSuscription = document.getElementById('divSuscription');

        ulErr.innerHTML = '';

        if (divEnvioEmail != null) {
            //Comprobar que no contiene controles vacíos
            var usuario = document.getElementById('ddlUsuarios').value;
            if (usuario == '0') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Nombre no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var diaEnvio = document.getElementById('ddlDiaEnvio').value; ;
            if (diaEnvio == '0') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El D&iacute;a de Env&iacute;o no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var horaEnvio = document.getElementById('txtHoraEnvio').value; ;
            //if (horaEnvio == '') {
            if (!(regexTime.test(horaEnvio))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'La Hora de Env&iacute;o no es v&aacute;lida';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var senderPeriod = document.getElementById('ddlSenderPeriod').value; ;
            if (senderPeriod == '0') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Periodo de Env&iacute;o no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var senderData = document.getElementById('ddlSenderData').value; ;
            if (senderPeriod == '0') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Periodo de Datos no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var idioma = document.getElementById('ddlIdioma').value; ;
            if (idioma == '0') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Idioma no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var subject = document.getElementById('txtSubject').value; ;
            if (subject == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Asunto no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var body = document.getElementById('txtBody').value; ;
            if (body == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Mensaje no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
        }


        if (divAltaUsuario != null) {
            //Comprobar que no contiene controles vacíos
            var name = document.getElementById('txtName').value; ;
            if (name == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Nombre no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var surname1 = document.getElementById('txtSurname1').value; ;
            if (surname1 == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Primer Apellido no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var surname2 = document.getElementById('txtSurname2').value; ;
            if (surname2 == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Segundo Apellido no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var login = document.getElementById('txtLogin').value; ;
            if (login == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El usuario no es correcto';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var password = document.getElementById('txtPassword').value;
            if (password == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'La contrase&ntilde;a no es correcta';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
        }
                      

        if (divModificarUsuario != null) {
            //Comprobar que no contiene controles vacíos
            var modifyName = document.getElementById('txtModifyName').value; ;
            if (modifyName == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Nombre no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var modifySurname1 = document.getElementById('txtModifySurname1').value; ;
            if (modifySurname1 == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Primer Apellido no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var modifySurname2 = document.getElementById('txtModifySurname2').value; ;
            if (modifySurname2 == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Segundo Apellido no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var modifyLogin = document.getElementById('txtModifyUser').value; ;
            if (modifyLogin == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El usuario no es correcto';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var modifyPassword = document.getElementById('txtModifyPassword').value;
            if (modifyPassword == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'La contrase&ntilde;a no es correcta';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
        }


        if (divEstadoEmail != null) {
            //Comprobar que no contiene controles vacíos y el formato de fecha es correcto
            
            var FechaIni = document.getElementById('txtFechaIni').value; ;
            if (FechaIni != '' && !(regexDate.test(FechaIni))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'La Fecha de Inicio no es v&aacute;lida';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
            //Verificar que la fecha sea válida
            if (regexDate.test(FechaIni)) {
                var valid = IsValidDate(FechaIni)
                if (!valid) {
                    sendForm = false;
                    var newLInode = document.createElement("li");
                    newLInode.innerHTML = 'La Fecha de Inicio no es v&aacute;lida';
                    ulErr.appendChild(newLInode);
                    divErr.style.display = 'block';
                }
            }            

            var FechaFin = document.getElementById('txtFechaFin').value; ;
            if (FechaFin != '' && !(regexDate.test(FechaFin))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'La Fecha de Fin no es v&aacute;lida';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
            //Verificar que la fecha sea válida
            if (regexDate.test(FechaFin)) {
                var valid = IsValidDate(FechaFin)
                if (!valid) {
                    sendForm = false;
                    var newLInode = document.createElement("li");
                    newLInode.innerHTML = 'La Fecha de Inicio no es v&aacute;lida';
                    ulErr.appendChild(newLInode);
                    divErr.style.display = 'block';
                }
            }
        }


        if (divAltaUsuario != null) {
            //Comprobar que no contiene controles vacíos
            var name = document.getElementById('txtName').value; ;
            if (name == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Nombre no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var surname1 = document.getElementById('txtSurname1').value; ;
            if (surname1 == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Primer Apellido no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var surname2 = document.getElementById('txtSurname2').value; ;
            if (surname2 == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El Segundo Apellido no es v&aacute;lido';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var login = document.getElementById('txtLogin').value; ;
            if (login == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'El usuario no es correcto';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var password = document.getElementById('txtPassword').value;
            if (password == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = 'La contrase&ntilde;a no es correcta';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
        }


        if (divSuscription != null) {
            //Comprobar que no contiene controles vacíos
            
            //	Name & Surname
            var name = document.getElementById('txtNameS').value;
            var firstSurname = document.getElementById('txtSurname1S').value;
            var SecondSurname = document.getElementById('txtSurname2S').value;
            var SecSurnameParse = true;

            if (SecondSurname != '' && !regexName.test(SecondSurname)) {
                SecSurnameParse = false;
            }

            if (name == '' || firstSurname == '' || !(regexName.test(name) && regexName.test(firstSurname)) || !SecSurnameParse) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtNameS">Nombre y Apellidos no son correctos</a>';                
                //newLInode.innerHTML = Language.NameSurname;
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            // NIF CIF
            var numNIF = document.getElementById('txtNIF').value.toUpperCase();
            var isCIF = false;

            if (regexCIF.test(numNIF)) {
                if (ValidaCIF(numNIF)) isCIF = true;
            }
            
            if (!isCIF && !(regexDNI.test(numNIF))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtNIF">El NIF &oacute; CIF no es correcto</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
            
            // Telephone
            var telf = document.getElementById('txtMobile').value;
            if (telf == '' || !(regexTel.test(telf))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtMobile">El tel&eacute;fono no es v&aacute;lido</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            // address
            var address = document.getElementById('txtStreet').value;
            if (address == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtStreet">La direcci&oacute;n no es v&aacute;lida</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var addressNum = document.getElementById('txtStreetNum').value;
            if (addressNum != '' && !(regexNumStreet.test(addressNum))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtNum">El n&uacute;mero de calle no es v&aacute;lido<br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9 y /, m&iacute;n. 1 - m&aacute;x. 10 )</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var level = document.getElementById('txtPiso').value;
            if (level != '' && !(regexLevel.test(level))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtPiso">El piso no es v&aacute;lido<br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, &ordf; y &ordm;, m&aacute;x. 10 )</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
           
            // Puerta
            var door = document.getElementById('txtPuerta').value;
            if (door != '' && !(regexDoor.test(door))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtPuerta">La puerta no es v&aacute;lida<br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, &ordf; y &ordm;, m&aacute;x. 10 )</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var postCode = document.getElementById('txtCP').value;
            if (!(regexCP.test(postCode))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtCP">El c&oacute;digo postal no es v&aacute;lido</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            // city
            var city = document.getElementById('txtLocalidad').value;
            if (!(regexName.test(city))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtLocalidad">La localidad no es v&aacute;lida</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
            
            // Province
            var province = document.getElementById('txtProvincia').value;
            if (!(regexName.test(province))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtProvincia">La provincia no es v&aacute;lido</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }            

            //	CreditCard
            var cardType = document.getElementById('ddlTipoTarjeta').value;
            if (cardType == '0') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#ddlTipoTarjeta">Elija un tipo de tarjeta de cr&eacute;dito</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var cardNum = document.getElementById('txtNumTarjeta').value;
            var chkCard = regexCard.test(cardNum) && checkCard(cardNum.toString());
            //Indica que el usuario está dentro de la sesion modificando sus datos.
            //(atributo añadido dinamicamente)
            //IExplorer y Firefox
            //var cardNumSession = document.getElementById('txtNumTarjeta').getAttribute('cardMask');

            //El atributo 'cardMask' guarda la máscara de entrada de la tarjeta de crédito
            //obtenida de la BBDD en el momento de cargar la pagina.
            //Si el usuario ha modificado el número de tarjeta de crédito el atributo "session"
            //y el valor del control no coincidirán.
            //if (cardNumSession == 'undefined' || cardNum != cardNumSession) {            
            if (!chkCard) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtNumTarjeta">El formato de tarjeta de cr&eacute;dito no es correcto.</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            var cardMonth = document.getElementById('ddlMesCad').value;
            var cardYear = document.getElementById('ddlAnyoCad').value;
            if (cardMonth == '0' || cardYear == '0' || !checkCardDate(cardYear, cardMonth)) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#ddlMesCad">Caducidad de la tarjeta de cr&eacute;dito no es v&aacute;lida</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            // Plates
            var platesNum = document.getElementById('txtMatriculas').value;
            if (platesNum == '') {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtMatriculas">La/s matr&iacute;cula/s no tienen un formato v&aacute;lido</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            // UserNAME
            var userName = document.getElementById('txtUsuario').value;
            if (userName == '' || !(regexLogin.test(userName))) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtUsuario">El nombre de usuario no es correcto <br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, m&iacute;n. 4 - m&aacute;x. 10 )</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }

            //Al login se le asigna el valor del NIF cuando se trate de una suscripcion mediante formulario impreso.
//            if (userName == 'Automatico' && sendForm == true)
//                document.getElementById('txtUsuario').value = numNIF;

            // Password
//            var password = document.getElementById('txtPsw').value;
//            var passwordChk = document.getElementById('txtPswRep').value;

//            if (password == '' || !(regexPINNum.test(password)) || password != passwordChk) {
//                sendForm = false;
//                var newLInode = document.createElement("li");
//                newLInode.innerHTML = '<a href="#txtPsw">La contrase&ntilde;a no es correcta.<br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, m&iacute;n. 6 - m&aacute;x. 20 )</a>';
//                ulErr.appendChild(newLInode);
//                divErr.style.display = 'block';
//            }

            //	Email
            if (!regexEmail.test(document.getElementById('txtEmail').value)) {
                sendForm = false;
                var newLInode = document.createElement("li");
                newLInode.innerHTML = '<a href="#txtEmail">El formato de correo electr&oacute;nico no es correcto.</a>';
                ulErr.appendChild(newLInode);
                divErr.style.display = 'block';
            }
        }

        if (!sendForm) {
            window.location = '#errorMsg';
            alert('Compruebe los datos!');
            formObj.action = '';
        }
    }

    return (sendForm);
}




function ControlsVal() {
    var sendForm = true; 

    // Usuario
    var userName = document.getElementById('inpUser').value;
    if (userName == '') {
        sendForm = false;                      
    }  

    // Password
    var password = document.getElementById('inpPswd').value;
    if (password == '') {
        sendForm = false;            
    }

    if (!sendForm) {
        alert('Por favor, introduzca el usuario y la contrase\u00f1a.'); 
    }

    return (sendForm);
}



function DeleteUser()
{
    var Language = GetLanguage();
    var delUser = window.confirm(Language.DeleteUser);
	if(delUser){
		document.location = "delete_user.aspx";
	} else {
		return false;
	}
}




function WidthCells() {
    var gvEmailStatus = document.getElementById('gvEmailStatus');     
    var cols = gvEmailStatus.cells.length / gvEmailStatus.rows.length;

    //Estableciendo el ancho de la 1ª celda se fija este ancho en el resto de celdas que pertenecen a la misma columna       
    for (index = 0; index < cols; index++) {
        if (index == 0) {
            //gvEmailStatus.cells[index].width = "35%";
            gvEmailStatus.cells[index].setAttribute('width', '35%');            
            gvEmailStatus.cells[index].setAttribute('style', 'white-space:normal');
        }
        if (index == 1) {
            //gvEmailStatus.cells[index].width = "25%";
            gvEmailStatus.cells[index].setAttribute('width', '25%');
            //gvEmailStatus.cells[index].setAttribute('style', 'white-space:normal');
        }
        if(index == 2)
            //gvEmailStatus.cells[index].width = "20%";
            gvEmailStatus.cells[index].setAttribute('width', '20%');
        if(index == 3)
            //gvEmailStatus.cells[index].width = "20%";
            gvEmailStatus.cells[index].setAttribute('width', '25%');
    }    
}


Castellano = {
    "NameSurname": "<a href=\"#name\">Nombre y Apellidos no son correctos</a>",    
    "NumNIF": "<a href=\"#DNINIF\">El NIF &oacute; CIF no es correcto</a>",
    "Telf": "<a href=\"#telephone\">El tel&eacute;fono 1 no es v&aacute;lido</a>",
    "Telf2": "<a href=\"#telephone2\">El tel&eacute;fono 2 no es v&aacute;lido</a>",
    "Address": "<a href=\"#address\">La direcci&oacute;n no es v&aacute;lida</a>",
    "AddressNum": "<a href=\"#addressNum\">El n&uacute;mero de calle no es v&aacute;lido</a>",
    "PostCode": "<a href=\"#PostCode\">El c&oacute;digo postal no es v&aacute;lido</a>",
    "City": "<a href=\"#city\">La localidad no es v&aacute;lida</a>",
    "Province": "<a href=\"#province\">La provincia no es v&aacute;lido</a>",
    "Country": "<a href=\"#country\">El pa&iacute;s no es v&aacute;lido</a>",
    "CardType": "<a href=\"#cardType\">Elija un tipo de tarjeta de cr&eacute;dito</a>",
    "CardNum": "<a href=\"#cardNum\">El formato de tarjeta de cr&eacute;dito no es correcto.</a>",
	"CardMonthYear": "<a href=\"#cardMonthExpire\">Caducidad de la tarjeta de cr&eacute;dito no es v&aacute;lida</a>",
	"PlatesNum": "<a href=\"#plates\">La/s matr&iacute;cula/s no tienen un formato v&aacute;lido</a>",
	"UserName": "<a href=\"#user\">El nombre de usuario no es correcto <br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, m&iacute;n. 4 - m&aacute;x. 10 )</a>",
	"Password": "<a href=\"#password\">La contrase&ntilde;a no es correcta.<br/> - ( car&aacute;cteres v&aacute;lidos del 0 al 9, m&iacute;n. 6 - m&aacute;x. 20 )</a>",	
	"Email": "<a href=\"#email\">El formato de correo electr&oacute;nico no es correcto.</a>",
	"DeleteUser": "Desea realmente darse de baja?",
	"lnkFeetManual": "Manual de Usuario"
};


Catalan = {
    "NameSurname": "<a href=\"#name\">El Nom y Cognoms no són correctes</a>",
    "NumNIF": "<a href=\"#DNINIF\">El NIF &oacute; CIF no es correcto</a>",
    "Telf": "<a href=\"#telephone\">El tel&eacute;fono 1 no es v&aacute;lido</a>",
    "Telf2": "<a href=\"#telephone2\">El tel&eacute;fono 2 no es v&aacute;lido</a>",
    "Address": "<a href=\"#address\">La direcci&oacute;n no es v&aacute;lida</a>",
    "AddressNum": "<a href=\"#addressNum\">El n&uacute;mero de calle no es v&aacute;lido</a>",
    "PostCode": "<a href=\"#PostCode\">El c&oacute;digo postal no es v&aacute;lido</a>",
    "City": "<a href=\"#city\">La localidad no es v&aacute;lida</a>",
    "Province": "<a href=\"#province\">La provincia no es v&aacute;lido</a>",
    "Country": "<a href=\"#country\">El pa&iacute;s no es v&aacute;lido</a>",
    "CardType": "<a href=\"#cardType\">Elija un tipo de tarjeta de cr&eacute;dito</a>",
    "CardNum": "<a href=\"#cardNum\">El formato de tarjeta de cr&eacute;dito no es correcto.</a>",
    "CardMonthYear": "<a href=\"#cardMonthExpire\">Caducidad de la tarjeta de cr&eacute;dito no es v&aacute;lida</a>",
    "PlatesNum": "<a href=\"#plates\">La/s matr&iacute;cula/s no tienen un formato v&aacute;lido</a>",
    "UserName": "<a href=\"#user\">El nombre de usuario no es correcto <br/> - ( car&aacute;cteres v&aacute;lidos de la A a la Z &oacute; del 0 al 9, m&iacute;n. 4 - m&aacute;x. 10 )</a>",
    "Password": "<a href=\"#password\">La contrase&ntilde;a no es correcta.<br/> - ( car&aacute;cteres v&aacute;lidos del 0 al 9, m&iacute;n. 6 - m&aacute;x. 20 )</a>",
    "Email": "<a href=\"#email\">El formato de correo electr&oacute;nico no es correcto.</a>",
    "DeleteUser": "\¿Desitja realment donar-se de baixa?",
    "lnkFeetManual": "Manual de Usuari"
};





