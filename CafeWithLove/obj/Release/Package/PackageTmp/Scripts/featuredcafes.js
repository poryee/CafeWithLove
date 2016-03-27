$(document).ready(function () {
    var height = 0;
    $(".featured-cafe-thumbnail").each(function (index) {
        console.log($(this).height());

        if ($(this).height() > height)
            height = $(this).height();
    });

    console.log(height + "px");

    $(".featured-cafe-thumbnail").each(function (index) {
        $(this).height(height + "px");
    });
});

