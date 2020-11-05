$(document).ready(function () {
    $('.btn-quickview').click(function (e) {
        e.preventDefault();
        $.ajax({
            type: "GET",
            url: $(this).attr('href'),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: true,
            success: function (data) {
                $('#quickViewProduct .modal-body').html(data);
                $('#quickViewProduct').modal('show');
                setTimeout(updateGlasse, 500);
            },
            error: function () {
                alert("Content load failed.");
            }
        });
    });
});

function updateGlasse() {
    $('.gc-start').glassCase({
        zoomAlignment: "displayArea",
        thumbsPosition: "left",
        zoomPosition: "inner",
        isZoomDiffWH: false
    });
}