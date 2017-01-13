$(document).ready(function () {
    $('.BudgetTable').DataTable({
        "order": [[0, "desc"]],
        "paging": false

    });
});

$(document).ready(function () {
    // Click filter
    $(".phaseRow").click(function () {
        if ($(this).attr('id') != 'selectedPhase') {

            var selectedPhase = document.getElementById('selectedPhase');
            $(selectedPhase).removeAttr('id');

            $(this).attr('id', 'selectedPhase');

            $('.budgetRow').each(function (i, obj) {
                $(this).show();

                if ($(this).attr('data-phase') != $('#selectedPhase').attr('data-selectedPhase')) {
                    $(this).hide();

                }

        })

        }
        else {

            $(this).removeAttr('id');

            $('.budgetRow').each(function (i, obj) {
                $(this).show();
})

        }

        
    })
});

$(document).ready(function () {
    const ctx = document.getElementById("budgetCanvas");

    var data = {
        labels: [
            "Pending",
            "Reviewed",
            "Approved"
        ],
        datasets: [
            {
                data: [$('#budgetCanvas').attr('data-status-total-pending'), $('#budgetCanvas').attr('data-status-total-reviewed'), $('#budgetCanvas').attr('data-status-total-approved')],
                backgroundColor: [
                    "#e6e600",
                    "#064979",
                    "#43AC6A"
                ],
                hoverBackgroundColor: [
                    "#e6e600",
                    "#064979",
                    "#43AC6A"
                ]
            }]
    };


    // For a pie chart
    var budgetChart = new Chart(ctx, {
        type: 'doughnut',
        data: data
    });

    

});