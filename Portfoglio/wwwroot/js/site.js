// Write your JavaScript code.
$(".cardClick").on("click", function(){
    UIkit.lightbox(this).show(1);
});

$("#checkbox").on("click", function(){
    if ($(this).is(':checked')) {
        $("#hiddenCheckbox").val(true);
    }
    else{
        $("#hiddenCheckbox").val(false);
    }
});