$(document).ready(function () {
    $('.gc-start').glassCase({
        zoomAlignment: "displayArea",
        thumbsPosition: "left",
        zoomPosition: "inner",
        isZoomDiffWH: false
    });

    $('#input-rate').rateYo({
        rating: 0,
        starWidth: '28px',
        onChange: function (rating) {
            $('#inputRateValue').val(rating);
        }
    });
});