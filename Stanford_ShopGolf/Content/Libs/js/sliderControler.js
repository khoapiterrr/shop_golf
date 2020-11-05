var slider = {
    init: function () {
        slider.resgisterEvent();
    },
    resgisterEvent: function () {
        $('.btn-active-slider').off('click').on('click', function (e) {
            e.preventDefault();
            let id = $(this).data('sliderid');
            const tag = $(this)
            $.ajax({
                url: '/Admin/Slider/ChangeActive' + `?id=${id}`,
                type: 'POST',
                success: function (data) {
                    console.log(tag)
                    if (data.Success) {
                        if (data.Data.duyet == true) {
                            tag.html(`<span class='badge badge-success'>Đã duyệt</span>`)
                        } else {
                            tag.html(`<span class='badge badge-danger'>Chưa duyệt</span>`)
                        }
                    }
                    else {
                        swal("Có lỗi", data.Message, "error");
                    }
                },
                cache: false,
                processData: false,
                contentType: false
            });
        })
    }
};

slider.init();