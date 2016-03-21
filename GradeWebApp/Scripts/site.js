$(function () {
    // Hide show scroll to top arrow
    $(window).scroll(function () {
        if ($(this).scrollTop() > 20) {
            $("#scroll-to-top").fadeIn(500);
        } else {
            $("#scroll-to-top").fadeOut(500);
        }
    });

    // Scroll gently to top if scroll to top arrow is clicked
    $("#scroll-to-top").click(function (e) {
        e.preventDefault();
        $('html, body').animate({ scrollTop: 0 }, 500);
    });
});