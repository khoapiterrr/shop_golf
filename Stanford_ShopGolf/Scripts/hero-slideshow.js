$(document).ready(function(){
    $('.hero-slider.single-item').slick({
      dots: true,
      infinite: true,
        speed: 300,
        autoplay: true,
      slidesToShow: 1,
      adaptiveHeight: false
    });
    $('.hero-slider.multiple-item').slick({
        dots: true,
        infinite: true,
        speed: 300,
        slidesToShow: 4,
        slidesToScroll: 4,
        adaptiveHeight: true,
        responsive: [{
            breakpoint: 1024,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
                infinite: true
            }

        }, {

            breakpoint: 600,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
                dots: true
            }

        }, {

            breakpoint: 300,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1,
                dots: false
            }
        }]
    });
});