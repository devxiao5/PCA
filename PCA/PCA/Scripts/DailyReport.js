// Analytics 
$(document).ready(function () {
    const CHART = document.getElementById("dailyReportCanvas");

    // Dates for x-axis
    var day1 = $('#dailyReportCanvas').attr('data-weekreport-1');
    var day2 = $('#dailyReportCanvas').attr('data-weekreport-2');
    var day3 = $('#dailyReportCanvas').attr('data-weekreport-3');
    var day4 = $('#dailyReportCanvas').attr('data-weekreport-4');
    var day5 = $('#dailyReportCanvas').attr('data-weekreport-5');
    var day6 = $('#dailyReportCanvas').attr('data-weekreport-6');
    var day7 = $('#dailyReportCanvas').attr('data-weekreport-7');

    var day1value, day2value, day3value, day4value, day5value, day6value, day7value;

    // Get values for analytics
    if ($("#" + day1).length == 0) {
        day1value = 0;
    }
    else {
        day1value = $("#" + day1).attr('data-totalHours')
    }

    if ($("#" + day2).length == 0) {
        day2value = 0;
    }
    else {
        day2value = $("#" + day2).attr('data-totalHours')
    }

    if ($("#" + day3).length == 0) {
        day3value = 0;
    }
    else {
        day3value = $("#" + day3).attr('data-totalHours')
    }

    if ($("#" + day4).length == 0) {
        day4value = 0;
    }
    else {
        day4value = $("#" + day4).attr('data-totalHours')
    }

    if ($("#" + day5).length == 0) {
        day5value = 0;
    }
    else {
        day5value = $("#" + day5).attr('data-totalHours')
    }

    if ($("#" + day6).length == 0) {
        day6value = 0;
    }
    else {
        day6value = $("#" + day6).attr('data-totalHours')
    }

    if ($("#" + day7).length == 0) {
        day7value = 0;
    }
    else {
        day7value = $("#" + day7).attr('data-totalHours')
    }

    // Create Chart with data
    let dailyReportCanvas = new Chart(CHART, {
        type: 'line',
        data: {
            labels: [day1, day2, day3, day4, day5, day6, day7],
            datasets: [
                {
                    label: "Hours Worked",
                    fill: true,
                    lineTension: 0.1,
                    backgroundColor: "rgba(23, 150, 243, 0.25)",
                    borderColor: "#064979",
                    borderCapStyle: 'butt',
                    borderDash: [],
                    borderDashOffset: 0.0,
                    borderJoinStyle: 'miter',
                    pointBorderColor: "#064979",
                    pointBackgroundColor: "#fff",
                    pointBorderWidth: 1,
                    pointHoverRadius: 5,
                    pointHoverBackgroundColor: "#064979",
                    pointHoverBorderColor: "#064979",
                    pointHoverBorderWidth: 2,
                    pointRadius: 1,
                    pointHitRadius: 10,
                    data: [day1value, day2value, day3value, day4value, day5value, day6value, day7value],
                    spanGaps: false,
                }
            ]
        }
    })
});

// Filter Options
$(document).ready(function () {
    // Click filter
    $(".dailyReportStatusFilter").click(function () {

        // Hide Link buttons
        $("#LinkButtonGroup").css({ 'visibility': 'hidden', 'max-height': '0px' });

        // Set current filter text
        var filterText = $(this).text();
        $('#selection').html(filterText);

        var selecteditem = document.getElementById('LinkButtonSelected');
        $(selecteditem).removeAttr('id');

        

        $('.dailyReportRow').each(function (i, obj) {
            $(this).show();
        })

        if (filterText == "All") {
            $('.dailyReportRow').each(function (i, obj) {
                $(this).show();
            })
        }
        else
            $('.dailyReportRow').each(function (i, obj) {
            if ($(this).attr('data-status') != filterText) {
                $(this).hide();
            }
        })
    })
});