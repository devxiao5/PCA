$(document).ready(function () {
    $('#table_id').DataTable({
        "paging": false
    });
});

$(document).ready(function () {
    $('#budgetDashboard').DataTable({
        "searching": false,
        "pageLength": 3,
        "bLengthChange": false
    });
});

$(document).ready(function () {
    $('.select2').select2();
});
/*$(function () { // will trigger when the document is ready
    $('.datepicker').datepicker(); //Initialise any date pickers
});*/
