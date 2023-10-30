(function ($) {
    "use strict";

    // Spinner
    var spinner = function () {
        setTimeout(function () {
            if ($('#spinner').length > 0) {
                $('#spinner').removeClass('show');
            }
        }, 1);
    };
    spinner();


    // Initiate the wowjs
    new WOW().init();


    // Sticky Navbar
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.sticky-top').addClass('shadow-sm').css('top', '0px');
        } else {
            $('.sticky-top').removeClass('shadow-sm').css('top', '-100px');
        }
    });


    // Back to top button
    $(window).scroll(function () {
        if ($(this).scrollTop() > 300) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function () {
        $('html, body').animate({ scrollTop: 0 }, 1500, 'easeInOutExpo');
        return false;
    });


    // Modal Video
    var $videoSrc;
    $('.btn-play').click(function () {
        $videoSrc = $(this).data("src");
    });
    console.log($videoSrc);
    $('#videoModal').on('shown.bs.modal', function (e) {
        $("#video").attr('src', $videoSrc + "?autoplay=1&amp;modestbranding=1&amp;showinfo=0");
    })
    $('#videoModal').on('hide.bs.modal', function (e) {
        $("#video").attr('src', $videoSrc);
    })


    // Project and Testimonial carousel
    $(".project-carousel, .testimonial-carousel").owlCarousel({
        autoplay: true,
        smartSpeed: 1000,
        margin: 25,
        loop: true,
        center: true,
        dots: false,
        nav: true,
        navText: [
            '<i class="bi bi-chevron-left"></i>',
            '<i class="bi bi-chevron-right"></i>'
        ],
        responsive: {
            0: {
                items: 1
            },
            576: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            }
        }
    });

    const navHeight = $('.nav-container').outerHeight(true);
    console.log(navHeight);
    $('#about').css("margin-top", "-" + (navHeight-25) + "px");
    $('#why').css("margin-top", "-" + navHeight + "px");
    $('#gallery').css("margin-top", "-" + navHeight + "px");
    $('#pricing').css("margin-top", "-" + navHeight + "px");
    $('#contact').css("margin-top", "-" + navHeight + "px");


    $.ajax({
        url: "https://localhost:50969/api/packageevent/best-deal",
        type: "GET",
        headers: {
            'Content-Type': 'application/json'
        }
    }).done((result) => {
        console.log(result);
        console.log(result.data);
        let packageEl = "";
        if (result.data.length > 0) {
            result.data.forEach(data => {
                const price = data.price.toLocaleString('id-ID');;
                const descArray = data.description.split(", ");

                let liDesc = "";
                descArray.forEach(i => { liDesc += `<li>${i}</li>` })

                packageEl += `

            <div class="col-lg-3 col-md-6 wow fadeInUp rounded" data-wow-delay="0.1s">
                <div class="service-item position-relative h-100 rounded">
                    <div class="service-text h-100 rounded">
                        <div class="price-block-header pt-4 pb-4 rounded-top" style="background-color: #eeeeee;">
                            <h4 class="m-0">${data.name}</h4>
                        </div>
                        <div class="price-block-body pb-3 pt-4">
                            <h4 class="text-primary mb-4 mt-3">Rp.${price}</h4>
                            <ul class="price-list p-0" style="list-style: none;">
                                <li>Up to ${data.capacity}</li>
                                ${liDesc}
                            </ul>
                            <a href="Auth/Login" class="btn btn-outline-primary rounded-pill py-2 px-3 mt-3 mb-5">Order Now</a>
                        </div>
                    </div>
                </div>
            </div>

            `;
            });
        } else {
            packageEl = `
            <div class="col-lg-12 col-md-6 wow fadeInUp rounded" data-wow-delay="0.1s">
                <h4 class="text-center">Data Tidak Ada</h4>
            </div>
            `
        }
        

        $('#package-container').html(packageEl);
        $('.price-list').each(function (index, element) {
            $(this).find("li").slice(4).hide();
        });
        
        
    }).fail((error) => {
        console.log(error);
    })

})(jQuery);

