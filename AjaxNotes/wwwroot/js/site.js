$(document).ready(function(){

    $(".dynamicNoteText").hide();

    $(".staticNoteText").click(function(){
        $(".dynamicNoteText").show();
        $(".staticNoteText").hide();
    });    

    $(".dynamicNoteText").keypress(function(e) {
        if(e.which == 13) {
            console.log($(".dynamicNoteText").val());
            $(".submitButton").click();
        }
    });
})