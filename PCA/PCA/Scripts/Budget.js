let ctx = document.getElementById("budgetCanvas");

    let data = {
        labels: [
            "Red",
            "Blue",
            "Yellow"
        ],
        datasets: [
            {
                data: [300, 50, 100],
                backgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56"
                ],
                hoverBackgroundColor: [
                    "#FF6384",
                    "#36A2EB",
                    "#FFCE56"
                ]
            }]
    };

    let options = {
        animation: {
            duration: 5000
        }
    }

    var budgetCanvas = new Chart(ctx, {
        type: 'doughnut',
        data: data,
        options: options
    });