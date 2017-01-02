$(document).ready(function () {
    const budgetChart = document.getElementById("budgetCanvas");

    let budgetCanvas = new Chart(budgetChart, {
        type: 'pie',
        data: {
            labels: [ 'Site', 'Building', 'Materials', 'Misc'],
            datasets: [
                {
                    data: [333, 333, 333, 5],
                    backgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56",
                        "#333",
                    ],
                    hoverBackgroundColor: [
                        "#FF6384",
                        "#36A2EB",
                        "#FFCE56",
                        "#333",
                    ]
                }]
        },
        options: {
            animation: {
                animateScale: true
            }
        }
    });

});