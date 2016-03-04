var days = ['sunday', 'monday', 'tuesday', 'wednesday', 'thursday', 'friday', 'saturday', 'sunday'];
var day = new Date().getDay();    // 0: sun, 1: mon, 2: tues
var dayString = days[day].charAt(0).toUpperCase() + days[day].slice(1)

$(document).ready(function () {
    $("#" + days[day]).removeClass("collaspedDay collapse")
    $("#" + days[day] + " td:first-child").text("Today");
    if (day == 0)       // shift sunday to be last of week 
        day = 7;
    for(var i = 1; i < day; i++)
    {
        $("#"+days[i]).insertAfter("#sunday");
    }

    $('#openingHoursToggle').click(function () {
        var txt = $("#openingHoursToggle").text() == '(Show All)';
        if (txt) {
            $("#" + days[day] + " td:first-child").text(dayString);
            $("#openingHoursToggle").text("(Hide All)");
        }
        else {
            $("#" + days[day] + " td:first-child").text("Today");
            $("#openingHoursToggle").text("(Show All)");
        }
        $(".collaspedDay").toggle();        // toggle visibility of days for opening hours
    });

});

