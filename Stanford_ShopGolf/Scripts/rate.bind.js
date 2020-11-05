$(function () {
    $(".rate-display").each(function (index, value) {
        var item = $(this).rateYo({
            rating: $(this).data("value"),
            readOnly: true,
            starWidth: "20px"
        });
        
    });
    
});