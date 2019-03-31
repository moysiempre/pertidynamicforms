// JavaScript Document
$(document).ready(function(){

   //submission scripts
  //$('.contactForm').submit( function(){
	$('#enviarBtn').click( function(e){	
	//$('a.sendButton').click( function(e){
		
  		  e.preventDefault();
		//statements to validate the form	
		var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
		var email = document.getElementById('mailId');
		//alert(email.value);
		if (!filter.test(email.value)) {
			$('.sin_email').show();
		} else {$('.sin_email').hide();}
		if (document.contactoT.nombreT.value == "") {
			$('.sin_nombre').show();
		} else {$('.sin_nombre').hide();}
		if (document.contactoT.empresaT.value == "") {
			$('.sin_empresa').show();
		} else {$('.sin_empresa').hide();}	
		/*if (document.contactoT.asuntoT.value == "") {
			$('.sin_asunto').show();
		} else {$('.sin_asunto').hide();}*/	
		if (document.contactoT.telefonoT.value == "") {
			$('.sin_telefono').show();
		} else {$('.sin_telefono').hide();}
		if (document.contactoT.mensajeT.value == "") {
			$('.sin_mensaje').show();
		} else {$('.sin_mensaje').hide();}		
		if ((document.contactoT.nombreT.value == "") || (!filter.test(email.value)) || (document.contactoT.mensajeT.value == "")){
			return false;
		} 
		
		if ((document.contactoT.nombreT.value != "") && (filter.test(email.value)) && (document.contactoT.mensajeT.value != "")) {
			//hide the form
			$('.formaDeContacto').hide();
			$('#enviarBtn').hide();
			$('#sendlabel').hide();		
			//show the loading bar
			$('.loader1').append($('.bar'));
			$('.bar1').css({display:'block'});
		
			//send the ajax request
			$.post('mail.php',{name:$('#nombreId').val(),
							  empresa:$('#empresaId').val(),
							  email:$('#mailId').val(),
							  telefono:$('#telefonoId').val(),
							  asunto:$('#asuntoId').val(),
							  message:$('#messageId').val()},
		
			//return the data
			//function(data){
				function(){
					$(location).attr("href","http://grupoperti.com.mx/gracias.html");
			  //hide the graphic
			  /*$('.bar1').css({display:'none'});
			  $('.loader1').append(data);			  */
			});
			
			//waits 2000, then closes the form and fades out
			//setTimeout('$("#backgroundPopup").fadeOut("slow"); $("#contactForm").slideUp("slow")', 2000);
			
			//stay on the page
			return false;
		} 
  });
	//only need force for IE6  
	
});