//console.log("funcionando");
$("#contacto").submit(function(event){
	event.preventDefault();
	enviar();
});
function enviar(){
	//console.log("ejecutado");
	var datos=$("#contacto").serialize();//datos en forma de arreglo
	console.log(datos);
	$.ajax({
		method:"POST",
		url:"formulario.php",
		data: datos,
		success: function(texto){
			if(texto=="exito"){
				correcto();
				clearInputs();
				console.log(texto);
			}else{
				phperror(texto);
				console.log(texto);
			}
		}
	});
}
function correcto(){
	$("#mensaje-exito").removeClass("d-none");
	$("#mensaje-error").addClass("d-none");
	$(location).attr("href", "http://grupoperti.com.mx/gracias.html");
}
function phperror(texto){
	$("#mensaje-error").removeClass("d-none");
	$("#mensaje-error").html(texto);
}
function clearInputs(data){
	$('#nombre').val('');
	$('#empresa').val('');
	$('#email').val('');
	$('#telefono').val('');
	$('#mensaje').val('');

}