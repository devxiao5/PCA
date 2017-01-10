$(document).ready(function () {
    $('.BudgetTable').DataTable({
        "order": [[0, "desc"]],
        "paging": true

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