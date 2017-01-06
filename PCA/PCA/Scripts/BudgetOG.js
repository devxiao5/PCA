$(document).ready(function () {
    const ctx = document.getElementById("budgetCanvas");

    var budgetCanvas = new Chart(ctx, {
        type: 'pie',
        data: {
        labels: [
            "Site",
            "Equipment",
            "Building",
            "Misc"
        ],
        datasets: [
            {
                data: [$('#budgetCanvas').attr('data-site-total'), $('#budgetCanvas').attr('data-equipment-total'),
                    $('#budgetCanvas').attr('data-building-total'), $('#budgetCanvas').attr('data-misc-total')],
                backgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56",
                    "#FFCE56"
                ],
                hoverBackgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56",
                    "#FFCE56"
                ]
            }]
    }, 
        options: {
        animation: {
            duration: 5000
        }
      }

    })
});