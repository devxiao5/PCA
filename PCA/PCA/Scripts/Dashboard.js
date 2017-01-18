$(document).ready(function () {

    $(".workflow-link").click(function () {
        var linktextstatus = $(this).attr("data-workflow-status");
        window.location.replace("/DailyReport/WorkflowRedirect?project=0" + "&status=" + linktextstatus);

    })



});
